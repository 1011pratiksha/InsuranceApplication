
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  PremiumScheduleService,
  PremiumSchedule
} from '../../../services/premium-services/premium-schedule';

@Component({
  selector: 'app-admin-reminders',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-reminders.html'
})
export class AdminReminders {
  policyId = 1;
  installments: PremiumSchedule[] = [];
  loading = false;
  errorMessage = '';
  successMessage = '';
  filter = 'All';

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

        if (this.installments.length === 0) {
          this.successMessage = 'No installments found for this policy.';
        }
      },
      error: (error: any) => {
        this.loading = false;
        this.errorMessage =
          error?.error?.message ?? 'Unable to load installments.';
      }
    });
  }

  get filteredInstallments(): PremiumSchedule[] {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    return this.installments.filter(item => {
      const status = (item.status || '').toLowerCase();
      const dueDate = new Date(item.due_date);
      dueDate.setHours(0, 0, 0, 0);

      if (this.filter === 'Pending') {
        return status === 'pending';
      }

      if (this.filter === 'Overdue') {
        return status === 'overdue' ||
          (status === 'pending' && dueDate < today);
      }

      if (this.filter === 'Upcoming') {
        return status === 'pending' && dueDate >= today;
      }

      return true;
    });
  }

  getReminderCategory(item: PremiumSchedule): string {
    const status = (item.status || '').toLowerCase();
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const dueDate = new Date(item.due_date);
    dueDate.setHours(0, 0, 0, 0);

    if (status === 'paid' || status === 'cancelled') {
      return item.status;
    }

    if (status === 'overdue' || dueDate < today) {
      return 'Overdue';
    }

    if (status === 'pending') {
      return 'Upcoming / Pending';
    }

    return item.status;
  }
}
