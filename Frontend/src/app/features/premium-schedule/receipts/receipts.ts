
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PaymentService } from '../../../services/premium-services/payment';

@Component({
  selector: 'app-receipts',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './receipts.html'
})
export class Receipts {
  paymentId: number | null = null;
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private paymentService: PaymentService) {}

  downloadReceipt(): void {
    if (!this.paymentId || this.paymentId <= 0) {
      this.errorMessage = 'Please enter a valid Payment ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.paymentService.downloadReceipt(this.paymentId).subscribe({
      next: (response: any) => {
        this.loading = false;

        if (response instanceof Blob) {
          this.saveFile(response, `receipt-${this.paymentId}.pdf`);
        } else if (response instanceof ArrayBuffer) {
          this.saveFile(
            new Blob([response], { type: 'application/pdf' }),
            `receipt-${this.paymentId}.pdf`
          );
        } else {
          this.errorMessage =
            'The receipt response was not a PDF file. Check the downloadReceipt method in payment.ts.';
        }
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to download the receipt.';
      }
    });
  }

  regenerateReceipt(): void {
    if (!this.paymentId || this.paymentId <= 0) {
      this.errorMessage = 'Please enter a valid Payment ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.paymentService.regenerateReceipt(this.paymentId).subscribe({
      next: (response: any) => {
        this.loading = false;
        this.successMessage =
          response?.message ??
          response?.data?.message ??
          'Receipt regeneration request completed. You can now download the receipt.';

        this.downloadReceipt();
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to regenerate the receipt.';
      }
    });
  }

  private saveFile(file: Blob, fileName: string): void {
    const fileUrl = URL.createObjectURL(file);
    const link = document.createElement('a');

    link.href = fileUrl;
    link.download = fileName;
    link.click();

    URL.revokeObjectURL(fileUrl);
    this.successMessage = 'Receipt downloaded successfully.';
  }
}