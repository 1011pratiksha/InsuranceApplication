
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { PaymentService } from '../../../services/premium-services/payment';
import { PremiumScheduleService, PremiumSchedule } from '../../../services/premium-services/premium-schedule';

@Component({
  selector: 'app-pay-premium',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pay-premium.html'
})
export class PayPremium {
  policyId = 1;
  installments: PremiumSchedule[] = [];
  loading = false;
  payingScheduleId: number | null = null;
  errorMessage = '';
  successMessage = '';

  constructor(
    private scheduleService: PremiumScheduleService,
    private paymentService: PaymentService
  ) {}

  loadSchedule(): void {
    if (!this.policyId || this.policyId <= 0) {
      this.errorMessage = 'Enter a valid policy ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.scheduleService.getPremiumSchedule(this.policyId)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: (response: any) => {
          this.installments = response.data ?? [];
        },
        error: (error: any) => {
          this.installments = [];
          this.errorMessage =
            error?.error?.message ||
            error?.error?.error ||
            'Could not load premium installments.';
        }
      });
  }

  payNow(item: PremiumSchedule): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (item.status === 'Paid' || item.status === 'Cancelled') {
      this.errorMessage = 'This installment cannot be paid.';
      return;
    }

    if (typeof window.Razorpay !== 'function') {
      this.errorMessage = 'Razorpay Checkout did not load. Refresh the page and try again.';
      return;
    }

    this.payingScheduleId = item.schedule_id;

    this.paymentService.createPayment(item.schedule_id)
      .pipe(finalize(() => this.payingScheduleId = null))
      .subscribe({
        next: (order: any) => {
          const options: Record<string, any> = {
            key: order.razorpay_key_id,
            amount: order.amount,
            currency: order.currency,
            name: 'Premium Payment',
            description: `Policy ${this.policyId}, installment ${item.installment_no}`,
            order_id: order.razorpay_order_id,
            handler: (result: any) => {
              this.verifyPayment(order.payment_id, result);
            },
            modal: {
              ondismiss: () => {
                this.successMessage = 'Checkout closed. Payment has not been confirmed.';
              }
            },
            theme: {
              color: '#0d9488'
            }
          };

          const checkout = new window.Razorpay(options);

          checkout.on('payment.failed', (failure: any) => {
            this.errorMessage =
              failure?.error?.description || 'Razorpay payment failed.';
          });

          checkout.open();
        },
        error: (error: any) => {
          this.errorMessage =
            error?.error?.message ||
            error?.error?.error ||
            'Could not create the payment order.';
        }
      });
  }

  private verifyPayment(paymentId: number, result: any): void {
    this.errorMessage = '';
    this.successMessage = 'Verifying payment securely...';

    this.paymentService.verifyPayment({
      payment_id: paymentId,
      razorpay_order_id: result.razorpay_order_id,
      razorpay_payment_id: result.razorpay_payment_id,
      razorpay_signature: result.razorpay_signature
    }).subscribe({
      next: (response: any) => {
        this.successMessage = response.message || 'Payment verified successfully.';
        this.loadSchedule();
      },
      error: (error: any) => {
        this.successMessage = '';
        this.errorMessage =
          error?.error?.message ||
          error?.error?.error ||
          'Payment verification failed. Check payment status before retrying.';
      }
    });
  }
}

