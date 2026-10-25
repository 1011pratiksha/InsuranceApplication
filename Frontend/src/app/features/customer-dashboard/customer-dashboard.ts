import { Component, OnInit, inject } from '@angular/core';
import { DatePipe, DecimalPipe, NgClass } from '@angular/common';
import { DashboardService } from '../../services/dashboard';
import { CustomerDashboard as CustomerDashboardData } from '../../models/customer-dashboard';

@Component({
  selector: 'app-customer-dashboard',
  standalone: true,
  imports: [DatePipe, DecimalPipe, NgClass],
  templateUrl: './customer-dashboard.html',
  styleUrl: './customer-dashboard.scss'
})
export class CustomerDashboard implements OnInit {

  private dashboardService = inject(DashboardService);

  loading = false;
  errorMessage = '';

  dashboardCards = [
    {
      title: 'Active Policies',
      value: 0 as number | string | null,
      icon: 'bi-shield-check',
      type: 'number'
    },
    {
      title: 'Next Premium Due',
      value: null as number | string | null,
      icon: 'bi-calendar-check',
      type: 'date'
    },
    {
      title: 'Open Claims',
      value: 0 as number | string | null,
      icon: 'bi-file-earmark-text',
      type: 'number'
    },
    {
      title: 'KYC Status',
      value: 'Not Submitted' as number | string | null,
      icon: 'bi-person-vcard',
      type: 'text'
    }
  ];

  chartPeriods = [
    { label: 'Period 1', value: 5 },
    { label: 'Period 2', value: 8 },
    { label: 'Period 3', value: 6 },
    { label: 'Period 4', value: 11 },
    { label: 'Period 5', value: 9 },
    { label: 'Period 6', value: 13 },
    { label: 'Period 7', value: 10 }
  ];

  get maxChartValue(): number {
    return Math.max(
      ...this.chartPeriods.map(period => period.value),
      1
    );
  }

  ngOnInit(): void {
    // Enable API fetching when the backend is ready.
  }

  loadCustomerDashboard(customerId: number): void {
    this.loading = true;
    this.errorMessage = '';

    this.dashboardService.getCustomerDashboard(customerId).subscribe({
      next: (data: CustomerDashboardData) => {
        this.dashboardCards[0].value = data.activePolicies;
        this.dashboardCards[1].value = data.nextPremiumDue;
        this.dashboardCards[2].value = data.openClaims;
        this.dashboardCards[3].value = data.kycStatus;

        this.loading = false;
      },
      error: (error) => {
        console.error('Customer Dashboard API error:', error);
        this.errorMessage = 'Unable to load customer dashboard data.';
        this.loading = false;
      }
    });
  }
}

