import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AdminDashboard } from '../models/admin-dashboard';
import { AgentDashboard } from '../models/agent-dashboard';
import { CustomerDashboard } from '../models/customer-dashboard';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private http = inject(HttpClient);

  // Replace the port when your Dashboard API HTTPS port is confirmed.
  private apiUrl = 'https://localhost:YOUR_DASHBOARD_API_PORT/api/Dashboard';

  // Admin Dashboard API
  getAdminDashboard(): Observable<AdminDashboard> {
    return this.http.get<AdminDashboard>(`${this.apiUrl}/Admin`);
  }

  // Agent Dashboard API
  getAgentDashboard(agentId: number): Observable<AgentDashboard> {
    return this.http.get<AgentDashboard>(
      `${this.apiUrl}/Agent/${agentId}`
    );
  }

  // Customer Dashboard API
  getCustomerDashboard(customerId: number): Observable<CustomerDashboard> {
    return this.http.get<CustomerDashboard>(
      `${this.apiUrl}/Customer/${customerId}`
    );
  }
}

