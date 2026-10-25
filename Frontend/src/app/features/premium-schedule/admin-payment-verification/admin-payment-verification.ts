
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaymentService } from '../../../services/premium-services/payment';

@Component({
  selector: 'app-admin-payment-verification',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-payment-verification.html'
})
export class AdminPaymentVerification {
  paymentId: number | null = null;
  payment: any = null;

  loading = false;
  receiptLoading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private paymentService: PaymentService) {}

  findPayment(): void {
    if (!this.paymentId || this.paymentId <= 0) {
      this.errorMessage = 'Please enter a valid Payment ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.payment = null;

    this.paymentService.getPayment(this.paymentId).subscribe({
      next: (response: any) => {
        this.payment = response?.data ?? response;
        this.loading = false;

        if (!this.payment || !this.payment.payment_id) {
          this.errorMessage = 'No payment details were returned.';
        }
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to retrieve payment details.';
      }
    });
  }

  downloadReceipt(): void {
    if (!this.payment?.payment_id) {
      this.errorMessage = 'Find a payment before downloading its receipt.';
      return;
    }

    this.receiptLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.paymentService.downloadReceipt(this.payment.payment_id).subscribe({
      next: (response: any) => {
        this.receiptLoading = false;

        if (response instanceof Blob) {
          this.saveReceipt(response);
        } else if (response instanceof ArrayBuffer) {
          this.saveReceipt(
            new Blob([response], { type: 'application/pdf' })
          );
        } else {
          this.errorMessage =
            'The receipt response is not a PDF file. Check the existing downloadReceipt method in payment.ts.';
        }
      },
      error: (error: any) => {
        this.receiptLoading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to download the receipt.';
      }
    });
  }

  regenerateReceipt(): void {
    if (!this.payment?.payment_id) {
      this.errorMessage = 'Find a payment before regenerating its receipt.';
      return;
    }

    this.receiptLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.paymentService.regenerateReceipt(this.payment.payment_id).subscribe({
      next: () => {
        this.receiptLoading = false;
        this.successMessage =
          'Receipt regeneration completed. You can download the receipt now.';
      },
      error: (error: any) => {
        this.receiptLoading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to regenerate the receipt.';
      }
    });
  }

  private saveReceipt(file: Blob): void {
    const url = URL.createObjectURL(file);
    const link = document.createElement('a');

    link.href = url;
    link.download = `receipt-${this.payment.payment_id}.pdf`;
    link.click();

    URL.revokeObjectURL(url);
    this.successMessage = 'Receipt downloaded successfully.';
  }
}