
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CreatePaymentResponse {
  message: string;
  payment_id: number;
  razorpay_order_id: string;
  razorpay_key_id: string;
  amount: number;
  currency: string;
}

export interface VerifyPaymentRequest {
  payment_id: number;
  razorpay_order_id: string;
  razorpay_payment_id: string;
  razorpay_signature: string;
}

export interface PaymentRecord {
  payment_id: number;
  schedule_id: number;
  policy_id: number;
  payment_date: string;
  amount_paid: number;
  payment_mode: string;
  transaction_reference: string;
  payment_status: string;
}

declare global {
  interface Window {
    Razorpay: new (options: Record<string, unknown>) => {
      open(): void;
      on(event: string, callback: (response: any) => void): void;
    };
  }
}

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private readonly apiUrl = 'https://localhost:7184/api/v1';

  constructor(private http: HttpClient) {}

  createPayment(scheduleId: number) {
    return this.http.post<CreatePaymentResponse>(
      `${this.apiUrl}/payments`,
      {
        schedule_id: scheduleId,
        payment_mode: 'Razorpay'
      }
    );
  }

  verifyPayment(request: VerifyPaymentRequest) {
    return this.http.post<any>(
      `${this.apiUrl}/payments/verify`,
      request
    );
  }

  getPayment(paymentId: number) {
    return this.http.get<PaymentRecord>(
      `${this.apiUrl}/payments/${paymentId}`
    );
  }

  getPaymentHistory(policyId: number) {
    return this.http.get<PaymentRecord[]>(
      `${this.apiUrl}/payments/history/${policyId}`
    );
  }

  downloadReceipt(paymentId: number) {
    return this.http.get(
      `${this.apiUrl}/payments/${paymentId}/receipt`,
      { responseType: 'blob' }
    );
  }

  regenerateReceipt(paymentId: number) {
    return this.http.post<any>(
      `${this.apiUrl}/payments/${paymentId}/receipt/regenerate`,
      {}
    );
  }
  createRefund(paymentId: number, amount: number) {
  return this.http.post<any>(
    `${this.apiUrl}/refunds`,
    {
      payment_id: paymentId,
      refund_amount: amount
    }
  );
}

getRefund(refundId: number) {
  return this.http.get<any>(
    `${this.apiUrl}/refunds/${refundId}`
  );
}
}