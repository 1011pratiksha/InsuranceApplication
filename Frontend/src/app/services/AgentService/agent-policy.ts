import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AgentPolicy {

  private apiUrl = 'https://localhost:7014/api/v1/agents';

  constructor(private http: HttpClient) {}

  // Get policies assigned to an agent
  getAgentPolicies(agentId: number) {
    return this.http.get<any[]>(
      `${this.apiUrl}/${agentId}/policies`,
      { withCredentials: true }
    );
  }

  // Get details of a specific agent policy
  getAgentPolicyById(agentId: number, policyId: number) {
    return this.http.get<any>(
      `${this.apiUrl}/${agentId}/policies/${policyId}`,
      { withCredentials: true }
    );
  }


  getAllCustomers() {
  return this.http.get<any[]>(
    `${this.apiUrl.replace('/agents', '/agents/customers')}`,
    { withCredentials: true }
  );
}

getAllPolicies() {
  return this.http.get<any[]>(
    `${this.apiUrl.replace('/agents', '/agents/all-policies')}`,
    { withCredentials: true }
  );
}

  // Assign a policy to an agent
  createAgentPolicy(
    agentId: number,
    data: {
      policyId: number;
      customerId: number;
      saleDate: string | null;
    }
  ) {
    return this.http.post(
      `${this.apiUrl}/${agentId}/policies`,
      data,
      { withCredentials: true }
    );
  }
}