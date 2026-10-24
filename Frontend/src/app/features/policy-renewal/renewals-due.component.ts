
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RenewalService } from '../../services/renewal/renewal.service';

@Component({
  selector: 'app-renewals-due',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './renewals-due.component.html',
  styleUrl: './renewals-due.component.css'
})
export class RenewalsDueComponent implements OnInit {
  private service = inject(RenewalService);
  private router = inject(Router);

  policies: any[] = [];

  search = '';
  statusFilter = '';
  dateFilter = '';

  loading = false;
  errorMessage = '';

  currentPage = 1;
  pageSize = 6;

  ngOnInit(): void {
    this.loadRenewals();
  }

  // Currently loads renewal ID 1 from your GET endpoint.
  loadRenewals(): void {
    this.loading = true;
    this.errorMessage = '';

    this.service.getRenewalDetails(1).subscribe({
      next: (response: any) => {
        const renewal =
          response?.data ??
          response?.Data ??
          response;

        // GET /renewals/{id} returns a single renewal.
        this.policies = renewal ? [renewal] : [];

        this.currentPage = 1;
        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message ??
          err?.error?.title ??
          'Unable to load renewal records. Please try again.';

        this.loading = false;
      }
    });
  }

  getPolicyNumber(policy: any): string {
    return String(
      policy?.policyNumber ??
      policy?.PolicyNumber ??
      policy?.policyNo ??
      policy?.PolicyNo ??
      policy?.policy_id ??
      policy?.policyId ??
      policy?.PolicyId ??
      '—'
    );
  }

  getCustomerName(policy: any): string {
    return String(
      policy?.customerName ??
      policy?.CustomerName ??
      policy?.customer?.fullName ??
      policy?.customer?.name ??
      policy?.Customer?.FullName ??
      policy?.Customer?.Name ??
      '—'
    );
  }

  getProductName(policy: any): string {
    return String(
      policy?.productName ??
      policy?.ProductName ??
      policy?.product?.productName ??
      policy?.Product?.ProductName ??
      '—'
    );
  }

  getRenewalDate(policy: any): string | null {
    return policy?.renewalDate ??
      policy?.RenewalDate ??
      policy?.renewal_date ??
      policy?.nextDueDate ??
      policy?.NextDueDate ??
      policy?.expiryDate ??
      policy?.ExpiryDate ??
      null;
  }

  getPremium(policy: any): number {
    return Number(
      policy?.premiumAmount ??
      policy?.PremiumAmount ??
      policy?.premium_amount ??
      policy?.premium ??
      policy?.Premium ??
      policy?.totalPremium ??
      policy?.TotalPremium ??
      0
    );
  }

  getStatus(policy: any): string {
    return String(
      policy?.renewalStatus ??
      policy?.RenewalStatus ??
      policy?.renewal_status ??
      policy?.status ??
      policy?.policyStatus ??
      policy?.PolicyStatus ??
      'Pending'
    );
  }

  get filteredPolicies(): any[] {
    const query = this.search.trim().toLowerCase();
    const now = new Date();

    return this.policies.filter((policy: any) => {
      const number = this.getPolicyNumber(policy).toLowerCase();
      const customer = this.getCustomerName(policy).toLowerCase();
      const product = this.getProductName(policy).toLowerCase();
      const status = this.getStatus(policy).toLowerCase();

      const matchesSearch =
        !query ||
        number.includes(query) ||
        customer.includes(query) ||
        product.includes(query);

      const matchesStatus =
        !this.statusFilter ||
        status === this.statusFilter.toLowerCase();

      let matchesDate = true;

      if (this.dateFilter) {
        const rawDate = this.getRenewalDate(policy);
        const date = rawDate ? new Date(rawDate) : null;
        const days = Number(this.dateFilter);

        matchesDate =
          !!date &&
          !Number.isNaN(date.getTime()) &&
          date >= new Date(now.getFullYear(), now.getMonth(), now.getDate()) &&
          date.getTime() - now.getTime() <= days * 86400000;
      }

      return matchesSearch && matchesStatus && matchesDate;
    });
  }

  get paginatedPolicies(): any[] {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredPolicies.slice(start, start + this.pageSize);
  }

  get totalPages(): number {
    return Math.max(
      1,
      Math.ceil(this.filteredPolicies.length / this.pageSize)
    );
  }

  get pageNumbers(): number[] {
    const visiblePages = 5;

    const start = Math.max(
      1,
      Math.min(
        this.currentPage - 2,
        this.totalPages - visiblePages + 1
      )
    );

    const end = Math.min(
      this.totalPages,
      start + visiblePages - 1
    );

    return Array.from(
      { length: end - start + 1 },
      (_, index) => start + index
    );
  }

  get showingFrom(): number {
    return this.filteredPolicies.length === 0
      ? 0
      : (this.currentPage - 1) * this.pageSize + 1;
  }

  get showingTo(): number {
    return Math.min(
      this.currentPage * this.pageSize,
      this.filteredPolicies.length
    );
  }

  applyFilters(): void {
    this.currentPage = 1;
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  clearFilters(): void {
    this.search = '';
    this.statusFilter = '';
    this.dateFilter = '';
    this.currentPage = 1;
  }

  getStatusClass(status: string): string {
    switch (String(status ?? '').toLowerCase()) {
      case 'pending':
        return 'status-pending';
      case 'approved':
      case 'active':
        return 'status-approved';
      case 'in review':
        return 'status-in-review';
      case 'rejected':
        return 'status-rejected';
      default:
        return 'status-default';
    }
  }

 
openCustomer(policy: any): void {
  const policyId =
    policy?.policyId ??
    policy?.PolicyId ??
    policy?.policy_id ??
    policy?.id ??
    policy?.Id;

  if (policyId == null) {
    this.errorMessage = 'Policy ID is missing from the renewal response.';
    return;
  }

  // Navigate using the policy ID until customer details
  // are retrieved through the policy/customer relationship.
  this.router.navigate(['/admin/policies', policyId]);
}

}
