
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RefundService } from '../../../services/premium-services/refund';

@Component({
  selector: 'app-admin-refunds',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-refunds.html'
})
export class AdminRefunds {
  refundId: number | null = null;
  approvedBy: number | null = null;
  refund: any = null;

  loading = false;
  approving = false;
  errorMessage = '';
  successMessage = '';

  constructor(private refundService: RefundService) {}

  loadRefund(): void {
    if (!this.refundId || this.refundId <= 0) {
      this.errorMessage = 'Please enter a valid Refund ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.refund = null;

    this.refundService.getRefund(this.refundId).subscribe({
      next: (response: any) => {
        this.refund = response?.data ?? response;
        this.loading = false;
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to retrieve refund details.';
      }
    });
  }

  approveRefund(): void {
    if (!this.refundId || this.refundId <= 0) {
      this.errorMessage = 'Please load a valid Refund ID first.';
      return;
    }

    if (!this.approvedBy || this.approvedBy <= 0) {
      this.errorMessage = 'Please enter a valid approver ID.';
      return;
    }

    if (
      !this.refund ||
      String(this.refund.refund_status ?? '').toLowerCase() !== 'pending'
    ) {
      this.errorMessage = 'Only a refund with Pending status can be approved.';
      return;
    }

    this.approving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.refundService.approveRefund(
      this.refundId,
      this.approvedBy
    ).subscribe({
      next: (response: any) => {
        this.approving = false;
        this.successMessage =
          response?.message ?? 'Refund approval request completed.';
        this.loadRefund();
      },
      error: (error: any) => {
        this.approving = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to approve the refund.';
      }
    });
  }
}
