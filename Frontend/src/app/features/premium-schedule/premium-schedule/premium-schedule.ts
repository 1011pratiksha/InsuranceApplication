import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';


import {
  PremiumScheduleService,
  PremiumSchedule as ScheduleData,
  PremiumScheduleRequest,
  ApiResponse
} from '../../../services/premium-services/premium-schedule';

import {
  PaymentService,
  PaymentRecord
} from '../../../services/premium-services/payment';

import {
  RefundService
} from '../../../services/premium-services/refund';


interface Installment {
  scheduleId: number;
  installmentNo: number;
  dueDate: string;
  amount: number;
  lateFee: number;
  status: string;
}

@Component({
  selector: 'app-premium-schedule',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './premium-schedule.html'
})
export class PremiumSchedule implements OnInit {
  policyId = 1;

  installments: Installment[] = [];
  payments: PaymentRecord[] = [];

  searchText = '';
  selectedStatus = '';
  fromDate = '';
  toDate = '';

  currentPage = 1;
  pageSize = 6;

  loading = false;
  generating = false;
  payingScheduleId: number | null = null;
  savingScheduleId: number | null = null;
  loadingHistory = false;

  errorMessage = '';
  successMessage = '';
  paymentMessage = '';
  historyError = '';

  showGenerateForm = false;

  scheduleRequest: PremiumScheduleRequest = {
    policyId: 1,
    premiumFrequency: 'Monthly',
    policyTermInYears: 1,
    annualPremium: 12000,
    startDate: new Date().toISOString().slice(0, 10)
  };

  editStatus: Record<number, string> = {};
  editDueDate: Record<number, string> = {};

  refundPaymentId: number | null = null;
  refundAmount = 0;
  refundLookupId: number | null = null;
  refundResult: any = null;
  refundMessage = '';
  refundError = '';
  refundLoading = false;

  constructor(
    private scheduleService: PremiumScheduleService,
    private paymentService: PaymentService,
    private refundService: RefundService
  ) {}

  ngOnInit(): void {
    this.loadSchedule();
    this.loadPaymentHistory();
  }

