import { Component, OnInit, inject } from '@angular/core';
import { DecimalPipe, NgClass } from '@angular/common';
import { DashboardService } from '../../services/dashboard';
import { AdminDashboard as AdminDashboardData } from '../../models/admin-dashboard';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [DecimalPipe, NgClass],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.scss'
})
export class AdminDashboard implements OnInit {

  private dashboardService = inject(DashboardService);

  loading = false;
  errorMessage = '';

  dashboardCards = [
    {
      title: 'Pending agent approvals',
      value: 0,
      icon: 'bi-person-check'
    },
    {
      title: 'KYC queue',
      value: 0,
      icon: 'bi-shield-check'
    },
    {
      title: 'UW cases open',
      value: 0,
      icon: 'bi-file-earmark-text'
    },
    {
      title: 'Claims open',
      value: 0,
      icon: 'bi-clipboard2-pulse'
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
    // API fetching will be enabled when the backend is ready.
  }

  loadAdminDashboard(): void {
    this.loading = true;
    this.errorMessage = '';

    this.dashboardService.getAdminDashboard().subscribe({
      next: (data: AdminDashboardData) => {
        this.dashboardCards[0].value = data.pendingAgentApprovals;
        this.dashboardCards[1].value = data.kycQueue;
        this.dashboardCards[2].value = data.openUWCases;
        this.dashboardCards[3].value = data.openClaims;

        this.loading = false;
      },
      error: (error) => {
        console.error('Admin Dashboard API error:', error);
        this.errorMessage = 'Unable to load dashboard data.';
        this.loading = false;
      }
    });
  }
}

