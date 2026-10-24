
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PolicyIssuanceService } from '../../../../services/policy-issurance/policy-issuance.service';

@Component({
  selector: 'app-my-policies',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './my-policies.component.html',
  styleUrl: './my-policies.component.css'
})
export class MyPoliciesComponent implements OnInit {

  private service = inject(PolicyIssuanceService);
  private router = inject(Router);

  policies: any[] = [];

  search = '';
  statusFilter = '';
  dateFilter = '';

  loading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadPolicies();
  }

  // Load policies from the existing API.
  loadPolicies(): void {
    this.loading = true;
    this.errorMessage = '';

    this.service.getAllPolicies().subscribe({
      next: (response: any) => {
        const data = Array.isArray(response)
          ? response
          : response?.data
            ?? response?.items
            ?? response?.Data
            ?? response?.Items
            ?? [];

        this.policies = Array.isArray(data) ? data : [];
        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message
          ?? err?.error?.title
          ?? 'Unable to load policies. Please try again.';

        this.loading = false;
      }
    });
  }

  // Supports different property naming conventions in API responses.
  getPolicyNumber(policy: any): string {
    return String(
      policy?.policyNumber
      ?? policy?.PolicyNumber
      ?? policy?.policyNo
      ?? policy?.PolicyNo
      ?? '—'
    );
  }

  getProductName(policy: any): string {
    return String(
      policy?.productName
      ?? policy?.ProductName
      ?? policy?.product?.productName
      ?? policy?.Product?.ProductName
      ?? '—'
    );
  }

  getPolicyStatus(policy: any): string {
    return String(
      policy?.status
      ?? policy?.Status
      ?? policy?.policyStatus
      ?? policy?.PolicyStatus
      ?? 'Pending'
    );
  }

  getSumInsured(policy: any): number {
    return Number(
      policy?.sumInsured
      ?? policy?.SumInsured
      ?? 0
    );
  }

  getPremium(policy: any): number {
    return Number(
      policy?.premium
      ?? policy?.Premium
      ?? policy?.totalPremium
      ?? policy?.TotalPremium
      ?? 0
    );
  }

  getNextDueDate(policy: any): any {
    return policy?.nextDueDate
      ?? policy?.NextDueDate
      ?? null;
  }

  // Search, status and date filtering.
  get filteredPolicies(): any[] {
    const query = this.search.trim().toLowerCase();
    const now = new Date();

    return this.policies.filter((policy: any) => {
      const policyNumber = this.getPolicyNumber(policy).toLowerCase();
      const productName = this.getProductName(policy).toLowerCase();
      const status = this.getPolicyStatus(policy).toLowerCase();

      const matchesSearch =
        !query ||
        policyNumber.includes(query) ||
        productName.includes(query);

      const matchesStatus =
        !this.statusFilter ||
        status === this.statusFilter.toLowerCase();

      let matchesDate = true;

      if (this.dateFilter) {
        const rawDate =
          policy?.issueDate
          ?? policy?.IssueDate
          ?? policy?.createdDate
          ?? policy?.CreatedDate
          ?? null;

        const policyDate = rawDate ? new Date(rawDate) : null;
        const days = Number(this.dateFilter);

        matchesDate =
          !!policyDate &&
          !Number.isNaN(policyDate.getTime()) &&
          policyDate <= now &&
          now.getTime() - policyDate.getTime() <=
            days * 24 * 60 * 60 * 1000;
      }

      return matchesSearch && matchesStatus && matchesDate;
    });
  }

  getStatusClass(status: string): string {
    switch (String(status ?? '').trim().toLowerCase()) {
      case 'pending':
        return 'status-pending';

      case 'approved':
        return 'status-approved';

      case 'active':
        return 'status-active';

      case 'in review':
        return 'status-in-review';

      case 'rejected':
        return 'status-rejected';

      default:
        return 'status-default';
    }
  }

  clearFilters(): void {
    this.search = '';
    this.statusFilter = '';
    this.dateFilter = '';
  }

  // Open the existing customer policy details route.
  openPolicy(policy: any): void {
    const id =
      policy?.id
      ?? policy?.Id
      ?? policy?.policyId
      ?? policy?.PolicyId;

    if (id == null) {
      this.errorMessage = 'Policy ID is missing.';
      return;
    }

    this.router.navigate(['/customer/policy-details', id]);
  }
}
