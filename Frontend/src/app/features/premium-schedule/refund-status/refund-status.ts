
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RefundService } from '../../../services/premium-services/refund';

@Component({
  selector: 'app-refund-status',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './refund-status.html'
})
export class RefundStatus {
  paymentId: number | null = null;
  refundAmount: number | null = null;
  refundId: number | null = null;

  refundDetails: any = null;
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private refundService: RefundService) {}

  submitRefund(): void {
    if (!this.paymentId || this.paymentId <= 0) {
      this.errorMessage = 'Please enter a valid Payment ID.';
      return;
    }

    if (
      this.refundAmount === null ||
      this.refundAmount <= 0
    ) {
      this.errorMessage = 'Please enter a valid refund amount.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.refundDetails = null;

    this.refundService.createRefund(
      this.paymentId,
      this.refundAmount
    ).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.refundDetails = response?.data ?? response;

        if (this.refundDetails?.refund_id) {
          this.refundId = this.refundDetails.refund_id;
        }

        this.successMessage =
          response?.message ?? 'Refund request submitted successfully.';
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to submit the refund request.';
      }
    });
  }

  checkRefundStatus(): void {
    if (!this.refundId || this.refundId <= 0) {
      this.errorMessage = 'Please enter a valid Refund ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.refundDetails = null;

    this.refundService.getRefund(this.refundId).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.refundDetails = response?.data ?? response;
        this.successMessage = 'Refund details loaded successfully.';
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to retrieve refund status.';
      }
    });
  }
}
