
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { PolicyIssuanceService } from '../../../../services/policy-issurance/policy-issuance.service';

@Component({
  selector: 'app-policy-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './policy-details.component.html',
  styleUrl: './policy-details.component.css'
})
export class PolicyDetailsComponent implements OnInit {
  private service = inject(PolicyIssuanceService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  policy: any = null;
  loading = false;
  errorMessage = '';
  actionMessage = '';

  ngOnInit(): void {
    this.loadPolicy();
  }

  loadPolicy(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id === null) {
      this.errorMessage = 'Policy ID is missing from the URL.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    this.service.getAllPolicies().subscribe({
      next: (response: any) => {
        const data = Array.isArray(response)
          ? response
          : response?.data
            ?? response?.items
            ?? response?.Data
            ?? [];

        this.policy = data.find((p: any) =>
          String(
            p.id ?? p.Id ?? p.policyId ?? p.PolicyId
          ) === id
        ) ?? null;

        if (!this.policy) {
          this.errorMessage = 'Policy not found.';
        }

        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message ?? 'Unable to load policy details.';
        this.loading = false;
      }
    });
  }

  get policyNumber(): string {
    return this.policy?.policyNumber
      ?? this.policy?.PolicyNumber
      ?? this.policy?.policyNo
      ?? this.policy?.PolicyNo
      ?? '—';
  }

  get status(): string {
    return this.policy?.status
      ?? this.policy?.policyStatus
      ?? this.policy?.PolicyStatus
      ?? 'Pending';
  }

  get cover(): number {
    return Number(
      this.policy?.cover
      ?? this.policy?.sumInsured
      ?? this.policy?.SumInsured
      ?? this.policy?.coverageAmount
      ?? this.policy?.CoverageAmount
      ?? 0
    );
  }

  get nominee(): string {
    return this.policy?.nomineeName
      ?? this.policy?.NomineeName
      ?? this.policy?.nominee?.name
      ?? this.policy?.Nominee?.Name
      ?? 'Not specified';
  }

  get riders(): string {
    const value = this.policy?.riders
      ?? this.policy?.Riders
      ?? this.policy?.riderName
      ?? this.policy?.RiderName;

    if (Array.isArray(value)) {
      return value.map((r: any) =>
        typeof r === 'string'
          ? r
          : r?.riderName ?? r?.RiderName ?? r?.name ?? ''
      ).filter(Boolean).join(', ') || 'None';
    }

    return value ? String(value) : 'None';
  }

  get validTill(): string | null {
    return this.policy?.validTill
      ?? this.policy?.ValidTill
      ?? this.policy?.expiryDate
      ?? this.policy?.ExpiryDate
      ?? this.policy?.endDate
      ?? this.policy?.EndDate
      ?? null;
  }

  getStatusClass(): string {
    switch (this.status.toLowerCase()) {
      case 'active':
        return 'status-active';
      case 'approved':
        return 'status-approved';
      case 'pending':
        return 'status-pending';
      case 'rejected':
        return 'status-rejected';
      default:
        return 'status-default';
    }
  }

  downloadPolicyPdf(): void {
    this.actionMessage =
      'Connect this action to your policy PDF download API.';
  }

  payPremium(): void {
    this.router.navigate(['/customer/premiums']);
  }

  requestChange(): void {
    this.router.navigate(['/customer/service-requests']);
  }

  raiseClaim(): void {
    this.router.navigate(['/customer/claims']);
  }

  backToPolicies(): void {
    this.router.navigate(['/customer/my-policies']);
  }
}
