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
  loading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.loading = true;
    this.errorMessage = '';

    this.service.getAllPolicies().subscribe({
      next: (response: any) => {
        const data = Array.isArray(response)
          ? response
          : response?.data ?? response?.items ?? [];

        this.policies = data;
        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message ?? 'Unable to load policies.';
        this.loading = false;
      }
    });
  }

  get filteredPolicies(): any[] {
    const query = this.search.trim().toLowerCase();

    return this.policies.filter((p: any) => {
      const number = String(p.policyNumber ?? p.policyNo ?? '');
      const product = String(
        p.productName ?? p.product?.productName ?? ''
      );
      const status = String(p.status ?? p.policyStatus ?? '');

      return (
        (!query ||
          number.toLowerCase().includes(query) ||
          product.toLowerCase().includes(query)) &&
        (!this.statusFilter ||
          status.toLowerCase() === this.statusFilter.toLowerCase())
      );
    });
  }

  openPolicy(policy: any): void {
    const id = policy.id ?? policy.policyId;

    if (!id) {
      this.errorMessage = 'Policy ID is missing.';
      return;
    }

    this.router.navigate(['/customer/policy-details', id]);
  }
}
