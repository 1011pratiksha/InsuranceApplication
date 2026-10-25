
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface QuoteRequest {
  customerId: number;
  productId: number;
  policyTerm: number;
  sumInsured: number;
  premiumFrequencyId: number;
  riderIds: number[];
  discountPercentage: number;
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
  basePremium?: number;
  riskLoading?: number;
  riderPremium?: number;
  discountAmount?: number;
  gstAmount?: number;
  finalPremium?: number;
}

@Injectable({
  providedIn: 'root'
})
export class QuoteGenerationService {
  // Replace PORT with your Quote Generation API's actual HTTPS port.
  private readonly apiUrl =
    'https://localhost:PORT/api/v1/quotes';

  constructor(private http: HttpClient) {}

  getQuotes(): Observable<Quote[]> {
    return this.http.get<Quote[]>(this.apiUrl);
  }

  getQuoteById(id: number): Observable<Quote> {
    return this.http.get<Quote>(`${this.apiUrl}/${id}`);
  }

  generateQuote(request: QuoteRequest): Observable<Quote> {
    return this.http.post<Quote>(this.apiUrl, request);
  }

  calculatePremium(request: QuoteRequest): Observable<Quote> {
    return this.http.post<Quote>(
      `${this.apiUrl}/calculate-premium`,
      request
    );
  }

  approveQuote(id: number): Observable<Quote> {
    return this.http.put<Quote>(
      `${this.apiUrl}/${id}/approve`,
      {}
    );
  }

  rejectQuote(id: number): Observable<Quote> {
    return this.http.put<Quote>(
      `${this.apiUrl}/${id}/reject`,
      {}
    );
  }

  convertQuoteToPolicy(id: number): Observable<unknown> {
    return this.http.post(
      `${this.apiUrl}/${id}/convert`,
      {}
    );
  }
}
