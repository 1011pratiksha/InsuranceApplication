import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ClaimResponseDto, ClaimService, ClaimTrackingStep } from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claim-tracking',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './claim-tracking.html',
  styleUrl: './claim-tracking.scss'
})
export class ClaimTracking implements OnInit {
  private route = inject(ActivatedRoute);
  claimService = inject(ClaimService);

  searchQuery = '';
  claim: ClaimResponseDto | undefined;
  steps: ClaimTrackingStep[] = [];
  isLoading = false;
  notFound = false;
  errorMessage = '';

  ngOnInit(): void {
    const q = this.route.snapshot.queryParamMap.get('q');
    if (q) {
      this.searchQuery = q;
      this.trackClaim();
    }
  }

  trackClaim(): void {
    if (!this.searchQuery.trim()) return;

    this.isLoading = true;
    this.notFound = false;
    this.claim = undefined;
    this.steps = [];
    this.errorMessage = '';

    // Try numeric ID first, then by claim number
    const idOrNum = this.searchQuery.trim();
    const numericId = Number(idOrNum);

    const searchObs = !isNaN(numericId) && numericId > 0
      ? this.claimService.getClaimById(numericId)
      : null;

    if (searchObs) {
      searchObs.subscribe({
        next: (found) => {
          this.claim = found;
          this.claimService.getClaimTrackingSteps(found.claimId).subscribe(stepsList => {
            this.steps = stepsList;
            this.isLoading = false;
          });
        },
        error: () => {
          // Try by claim number string
          this.searchByClaimNumber(idOrNum);
        }
      });
    } else {
      this.searchByClaimNumber(idOrNum);
    }
  }

  private searchByClaimNumber(claimNumber: string): void {
    this.claimService.getAllClaims().subscribe({
      next: (claims) => {
        const found = claims.find(c => c.claimNumber.toUpperCase() === claimNumber.toUpperCase());
        if (found) {
          this.claim = found;
          this.claimService.getClaimTrackingSteps(found.claimId).subscribe(stepsList => {
            this.steps = stepsList;
            this.isLoading = false;
          });
        } else {
          this.notFound = true;
          this.isLoading = false;
        }
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to search claims.';
        this.isLoading = false;
      }
    });
  }

  get progressPercentage(): number {
    if (!this.steps || this.steps.length === 0) return 0;
    const completedCount = this.steps.filter(s => s.completed).length;
    return Math.round((completedCount / this.steps.length) * 100);
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

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }
}
