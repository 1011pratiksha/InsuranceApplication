
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RefundService } from '../../../services/premium-services/refund';

@Component({
  selector: 'app-refund-status',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './refund-status.html',
  styleUrl: './refund-status.scss'
})
export class RefundStatus {
  paymentId: number | null = null;
  refundAmount: number | null = null;
  refundId: number | null = null;

  loading = false;
  successMessage = '';
  errorMessage = '';
  refundDetails: any = null;

  constructor(private refundService: RefundService) {}

  submitRefund(): void {
    this.successMessage = '';
    this.errorMessage = '';
    this.refundDetails = null;

    if (!this.paymentId || this.paymentId <= 0) {
      this.errorMessage = 'Please enter a valid payment ID.';
      return;
    }

    if (
      this.refundAmount === null ||
      this.refundAmount <= 0
    ) {
      this.errorMessage = 'Please enter a refund amount greater than zero.';
      return;
    }

    this.loading = true;

    this.refundService.createRefund(
      this.paymentId,
      this.refundAmount
    ).subscribe({
      next: (response) => {
        this.successMessage =
          response?.message || 'Refund request submitted successfully.';

        if (response?.refund_id) {
          this.refundId = response.refund_id;
          this.refundDetails = response;
        }

        this.loading = false;
      },
      error: (error) => {
        this.errorMessage =
          error?.error?.message ||
          error?.error?.title ||
          error?.message ||
          'Failed to submit refund request.';

        this.loading = false;
      }
    });
  }

  checkRefundStatus(): void {
    this.successMessage = '';
    this.errorMessage = '';
    this.refundDetails = null;

    if (!this.refundId || this.refundId <= 0) {
      this.errorMessage = 'Please enter a valid refund ID.';
      return;
    }

    this.loading = true;

    this.refundService.getRefund(this.refundId).subscribe({
      next: (response) => {
        this.refundDetails = response;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage =
          error?.error?.message ||
          error?.error?.title ||
          error?.message ||
          'Failed to retrieve refund status.';

        this.loading = false;
      }
    });
  }
}