  loadSchedule(): void {
    if (!this.policyId || this.policyId <= 0) {
      this.errorMessage = 'Enter a valid policy ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    this.scheduleService.getPremiumSchedule(this.policyId)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: (response: ApiResponse<ScheduleData[]>) => {
          this.installments = (response.data ?? []).map(item => ({
            scheduleId: item.schedule_id,
            installmentNo: item.installment_no,
            dueDate: this.toDateInput(item.due_date),
            amount: item.premium_amount,
            lateFee: item.late_fee ?? 0,
            status: item.status
          }));

          this.installments.forEach(item => {
            this.editStatus[item.scheduleId] = item.status;
            this.editDueDate[item.scheduleId] = item.dueDate;
          });

          this.currentPage = 1;
        },
        error: error => {
          this.installments = [];
          this.errorMessage = this.getError(
            error,
            'Could not load the schedule. Check the API URL and backend.'
          );
        }
      });
  }

  generateSchedule(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (
      this.scheduleRequest.policyId <= 0 ||
      this.scheduleRequest.policyTermInYears <= 0 ||
      this.scheduleRequest.annualPremium <= 0 ||
      !this.scheduleRequest.startDate
    ) {
      this.errorMessage =
        'Enter a valid policy ID, term, premium and start date.';
      return;
    }

    this.generating = true;

    this.scheduleService.generateSchedule(this.scheduleRequest)
      .pipe(finalize(() => this.generating = false))
      .subscribe({
        next: response => {
          this.policyId = this.scheduleRequest.policyId;
          this.successMessage =
            response.message || 'Schedule generated successfully.';
          this.showGenerateForm = false;
          this.loadSchedule();
        },
        error: error => {
          this.errorMessage = this.getError(
            error,
            'Schedule generation failed. Check the policy details and try again.'
          );
        }
      });
  }

  updateInstallment(item: Installment): void {
    this.errorMessage = '';
    this.successMessage = '';

    this.savingScheduleId = item.scheduleId;

    this.scheduleService.updateInstallment(item.scheduleId, {
      Status: this.editStatus[item.scheduleId],
      DueDate: this.editDueDate[item.scheduleId] || null
    })
      .pipe(finalize(() => this.savingScheduleId = null))
      .subscribe({
        next: response => {
          this.successMessage = response.message || 'Installment updated.';
          this.loadSchedule();
        },
        error: error => {
          this.errorMessage = this.getError(
            error,
            'Could not update the installment.'
          );
        }
      });
  }

  payNow(item: Installment): void {
    this.errorMessage = '';
    this.paymentMessage = '';

    if (item.status === 'Paid' || item.status === 'Cancelled') {
      this.errorMessage = 'This installment cannot be paid.';
      return;
    }

    if (typeof window.Razorpay !== 'function') {
      this.errorMessage =
        'Razorpay Checkout did not load. Refresh the page and try again.';
      return;
    }

    this.payingScheduleId = item.scheduleId;

    this.paymentService.createPayment(item.scheduleId)
      .pipe(finalize(() => this.payingScheduleId = null))
      .subscribe({
        next: order => {
          const checkoutOptions: Record<string, any> = {
            key: order.razorpay_key_id,
            amount: order.amount,
            currency: order.currency,
            name: 'Premium Payment',
            description:
              `Policy ${this.policyId}, installment ${item.installmentNo}`,
            order_id: order.razorpay_order_id,
            handler: (result: any) => {
              this.verifyCheckoutPayment(order.payment_id, result);
            },
            modal: {
              ondismiss: () => {
                this.paymentMessage =
                  'Checkout closed. Payment is not confirmed.';
              }
            },
            theme: {
              color: '#0d9488'
            }
          };

          const checkout = new window.Razorpay(checkoutOptions);

          checkout.on('payment.failed', (failure: any) => {
            this.errorMessage =
              failure?.error?.description || 'Razorpay payment failed.';
          });

          checkout.open();
        },
        error: error => {
          this.errorMessage = this.getError(
            error,
            'Could not create a payment order.'
          );
        }
      });
  }

  private verifyCheckoutPayment(paymentId: number, result: any): void {
    this.paymentMessage = 'Verifying payment securely...';
    this.errorMessage = '';

    this.paymentService.verifyPayment({
      payment_id: paymentId,
      razorpay_order_id: result.razorpay_order_id,
      razorpay_payment_id: result.razorpay_payment_id,
      razorpay_signature: result.razorpay_signature
    }).subscribe({
      next: response => {
        this.paymentMessage =
          response.message || 'Payment verified successfully.';
        this.loadSchedule();
        this.loadPaymentHistory();
      },
      error: error => {
        this.paymentMessage = '';
        this.errorMessage = this.getError(
          error,
          'Payment verification failed. Check payment status before retrying.'
        );
        this.loadPaymentHistory();
      }
    });
  }

  loadPaymentHistory(): void {
    if (!this.policyId || this.policyId <= 0) {
      this.payments = [];
      return;
    }

    this.loadingHistory = true;
    this.historyError = '';

    this.paymentService.getPaymentHistory(this.policyId)
      .pipe(finalize(() => this.loadingHistory = false))
      .subscribe({
        next: payments => {
          this.payments = payments ?? [];
        },
        error: error => {
          this.payments = [];
          this.historyError = this.getError(
            error,
            'Could not load payment history.'
          );
        }
      });
  }

  downloadReceipt(payment: PaymentRecord): void {
    if (payment.payment_status !== 'Success') {
      this.errorMessage =
        'Receipt is available only for successful payments.';
      return;
    }

    this.errorMessage = '';

    this.paymentService.downloadReceipt(payment.payment_id).subscribe({
      next: blob => {
        const url = URL.createObjectURL(blob);
        const link = document.createElement('a');

        link.href = url;
        link.download = `payment-receipt-${payment.payment_id}.pdf`;
        link.click();

        URL.revokeObjectURL(url);
      },
      error: error => {
        this.errorMessage = this.getError(
          error,
          'Receipt download failed. Try regenerating the receipt.'
        );
      }
    });
  }

  regenerateReceipt(payment: PaymentRecord): void {
    this.errorMessage = '';
    this.successMessage = '';

    this.paymentService.regenerateReceipt(payment.payment_id).subscribe({
      next: response => {
        this.successMessage =
          response.message || 'Receipt generated successfully.';
        this.downloadReceipt(payment);
      },
      error: error => {
        this.errorMessage = this.getError(
          error,
          'Receipt regeneration failed.'
        );
      }
    });
  }

  requestRefund(payment: PaymentRecord): void {
    this.refundError = '';
    this.refundMessage = '';

    if (payment.payment_status !== 'Success') {
      this.refundError = 'Only successful payments can be refunded.';
      return;
    }

    if (this.refundAmount <= 0) {
      this.refundError = 'Enter a refund amount greater than zero.';
      return;
    }

    this.refundLoading = true;

    this.refundService.createRefund(
      payment.payment_id,
      this.refundAmount
    )
      .pipe(finalize(() => this.refundLoading = false))
      .subscribe({
        next: response => {
          this.refundMessage =
            response.message || 'Refund request submitted.';
          this.refundLookupId = response.refund_id;
          this.refundPaymentId = payment.payment_id;
          this.refundAmount = 0;
        },
        error: error => {
          this.refundError = this.getError(
            error,
            'Refund request failed.'
          );
        }
      });
  }

  lookupRefund(): void {
    if (!this.refundLookupId || this.refundLookupId <= 0) {
      this.refundError = 'Enter a valid refund ID.';
      return;
    }

    this.refundError = '';
    this.refundMessage = '';
    this.refundResult = null;
    this.refundLoading = true;

    this.refundService.getRefund(this.refundLookupId)
      .pipe(finalize(() => this.refundLoading = false))
      .subscribe({
        next: result => {
          this.refundResult = result;
        },
        error: error => {
          this.refundError = this.getError(error, 'Refund not found.');
        }
      });
  }

  submitRefund(): void {
    if (!this.refundPaymentId || this.refundAmount <= 0) {
      this.refundError = 'Enter a valid payment ID and refund amount.';
      return;
    }

    const payment = this.payments.find(
      item => item.payment_id === this.refundPaymentId
    );

    if (!payment) {
      this.refundError = 'Payment record not found.';
      return;
    }

    this.requestRefund(payment);
  }

  get filteredInstallments(): Installment[] {
    const search = this.searchText.trim().toLowerCase();

    return this.installments.filter(item => {
      const matchesSearch =
        String(item.installmentNo).includes(search) ||
        String(item.scheduleId).includes(search);

      const matchesStatus =
        !this.selectedStatus || item.status === this.selectedStatus;

      const matchesFrom =
        !this.fromDate || item.dueDate >= this.fromDate;

      const matchesTo =
        !this.toDate || item.dueDate <= this.toDate;

      return matchesSearch && matchesStatus && matchesFrom && matchesTo;
    });
  }

  get paginatedInstallments(): Installment[] {
    const start = (this.currentPage - 1) * this.pageSize;

    return this.filteredInstallments.slice(
      start,
      start + this.pageSize
    );
  }

  get totalPages(): number {
    return Math.max(
      1,
      Math.ceil(this.filteredInstallments.length / this.pageSize)
    );
  }

  get totalDue(): number {
    return this.installments
      .filter(item =>
        item.status !== 'Paid' && item.status !== 'Cancelled'
      )
      .reduce(
        (sum, item) => sum + item.amount + item.lateFee,
        0
      );
  }

  get paidCount(): number {
    return this.installments.filter(
      item => item.status === 'Paid'
    ).length;
  }

  get pendingCount(): number {
    return this.installments.filter(item =>
      item.status === 'Pending' || item.status === 'Overdue'
    ).length;
  }

  onFilterChange(): void {
    this.currentPage = 1;
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  changePolicy(): void {
    this.scheduleRequest.policyId = this.policyId;
    this.loadSchedule();
    this.loadPaymentHistory();
  }

  clearFilters(): void {
    this.searchText = '';
    this.selectedStatus = '';
    this.fromDate = '';
    this.toDate = '';
    this.currentPage = 1;
  }

  private toDateInput(value: string): string {
    return value ? value.slice(0, 10) : '';
  }

  private getError(error: any, fallback: string): string {
    if (typeof error?.error === 'string') {
      return error.error;
    }

    return error?.error?.error
      || error?.error?.message
      || error?.message
      || fallback;
  }
}