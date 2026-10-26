import { Component, OnInit, inject } from '@angular/core';
import { DecimalPipe, NgClass } from '@angular/common';
import { DashboardService } from '../../services/dashboard';
import { AgentDashboard as AgentDashboardData } from '../../models/agent-dashboard';

@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [DecimalPipe, NgClass],
  templateUrl: './agent-dashboard.html',
  styleUrl: './agent-dashboard.scss'
})
export class AgentDashboard implements OnInit {

  private dashboardService = inject(DashboardService);

  loading = false;
  errorMessage = '';

  dashboardCards = [
    {
      title: 'My Customers',
      value: 0,
      icon: 'bi-people',
      currency: false
    },
    {
      title: 'Policies Sold',
      value: 0,
      icon: 'bi-file-earmark-check',
      currency: false
    },
    {
      title: 'Commission Pending',
      value: 0,
      icon: 'bi-hourglass-split',
      currency: true
    },
    {
      title: 'Commission Paid (YTD)',
      value: 0,
      icon: 'bi-wallet2',
      currency: true
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

  loadAgentDashboard(agentId: number): void {
    this.loading = true;
    this.errorMessage = '';

    this.dashboardService.getAgentDashboard(agentId).subscribe({
      next: (data: AgentDashboardData) => {
        this.dashboardCards[0].value = data.totalCustomers;
        this.dashboardCards[1].value = data.policiesSold;
        this.dashboardCards[2].value = data.commissionPending;
        this.dashboardCards[3].value = data.commissionPaidYTD;

        this.loading = false;
      },
      error: (error) => {
        console.error('Agent Dashboard API error:', error);
        this.errorMessage = 'Unable to load agent dashboard data.';
        this.loading = false;
      }
    });
  }
}

