
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RenewalService {

  private http = inject(HttpClient);

  private readonly baseUrl = 'https://localhost:7107';

  // GET /api/v1/renewals
  // Requires a list endpoint in your backend.
  getAllRenewals(): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl}/api/v1/renewals`
    );
  }

  // POST /api/v1/policies/{id}/renew
  renewPolicy(
    policyId: number,
    request: any = {}
  ): Observable<any> {
    return this.http.post<any>(
      `${this.baseUrl}/api/v1/policies/${policyId}/renew`,
      request
    );
  }

  // GET /api/v1/renewals/{id}
  getRenewalDetails(renewalId: number): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl}/api/v1/renewals/${renewalId}`
    );
  }
}
