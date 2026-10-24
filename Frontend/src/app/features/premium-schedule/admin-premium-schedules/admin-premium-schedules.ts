
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  PremiumScheduleService,
  PremiumSchedule,
  PremiumScheduleRequest,
  UpdateInstallmentRequest
} from '../../../services/premium-services/premium-schedule';

@Component({
  selector: 'app-admin-premium-schedules',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-premium-schedules.html'
})
export class AdminPremiumSchedules {
  policyId = null as any;
  installments: PremiumSchedule[] = [];

  premiumFrequency = 'Monthly';
  policyTermInYears = null as any;
  annualPremium = null as any;
  startDate = new Date().toISOString().substring(0, 10);

  loading = false;
  generating = false;
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

    this.scheduleService.getPremiumSchedule(this.policyId).subscribe({
      next: (response: any) => {
        const result = response?.data ?? response;
        this.installments = Array.isArray(result) ? result : [];
        this.loading = false;
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to load the premium schedule.';
      }
    });
  }

  generateSchedule(): void {
    if (
      !this.policyId ||
      this.policyId <= 0 ||
      this.policyTermInYears <= 0 ||
      this.annualPremium <= 0 ||
      !this.startDate
    ) {
      this.errorMessage = 'Enter valid policy details before generating the schedule.';
      return;
    }

    const request: PremiumScheduleRequest = {
      policyId: this.policyId,
      premiumFrequency: this.premiumFrequency,
      policyTermInYears: this.policyTermInYears,
      annualPremium: this.annualPremium,
      startDate: this.startDate
    };

    this.generating = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.scheduleService.generateSchedule(request).subscribe({
      next: (response: any) => {
        this.generating = false;
        this.successMessage =
          response?.message ?? 'Premium schedule generation completed.';
        this.loadSchedule();
      },
      error: (error: any) => {
        this.generating = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to generate the premium schedule.';
      }
    });
  }

  updateInstallment(installment: PremiumSchedule): void {
    const request: UpdateInstallmentRequest = {
      Status: installment.status,
      DueDate: installment.due_date || null
    };

    this.errorMessage = '';
    this.successMessage = '';

    this.scheduleService.updateInstallment(
      installment.schedule_id,
      request
    ).subscribe({
      next: () => {
        this.successMessage =
          `Installment ${installment.schedule_id} updated successfully.`;
        this.loadSchedule();
      },
      error: (error: any) => {
        this.errorMessage =
          error?.error?.message ?? 'Unable to update the installment.';
      }
    });
  }
}
