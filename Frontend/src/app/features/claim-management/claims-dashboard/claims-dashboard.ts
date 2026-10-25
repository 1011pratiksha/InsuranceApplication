import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ClaimResponseDto, ClaimService, UserRole } from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claims-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './claims-dashboard.html',
  styleUrl: './claims-dashboard.scss'
})
export class ClaimsDashboard implements OnInit {
  private router = inject(Router);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  isLoading = true;

  get currentRole(): UserRole {
    return this.claimService.currentRole;
  }

  // Key KPI Metrics
  totalClaimsCount = 0;
  pendingReviewCount = 0;
  investigationCount = 0;
  approvedCount = 0;
  settledCount = 0;
  totalSettlementValue = 0;

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.isLoading = true;
    this.claimService.getAllClaims().subscribe({
      next: (data) => {
        this.claims = data;
        this.calculateMetrics(data);
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  private calculateMetrics(claims: ClaimResponseDto[]): void {
    this.totalClaimsCount = claims.length;
    this.pendingReviewCount = claims.filter(c => c.claimStatus === 'Registered').length;
    this.approvedCount = claims.filter(c => c.claimStatus === 'Approved').length;
    this.settledCount = claims.filter(c => c.claimStatus === 'Settled').length;
    this.totalSettlementValue = claims
      .filter(c => c.claimStatus === 'Settled')
      .reduce((acc, c) => acc + (c.estimatedLoss || 0), 0);
  }

  viewClaimDetails(id?: number): void {
    if (id !== undefined) {
      const role = this.currentRole.toLowerCase();
      this.router.navigate([`/${role}/claims/details`, id]);
    }
  }

  getRoleLink(path: string): string {
    const role = this.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }

  getStatusBadgeClass(status?: string): string {
    switch (status) {
      case 'Settled': return 'badge-success';
      case 'Approved': return 'badge-purple';
      case 'Rejected': return 'badge-danger';
      case 'Registered': return 'badge-info';
      default: return 'badge-neutral';
    }
  }
}
