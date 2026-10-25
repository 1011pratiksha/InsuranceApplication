
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaymentService } from '../../../services/premium-services/payment';

@Component({
  selector: 'app-payment-history',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './payment-history.html'
})
export class PaymentHistory {
  policyId = 1;
  payments: any[] = [];
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private paymentService: PaymentService) {}

  loadPaymentHistory(): void {
    if (!this.policyId || this.policyId <= 0) {
      this.errorMessage = 'Please enter a valid Policy ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.payments = [];

    this.paymentService.getPaymentHistory(this.policyId).subscribe({
      next: (response: any) => {
        const result = response?.data ?? response;
        this.payments = Array.isArray(result) ? result : [];
        this.loading = false;

        if (this.payments.length === 0) {
          this.successMessage = 'No payment history found for this policy.';
        }
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to load payment history. Please check the Policy ID.';
      }
    });
  }
}