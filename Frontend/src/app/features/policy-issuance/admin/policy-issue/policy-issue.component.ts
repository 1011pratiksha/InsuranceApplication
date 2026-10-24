
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { PolicyIssuanceService } from '../../../../services/policy-issurance/policy-issuance.service';

@Component({
  selector: 'app-policy-issue',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './policy-issue.component.html',
  styleUrl: './policy-issue.component.css'
})
export class PolicyIssueComponent implements OnInit {

  private service = inject(PolicyIssuanceService);
  private router = inject(Router);

  quoteId = 1;
  policy: any = null;

  loading = false;
  issuing = false;

  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    this.loadPolicies();
  }

  get policyNumber(): string {
    return this.policy?.policyNumber
      ?? this.policy?.PolicyNumber
      ?? '';
  }

  get customerName(): string {
    return this.policy?.customerName
      ?? this.policy?.CustomerName
      ?? this.policy?.customer?.name
      ?? this.policy?.Customer?.Name
      ?? '';
  }

  get productName(): string {
    return this.policy?.productName
      ?? this.policy?.ProductName
      ?? this.policy?.product?.productName
      ?? this.policy?.Product?.ProductName
      ?? '';
  }

  get startDate(): string {
    const value = this.policy?.startDate
      ?? this.policy?.StartDate
      ?? this.policy?.policyStartDate
      ?? this.policy?.PolicyStartDate;

    if (!value) {
      return '';
    }

    const date = new Date(value);

    return Number.isNaN(date.getTime())
      ? String(value)
      : date.toLocaleDateString('en-GB', {
          day: '2-digit',
          month: 'short',
          year: 'numeric'
        });
  }

  get nomineeMapped(): boolean {
    return Boolean(
      this.policy?.nomineeMapped
      ?? this.policy?.NomineeMapped
      ?? this.policy?.nomineeId
      ?? this.policy?.NomineeId
    );
  }

  get riders(): string {
    const value = this.policy?.riders
      ?? this.policy?.Riders
      ?? this.policy?.riderName
      ?? this.policy?.RiderName;

    if (Array.isArray(value)) {
      return value
        .map((r: any) => r?.riderName ?? r?.RiderName ?? r?.name ?? r)
        .join(', ') || 'None selected';
    }

    return value ? String(value) : 'None selected';
  }

  get sumInsured(): number {
    return Number(
      this.policy?.sumInsured
      ?? this.policy?.SumInsured
      ?? 0
    );
  }

  get premium(): number {
    return Number(
      this.policy?.premium
      ?? this.policy?.Premium
      ?? this.policy?.totalPremium
      ?? this.policy?.TotalPremium
      ?? 0
    );
  }

  get policyStatus(): string {
    return String(
      this.policy?.policyStatus
      ?? this.policy?.PolicyStatus
      ?? this.policy?.status
      ?? this.policy?.Status
      ?? 'Pending'
    );
  }

  get documentsGenerated(): boolean {
    const status = String(
      this.policy?.documentStatus
      ?? this.policy?.DocumentStatus
      ?? ''
    ).toLowerCase();

    return Boolean(
      this.policy?.documentsGenerated
      ?? this.policy?.DocumentsGenerated
      ?? this.policy?.documentId
      ?? this.policy?.DocumentId
    ) || ['generated', 'ready', 'completed'].includes(status);
  }

  loadPolicies(): void {
    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.service.getAllPolicies().subscribe({
      next: (response: any) => {
        const policies = Array.isArray(response)
          ? response
          : response?.data ?? response?.items ?? response?.Data ?? [];

        this.policy = policies.find(
          (p: any) =>
            Number(p.quoteId ?? p.QuoteId) === this.quoteId
        ) ?? null;

        if (!this.policy) {
          this.errorMessage =
            'No policy found for this quote. Please verify the quote ID.';
        }

        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message ?? 'Unable to load policies.';

        this.loading = false;
      }
    });
  }

  issuePolicy(): void {
    if (this.issuing || this.loading || this.policyStatus === 'Active') {
      return;
    }

    this.issuing = true;
    this.errorMessage = '';
    this.successMessage = '';

    const request = {
      quoteId: this.quoteId
    };

    this.service.issuePolicy(request).subscribe({
      next: (response: any) => {
        const issuedPolicy = response?.data
          ?? response?.Data
          ?? response;

        this.policy = {
          ...this.policy,
          ...issuedPolicy
        };

        this.successMessage =
          response?.message
          ?? response?.Message
          ?? 'Policy issuance successful.';

        this.issuing = false;

        // Reload to retrieve the persisted policy and document status.
        this.loadPolicies();
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message
          ?? err?.error?.title
          ?? 'Policy issuance failed.';

        this.issuing = false;
      }
    });
  }

  backToUnderwriting(): void {
    this.router.navigate(['/underwriting']);
  }
}
