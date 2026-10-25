
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AssignedCustomers {

  constructor(private http: HttpClient) { }

  url1 = 'https://localhost:7014/api/v1/agents';

  // Current agent's assigned customers
  getCustomers(agentId: number) {
    return this.http.get(
      `${this.url1}/${agentId}/customers`,
      { withCredentials: true }
    );
  }

  // All available customers for dropdown
  getAllCustomers() {
    return this.http.get(
      `${this.url1}/available-customers`,
      { withCredentials: true }
    );
  }

  // All agents for dropdown
  getAllAgents() {
    return this.http.get(
      this.url1,
      { withCredentials: true }
    );
  }

  // Assign customer
  assignCustomer(agentId: number, data: any) {
    return this.http.post(
      `${this.url1}/${agentId}/customers`,
      data,
      { withCredentials: true }
    );
  }

  // Transfer customer
  transferCustomer(agentId: number, customerId: number, data: any) {
    return this.http.put(
      `${this.url1}/${agentId}/customers/${customerId}/transfer`,
      data,
      { withCredentials: true }
    );
  }
}
