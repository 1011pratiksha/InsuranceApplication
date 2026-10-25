
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AgentCommission {

  private apiUrl = 'https://localhost:7014/api/v1/commissions';

  constructor(private http: HttpClient) {}

  // Get all commissions with optional filters
  getAllCommissions(
    status?: string,
    agentId?: number,
    fromDate?: string,
    toDate?: string
  ) {
    let params = new HttpParams();

    if (status) {
      params = params.set('status', status);
    }

    if (agentId != null && agentId > 0) {
      params = params.set('agentId', agentId);
    }

    if (fromDate) {
      params = params.set('fromDate', fromDate);
    }

    if (toDate) {
      params = params.set('toDate', toDate);
    }

    return this.http.get<any[]>(this.apiUrl, { params });
  }

  // Calculate commission
  calculateCommission(data: {
    agentId: number;
    policyId: number;
  }) {
    return this.http.post(`${this.apiUrl}/calculate`, data);
  }

  // Get commissions by Agent ID
  getCommissionsByAgentId(agentId: number) {
    return this.http.get<any[]>(
      `https://localhost:7014/api/v1/agents/${agentId}/commissions`
    );
  }

  // Update commission status
  updateCommissionStatus(
    commissionId: number,
    status: string
  ) {
    return this.http.patch(
      `${this.apiUrl}/${commissionId}/status`,
      { status }
    );
  }



  
  // Get all policies for dropdown
  getAllPolicies() {
    return this.http.get<any[]>(
      `${this.apiUrl}/policies`
    );
  }


  getAllAgents() {
  return this.http.get<any[]>(
    'https://localhost:7014/api/v1/agents'
  );
}

}
