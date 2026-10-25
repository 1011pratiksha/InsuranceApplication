
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// ==================== QUOTE MODELS ====================

export interface QuoteRequest {
  customerId: number;
  productId: number;
  policyTerm: number;
  sumInsured: number;
  premiumFrequencyId: number;
  riderIds?: number[];
  discountPercentage?: number;
}

export interface Quote {
  quoteId: number;
  quoteNumber: string;
  customerId: number;
  productId: number;
  policyTerm: number;
  sumInsured: number;
  premiumFrequencyId: number;
  quoteDate: string;
  expiryDate: string;
  quoteStatus: string;
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
  quoteId: number;
  productRate: number;
  riskPercentage: number;
  riderPremium: number;
  discountPercentage: number;
  gstPercentage: number;
}

export interface PremiumCalculation {
  calculationId?: number;
  quoteId: number;
  basePremium: number;
  riskLoading: number;
  riderPremium: number;
  discountAmount: number;
  gstAmount: number;
  finalPremium: number;
}

export interface QuoteHistory {
  historyId: number;
  quoteId: number;
  action: string;
  performedBy: number;
  remarks?: string;
  actionDate: string;
}

export interface ApiMessage {
  msg?: string;
  message?: string;
  [key: string]: unknown;
}

// ==================== QUOTE SERVICE ====================

@Injectable({
  providedIn: 'root'
})
export class QuoteGenerationService {

  // Update these URLs if your backend uses different ports.
  private readonly quoteUrl =
    'https://localhost:7227/api/v1/quotes';

  private readonly productUrl =
    'https://localhost:7225/api/v1/products';

  private readonly customerUrl =
    'http://localhost:5112/api/v1/customers';

  constructor(private http: HttpClient) {}

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
    dto: Partial<QuoteRequest>
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

  // ---------- PREMIUM APIs ----------

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

  // ---------- ADMIN APIs ----------

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

  convertQuote(id: number): Observable<ApiMessage> {
    return this.http.put<ApiMessage>(
      `${this.quoteUrl}/${id}/convert`,
      {}
    );
  }

  // ---------- QUOTE HISTORY ----------

  getHistory(id: number): Observable<QuoteHistory[]> {
    return this.http.get<QuoteHistory[]>(
      `https://localhost:7227/api/v1/quote-history/${id}`
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
}
