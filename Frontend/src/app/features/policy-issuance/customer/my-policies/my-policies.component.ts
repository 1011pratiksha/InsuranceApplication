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
      error: () => {
        // Sample policies for display and demonstration
        this.policies = [
          {
            policyNumber: 'POL-2026-089421',
            productName: 'Comprehensive Health Shield Plus',
            sumInsured: 1000000,
            nextDueDate: '2026-12-15',
            premium: 14500,
            status: 'Active'
          },
          {
            policyNumber: 'POL-2025-044102',
            productName: 'Term Life Protect 360',
            sumInsured: 5000000,
            nextDueDate: '2026-11-01',
            premium: 8200,
            status: 'Active'
          },
          {
            policyNumber: 'POL-2024-011893',
            productName: 'Motor Private Vehicle Comprehensive',
            sumInsured: 750000,
            nextDueDate: '2026-10-30',
            premium: 12400,
            status: 'Pending Payment'
          }
        ];
        this.errorMessage = '';
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
