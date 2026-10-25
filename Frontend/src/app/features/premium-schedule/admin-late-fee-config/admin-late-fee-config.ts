
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  PremiumScheduleService,
  PremiumSchedule
} from '../../../services/premium-services/premium-schedule';

@Component({
  selector: 'app-admin-late-fee-config',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-late-fee-config.html'
})
export class AdminLateFeeConfig {
  policyId = 1;
  installments: PremiumSchedule[] = [];
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private scheduleService: PremiumScheduleService) {}

  loadSchedule(): void {
    if (!this.policyId || this.policyId <= 0) {
      this.errorMessage = 'Please enter a valid Policy ID.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';
    this.installments = [];

    this.scheduleService.getPremiumSchedule(this.policyId).subscribe({
      next: (response: any) => {
        const result = response?.data ?? response;
        this.installments = Array.isArray(result) ? result : [];
        this.loading = false;

        if (this.installments.length === 0) {
          this.successMessage = 'No installments found for this policy.';
        }
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to load the premium schedule.';
      }
    });
  }
}