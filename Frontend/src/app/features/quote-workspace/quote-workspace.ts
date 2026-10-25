
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';

import {
  QuoteGenerationService,
  Quote,
  QuoteRequest,
  Customer,
  Product,
  PremiumCalculation,
  PremiumRequest,
  ApiMessage
} from '../quote-generation/quote-generation';

@Component({
  selector: 'app-quote-workspace',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './quote-workspace.html',
  styleUrl: './quote-workspace.scss'
})
export class QuoteWorkspace implements OnInit {
  role = 'agent';
  page = 'new';
  quoteId: number | null = null;

  quotes: Quote[] = [];
  customers: Customer[] = [];
  products: Product[] = [];

  selectedQuote: Quote | null = null;
  premium: PremiumCalculation | null = null;

  loading = false;
  saving = false;
  errorMessage = '';
  successMessage = '';

  form: QuoteRequest = {
    customerId: 0,
    productId: 0,
    policyTerm: 1,
    sumInsured: 100000,
    premiumFrequencyId: 1,
    riderIds: [],
    discountPercentage: 0
  };

  premiumInputs = {
    productRate: 5,
    riskPercentage: 0,
    riderPremium: 0,
    discountPercentage: 0,
    gstPercentage: 18
  };

  constructor(
    private api: QuoteGenerationService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.role = data['role'] ?? 'agent';
      this.page = data['page'] ?? 'new';
      this.loadReferenceData();
    });

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      this.quoteId = id ? Number(id) : null;

      if (this.quoteId) {
        this.loadQuoteDetails(this.quoteId);
      }
    });
  }

  get isAdmin(): boolean {
    return this.role === 'admin';
  }

  get isAgent(): boolean {
    return this.role === 'agent';
  }

  get isCustomer(): boolean {
    return this.role === 'customer';
  }

  get pageTitle(): string {
    switch (this.page) {
      case 'new':
        return 'Create New Quote';
      case 'result':
        return 'Premium Calculation';
      case 'review':
        return 'Review Quote';
      case 'config':
        return 'Quote Configuration';
      case 'accept':
        return 'Your Quote';
      case 'detail':
        return 'Quote Details';
      default:
        return 'Quote Management';
    }
  }

  loadReferenceData(): void {
    this.loading = true;
    this.errorMessage = '';

    this.api.getCustomers().subscribe({
      next: (data: Customer[]) => {
        this.customers = Array.isArray(data) ? data : [];
      },
      error: () => {
        this.customers = [];
      }
    });

    this.api.getProducts().subscribe({
      next: (data: Product[]) => {
        this.products = Array.isArray(data) ? data : [];
      },
      error: () => {
        this.products = [];
      }
    });

    this.loadQuotes();
  }

  loadQuotes(): void {
    this.loading = true;

    this.api.getQuotes().subscribe({
      next: (data: Quote[]) => {
        this.quotes = Array.isArray(data) ? data : [];
        this.loading = false;

        if (this.quoteId !== null) {
          this.selectedQuote =
            this.quotes.find(q => q.quoteId === this.quoteId) ?? null;
        }
      },
      error: (err: unknown) => {
        this.loading = false;
        this.errorMessage = this.getErrorMessage(
          err,
          'Unable to load quotes. Check that the backend is running.'
        );
      }
    });
  }

  loadQuoteDetails(id: number): void {
    this.api.getQuote(id).subscribe({
      next: (quote: Quote) => {
        this.selectedQuote = quote;

        this.api.getPremium(id).subscribe({
          next: (result: PremiumCalculation) => {
            this.premium = result;
          },
          error: () => {
            this.premium = null;
          }
        });
      },
      error: (err: unknown) => {
        this.selectedQuote = null;
        this.errorMessage = this.getErrorMessage(
          err,
          'Unable to load quote details.'
        );
      }
    });
  }

  createQuote(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (
      !this.form.customerId ||
      !this.form.productId ||
      this.form.policyTerm < 1 ||
      this.form.sumInsured <= 0 ||
      !this.form.premiumFrequencyId
    ) {
      this.errorMessage = 'Please enter valid values in all required fields.';
      return;
    }

    this.saving = true;

    this.api.createQuote(this.form).subscribe({
      next: (response: ApiMessage) => {
        this.saving = false;
        this.successMessage =
          response.message ?? response.msg ?? 'Quote created successfully.';

        this.router.navigate(['/agent/quotes']);
      },
      error: (err: unknown) => {
        this.saving = false;
        this.errorMessage = this.getErrorMessage(
          err,
          'Unable to create quote.'
        );
      }
    });
  }

  calculatePremium(): void {
    if (!this.selectedQuote) {
      this.errorMessage = 'Select a quote before calculating the premium.';
      return;
    }

    const request: PremiumRequest = {
      quoteId: this.selectedQuote.quoteId,
      productRate: this.premiumInputs.productRate,
      riskPercentage: this.premiumInputs.riskPercentage,
      riderPremium: this.premiumInputs.riderPremium,
      discountPercentage: this.premiumInputs.discountPercentage,
      gstPercentage: this.premiumInputs.gstPercentage
    };

    this.saving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.api.calculatePremium(request).subscribe({
      next: (response: ApiMessage) => {
        this.saving = false;
        this.successMessage =
          response.message ?? response.msg ?? 'Premium calculation submitted.';

        this.api.getPremium(this.selectedQuote!.quoteId).subscribe({
          next: (result: PremiumCalculation) => {
            this.premium = result;
          },
          error: () => {
            this.premium = null;
          }
        });
      },
      error: (err: unknown) => {
        this.saving = false;
        this.errorMessage = this.getErrorMessage(
          err,
          'Premium calculation failed.'
        );
      }
    });
  }

  approveQuote(quote: Quote): void {
    this.runQuoteAction(
      () => this.api.approveQuote(quote.quoteId),
      'Quote approved successfully.'
    );
  }

  rejectQuote(quote: Quote): void {
    this.runQuoteAction(
      () => this.api.rejectQuote(quote.quoteId),
      'Quote rejected successfully.'
    );
  }

  convertQuote(quote: Quote): void {
    this.runQuoteAction(
      () => this.api.convertQuote(quote.quoteId),
      'Quote converted successfully.'
    );
  }

  cancelQuote(quote: Quote): void {
    if (!confirm(`Cancel quote ${quote.quoteNumber}?`)) {
      return;
    }

    this.runQuoteAction(
      () => this.api.cancelQuote(quote.quoteId),
      'Quote cancelled successfully.'
    );
  }

  private runQuoteAction(
    action: () => Observable<ApiMessage>,
    successText: string
  ): void {
    this.errorMessage = '';
    this.successMessage = '';
    this.saving = true;

    action().subscribe({
      next: (response: ApiMessage) => {
        this.saving = false;
        this.successMessage =
          response.message ?? response.msg ?? successText;
        this.loadQuotes();

        if (this.quoteId !== null) {
          this.loadQuoteDetails(this.quoteId);
        }
      },
      error: (err: unknown) => {
        this.saving = false;
        this.errorMessage = this.getErrorMessage(
          err,
          'The requested action could not be completed.'
        );
      }
    });
  }

  customerIdOf(customer: Customer): number {
    return Number(customer.customerId ?? customer.CustomerId ?? 0);
  }

  customerName(customerId: number): string {
    const customer = this.customers.find(
      c => this.customerIdOf(c) === Number(customerId)
    );

    if (!customer) {
      return `Customer #${customerId}`;
    }

    const name = [
      customer.firstName ?? customer.FirstName,
      customer.middleName ?? customer.MiddleName,
      customer.lastName ?? customer.LastName
    ].filter(Boolean).join(' ');

    return name ||
      customer.customerCode ||
      customer.CustomerCode ||
      `Customer #${customerId}`;
  }

  productName(productId: number): string {
    const product = this.products.find(
      p => Number(p.product_id) === Number(productId)
    );

    return product?.product_name ?? `Product #${productId}`;
  }

  openQuote(quote: Quote): void {
    this.router.navigate([
      `/${this.role}/quote-details`,
      quote.quoteId
    ]);
  }

  reviewQuote(quote: Quote): void {
    this.router.navigate([
      '/admin/quote-review',
      quote.quoteId
    ]);
  }

  goToCreateQuote(): void {
    this.router.navigate(['/agent/quote-new']);
  }

  statusClass(status: string | null | undefined): string {
    const value = (status ?? '').toLowerCase();

    if (
      value.includes('approv') ||
      value.includes('convert') ||
      value.includes('accept')
    ) {
      return 'status-success';
    }

    if (
      value.includes('reject') ||
      value.includes('cancel') ||
      value.includes('expir')
    ) {
      return 'status-danger';
    }

    if (
      value.includes('pending') ||
      value.includes('draft') ||
      value.includes('review')
    ) {
      return 'status-warning';
    }

    return 'status-neutral';
  }

  trackByQuoteId(index: number, quote: Quote): number {
    return quote.quoteId;
  }

  private getErrorMessage(error: unknown, fallback: string): string {
    if (typeof error === 'object' && error !== null && 'error' in error) {
      const body = (error as {
        error?: { message?: string; msg?: string };
      }).error;

      if (body?.message) return body.message;
      if (body?.msg) return body.msg;
    }

    return fallback;
  }
}
