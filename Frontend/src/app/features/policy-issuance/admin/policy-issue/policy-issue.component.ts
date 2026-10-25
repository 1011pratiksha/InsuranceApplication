
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PolicyIssuanceService } from '../../../../services/policy-issurance/policy-issuance.service';

@Component({
  selector: 'app-policy-issue',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './policy-issue.component.html',
  styleUrl: './policy-issue.component.css'
})
export class PolicyIssueComponent implements OnInit {

  private service = inject(PolicyIssuanceService);
  private router = inject(Router);

  // Temporary quote ID for testing
  quoteId: number = 1;

  policy: any = null;

  loading: boolean = false;
  issuing: boolean = false;

  errorMessage: string = '';
  successMessage: string = '';

  ngOnInit(): void {
    this.loadPolicies();
  }

  // Load existing policies
  loadPolicies(): void {
    this.loading = true;
    this.errorMessage = '';

    this.service.getAllPolicies().subscribe({
      next: (response: any) => {
        const policies = Array.isArray(response)
          ? response
          : response?.data ?? response?.items ?? [];

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

  // Issue policy
  issuePolicy(): void {
    if (this.issuing) {
      return;
    }

    this.issuing = true;
    this.errorMessage = '';
    this.successMessage = '';

    // Confirm this request body against your API DTO.
    const request = {
      quoteId: this.quoteId
    };

    this.service.issuePolicy(request).subscribe({
      next: (response: any) => {
        this.policy = response?.data ?? response;

        this.successMessage =
          response?.message ?? 'Policy issuance successful.';

        this.issuing = false;
      },

      error: (err: any) => {
        this.errorMessage =
          err?.error?.message ?? 'Policy issuance failed.';

        this.issuing = false;
      }
    });
  }

  // Return to Underwriting
  backToUnderwriting(): void {
    this.router.navigate(['/underwriting']);
  }
}
