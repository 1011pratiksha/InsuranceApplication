
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// ==================== QUOTE MODELS ====================

export interface QuoteRequest {
  customer_id: number;
  product_id: number;
  premium_frequency_id: number;
  policy_term: number;
  sum_insured: number;
}

export interface UpdateQuoteRequest {
  premium_frequency_id: number;
  policy_term: number;
  sum_insured: number;
}

export interface Quote {
  quote_id: number;
  quote_number: string;
  customer_id: number;
  product_id: number;
  premium_frequency_id: number;
  policy_term: number;
  sum_insured: number;
  quote_date: string;
  expiry_date: string;
  quote_status: string;
}

export interface Customer {
  customerId?: number;
  CustomerId?: number;
  customerCode?: string;
  CustomerCode?: string;
  firstName?: string;
  FirstName?: string;
  middleName?: string;
  MiddleName?: string;
  lastName?: string;
  LastName?: string;
  email?: string;
  Email?: string;
  customerStatus?: string;
  CustomerStatus?: string;
}

export interface Product {
  product_id: number;
  category_id: number;
  product_code: string;
  product_name: string;
  product_type: string;
  description?: string | null;
  minimum_age?: number | null;
  maximum_age?: number | null;
  minimum_sum_insured?: number | null;
  maximum_sum_insured?: number | null;
  status: string;
}

export interface PremiumRequest {
  quote_id: number;
  product_rate: number;
  risk_percentage: number;
  rider_premium: number;
  discount_percentage: number;
  gst_percentage: number;
}

export interface PremiumCalculation {
  calculation_id: number;
  quote_id: number;
  base_premium: number;
  risk_loading: number;
  rider_premium: number;
  discount_amount: number;
  gst_amount: number;
  final_premium: number;
}

export interface QuoteHistory {
  history_id: number;
  quote_id: number;
  action: string;
  performed_by: number;
  remarks?: string;
  action_date: string;
}

export interface ApiMessage {
  msg?: string;
  message?: string;
  [key: string]: unknown;
}

// ==================== QUOTE GENERATION SERVICE ====================

@Injectable({
  providedIn: 'root'
})
export class QuoteGenerationService {

  private readonly quoteUrl =
    'https://localhost:7227/api/v1/quotes';

  private readonly quoteHistoryUrl =
    'https://localhost:7227/api/v1/quote-history';

  private readonly productUrl =
    'https://localhost:7225/api/v1/products';

  private readonly customerUrl =
    'http://localhost:5112/api/v1/customers';

  constructor(private readonly http: HttpClient) {}

  // ---------- QUOTE APIs ----------

  getQuotes(): Observable<Quote[]> {
    return this.http.get<Quote[]>(this.quoteUrl);
  }

  getQuote(id: number): Observable<Quote> {
    return this.http.get<Quote>(`${this.quoteUrl}/${id}`);
  }

  createQuote(dto: QuoteRequest): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(this.quoteUrl, dto);
  }

  updateQuote(
    id: number,
    dto: UpdateQuoteRequest
  ): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.quoteUrl}/${id}`,
      dto
    );
  }

  cancelQuote(id: number): Observable<ApiMessage> {
    return this.http.delete<ApiMessage>(
      `${this.quoteUrl}/${id}`
    );
  }

  // ---------- PREMIUM CALCULATION APIs ----------

  calculatePremium(
    dto: PremiumRequest
  ): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(
      `${this.quoteUrl}/calculate-premium`,
      dto
    );
  }

  getPremium(id: number): Observable<PremiumCalculation> {
    return this.http.get<PremiumCalculation>(
      `${this.quoteUrl}/${id}/premium`
    );
  }

  // ---------- QUOTE APPROVAL APIs ----------

  approveQuote(id: number): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.quoteUrl}/${id}/approve`,
      {}
    );
  }

  rejectQuote(id: number): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.quoteUrl}/${id}/reject`,
      {}
    );
  }

  // ---------- QUOTE LIFECYCLE APIs ----------

  convertQuote(id: number): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.quoteUrl}/${id}/convert`,
      {}
    );
  }

  // ---------- QUOTE HISTORY APIs ----------

  getHistory(id: number): Observable<QuoteHistory[]> {
    return this.http.get<QuoteHistory[]>(
      `${this.quoteHistoryUrl}/${id}`
    );
  }

  getAllHistory(): Observable<QuoteHistory[]> {
    return this.http.get<QuoteHistory[]>(
      this.quoteHistoryUrl
    );
  }

  // ---------- PRODUCT MANAGEMENT API ----------

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productUrl);
  }

  // ---------- CUSTOMER REGISTRATION API ----------

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.customerUrl);
  }

  // ---------- UNDERWRITING-RELATED QUOTE APIs ----------

  sendForUnderwriting(
    quoteId: number,
    customerId: number
  ): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(
      `${this.quoteUrl}/${quoteId}/send-for-underwriting`,
      null,
      { params: { customerId } }
    );
  }

  calculateRiskScore(
    quoteId: number,
    dto: Record<string, unknown>
  ): Observable<ApiMessage> {
    return this.http.post<ApiMessage>(
      `${this.quoteUrl}/${quoteId}/risk-score`,
      dto
    );
  }

  getUnderwritingCase(id: number): Observable<unknown> {
    return this.http.get<unknown>(
      `${this.quoteUrl}/${id}/underwriting`
    );
  }

  getRiskScore(id: number): Observable<unknown> {
    return this.http.get<unknown>(
      `${this.quoteUrl}/${id}/risk-score`
    );
  }

  getUnderwritingDecision(id: number): Observable<unknown> {
    return this.http.get<unknown>(
      `${this.quoteUrl}/${id}/underwriting-decision`
    );
  }
}
