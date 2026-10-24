import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface PremiumSchedule {
  schedule_id: number;
  policy_id: number;
  installment_no: number;
  due_date: string;
  premium_amount: number;
  late_fee: number;
  status: string;
}

export interface PremiumScheduleRequest {
  policyId: number;
  premiumFrequency: string;
  policyTermInYears: number;
  annualPremium: number;
  startDate: string;
}

export interface UpdateInstallmentRequest {
  Status: string;
  DueDate?: string | null;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  error: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class PremiumScheduleService {
  private apiUrl = 'https://localhost:7184/api/v1/premium-schedules';

  constructor(private http: HttpClient) {}

  generateSchedule(
    request: PremiumScheduleRequest
  ): Observable<ApiResponse<PremiumSchedule[]>> {
    return this.http.post<ApiResponse<PremiumSchedule[]>>(
      this.apiUrl,
      request
    );
  }

  getPremiumSchedule(
    policyId: number
  ): Observable<ApiResponse<PremiumSchedule[]>> {
    return this.http.get<ApiResponse<PremiumSchedule[]>>(
      `${this.apiUrl}/${policyId}`
    );
  }

  updateInstallment(
    scheduleId: number,
    request: UpdateInstallmentRequest
  ): Observable<ApiResponse<PremiumSchedule>> {
    return this.http.put<ApiResponse<PremiumSchedule>>(
      `${this.apiUrl}/${scheduleId}`,
      request
    );
  }
}
