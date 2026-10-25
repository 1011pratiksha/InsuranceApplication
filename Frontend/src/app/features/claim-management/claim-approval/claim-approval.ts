import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ClaimResponseDto, ClaimService } from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claim-approval',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './claim-approval.html',
  styleUrl: './claim-approval.scss'
})
export class ClaimApproval implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  selectedClaim: ClaimResponseDto | null = null;
  isProcessing = false;
  successMessage = '';
  errorMessage = '';

  ngOnInit(): void {
    this.loadClaims();
  }

  loadClaims(): void {
    this.claimService.getAllClaims().subscribe({
      next: (list) => {
        this.claims = list;

        const paramClaimId = this.route.snapshot.queryParamMap.get('claimId');
        if (paramClaimId) {
          const match = list.find(c => c.claimId === Number(paramClaimId));
          if (match) {
            this.selectedClaim = match;
            return;
          }
        }

        if (list.length > 0) {
          this.selectedClaim = list[0];
        }
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to load claims.';
      }
    });
  }

  selectClaim(claim: ClaimResponseDto): void {
    this.selectedClaim = claim;
    this.successMessage = '';
    this.errorMessage = '';
  }

  // PUT /api/claims/{id}/approve
  approveClaim(): void {
    if (!this.selectedClaim) return;
    this.isProcessing = true;
    this.successMessage = '';
    this.errorMessage = '';

    this.claimService.approveClaim(this.selectedClaim.claimId).subscribe({
      next: (res) => {
        this.isProcessing = false;
        this.successMessage = res.message || `Claim #${this.selectedClaim?.claimNumber} approved successfully.`;
        this.loadClaims();
      },
      error: (err) => {
        this.isProcessing = false;
        this.errorMessage = err.message || 'Approval failed.';
      }
    });
  }

  // PUT /api/claims/{id}/reject
  rejectClaim(): void {
    if (!this.selectedClaim) return;
    this.isProcessing = true;
    this.successMessage = '';
    this.errorMessage = '';

    this.claimService.rejectClaim(this.selectedClaim.claimId).subscribe({
      next: (res) => {
        this.isProcessing = false;
        this.successMessage = res.message || `Claim #${this.selectedClaim?.claimNumber} rejected successfully.`;
        this.loadClaims();
      },
      error: (err) => {
        this.isProcessing = false;
        this.errorMessage = err.message || 'Rejection failed.';
      }
    });
  }

  goToSettlement(): void {
    if (this.selectedClaim) {
      const role = this.claimService.currentRole.toLowerCase();
      this.router.navigate([`/${role}/claims/settlement`], {
        queryParams: { claimId: this.selectedClaim.claimId }
      });
    }
  }

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }

  getStatusBadgeClass(status: string): string {
    switch (status) {
      case 'Settled': return 'badge-success';
      case 'Approved': return 'badge-purple';
      case 'Rejected': return 'badge-danger';
      case 'Registered': return 'badge-info';
      default: return 'badge-neutral';
    }
  }
}
