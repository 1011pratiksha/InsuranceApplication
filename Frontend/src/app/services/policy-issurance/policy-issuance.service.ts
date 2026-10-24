
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PolicyIssuanceService {

  private http = inject(HttpClient);

  private readonly baseUrl =
    'https://localhost:7083/api/v1/policies';

  getAllPolicies(): Observable<any> {
    return this.http.get<any>(this.baseUrl);
  }

  getPolicyById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  issuePolicy(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl, request);
  }

  generateDocument(id: number): Observable<any> {
    return this.http.post<any>(
      `${this.baseUrl}/${id}/document/generate`,
      {}
    );
  }

  downloadDocument(id: number): Observable<Blob> {
    return this.http.get(
      `${this.baseUrl}/${id}/document`,
      { responseType: 'blob' }
    );
  }

  activatePolicy(id: number): Observable<any> {
    return this.http.put<any>(
      `${this.baseUrl}/${id}/activate`, {}
    );
  }

  suspendPolicy(id: number): Observable<any> {
    return this.http.put<any>(
      `${this.baseUrl}/${id}/suspend`, {}
    );
  }

  cancelPolicy(id: number): Observable<any> {
    return this.http.put<any>(
      `${this.baseUrl}/${id}/cancel`, {}
    );
  }
}
