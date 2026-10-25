import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RefundService {
  private readonly apiUrl = 'https://localhost:7184/api/v1/refunds';

  constructor(private http: HttpClient) {}

  createRefund(paymentId: number, amount: number): Observable<any> {
    return this.http.post<any>(this.apiUrl, {
      payment_id: paymentId,
      refund_amount: amount
    });
  }

  getRefund(refundId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${refundId}`);
  }

  approveRefund(refundId: number, approvedBy: number): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${refundId}/approve`, {
      approved_by: approvedBy
    });
  }
}