
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface UnderwritingCase {
  underwriting_id: number;
  quote_id: number;
  customer_id: number;
  product_id: number;
  underwriting_date: string;
  status: string;
}

export interface CreateUnderwriting {
  quote_id: number;
  customer_id: number;
  product_id: number;
}

export interface MedicalDetails {
  medical_id?: number;
  underwriting_id?: number;
  height: number;
  weight: number;
  bmi?: number;
  blood_pressure?: string;
  diabetes: boolean;
  heart_disease: boolean;
  cancer_history: boolean;
  smoking: boolean;
  alcohol: boolean;
  medical_history?: string;
  medical_report_path?: string;
}

export interface FinancialDetails {
  financial_id?: number;
  underwriting_id?: number;
  annual_income: number;
  monthly_income: number;
  employer_name: string;
  occupation: string;
  existing_insurance: number;
  loan_liability: number;
}

export interface RiskScore {
  risk_id?: number;
  underwriting_id?: number;
  age_score: number;
  medical_score: number;
  occupation_score: number;
  lifestyle_score: number;
  financial_score: number;
  total_score?: number;
  risk_category?: string;
}

export interface UnderwritingDecision {
  decision: string;
  loading_percentage: number;
  exclusion_details?: string;
  rejection_reason?: string;
  approved_by?: number;
}

export interface PremiumLoading {
  underwriting_id: number;
  risk_category: string;
  loading_percentage: number;
}

export interface AuditLog {
  audit_id: number;
  underwriting_id: number;
  action: string;
  performed_by?: number;
  remarks?: string;
  action_date: string;
}

export interface ApiMessage {
  msg?: string;
  message?: string;
  underwriting_id?: number;
  [key: string]: unknown;
}

@Injectable({
  providedIn: 'root'
})
export class UnderwritingService {
  // Confirm this port against the Underwriting backend launchSettings.json.
  private readonly baseUrl = 'https://localhost:7101/api/v1/underwriting';

  constructor(private readonly http: HttpClient) {}

  getCases(): Observable<UnderwritingCase[]> {
    return this.http.get<UnderwritingCase[]>(this.baseUrl);
  }

  getCase(id: number): Observable<UnderwritingCase> {
    return this.http.get<UnderwritingCase>(`${this.baseUrl}/${id}`);
  }

  createCase(dto: CreateUnderwriting): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(this.baseUrl, dto);
  }

  updateCase(
    id: number,
    dto: CreateUnderwriting
  ): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(`${this.baseUrl}/${id}`, dto);
  }

  addMedical(
    id: number,
    dto: MedicalDetails
  ): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(
      `${this.baseUrl}/${id}/medical`,
      dto
    );
  }

  updateMedical(
    id: number,
    dto: MedicalDetails
  ): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.baseUrl}/${id}/medical`,
      dto
    );
  }

  getMedical(id: number): Observable<MedicalDetails> {
    return this.http.get<MedicalDetails>(
      `${this.baseUrl}/${id}/medical`
    );
  }

  addFinancial(
    id: number,
    dto: FinancialDetails
  ): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(
      `${this.baseUrl}/${id}/financial`,
      dto
    );
  }

  updateFinancial(
    id: number,
    dto: FinancialDetails
  ): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.baseUrl}/${id}/financial`,
      dto
    );
  }

  getFinancial(id: number): Observable<FinancialDetails> {
    return this.http.get<FinancialDetails>(
      `${this.baseUrl}/${id}/financial`
    );
  }

  calculateRisk(
    id: number,
    dto: RiskScore
  ): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(
      `${this.baseUrl}/${id}/risk-score`,
      dto
    );
  }

  getRisk(id: number): Observable<RiskScore> {
    return this.http.get<RiskScore>(
      `${this.baseUrl}/${id}/risk-score`
    );
  }

  getPremiumLoading(id: number): Observable<PremiumLoading> {
    return this.http.get<PremiumLoading>(
      `${this.baseUrl}/${id}/premium-loading`
    );
  }

  approve(
    id: number,
    dto: UnderwritingDecision
  ): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.baseUrl}/${id}/approve`,
      dto
    );
  }

  reject(
    id: number,
    dto: UnderwritingDecision
  ): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.baseUrl}/${id}/reject`,
      dto
    );
  }

  applyLoading(
    id: number,
    dto: UnderwritingDecision
  ): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.baseUrl}/${id}/loading`,
      dto
    );
  }

  getDecision(id: number): Observable<UnderwritingDecision> {
    return this.http.get<UnderwritingDecision>(
      `${this.baseUrl}/${id}/decision`
    );
  }

  getAuditLog(id: number): Observable<AuditLog[]> {
    return this.http.get<AuditLog[]>(
      `${this.baseUrl}/${id}/audit-log`
    );
  }
}
