import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import {
  AssessmentResponseDto,
  ClaimDocumentResponseDto,
  ClaimResponseDto,
  ClaimService,
  SettlementResponseDTO,
  SurveyResponseDto
} from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claim-details',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './claim-details.html',
  styleUrl: './claim-details.scss'
})
export class ClaimDetails implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  claimService = inject(ClaimService);

  claim: ClaimResponseDto | null = null;
  documents: ClaimDocumentResponseDto[] = [];
  surveys: SurveyResponseDto[] = [];
  assessment: AssessmentResponseDto | null = null;
  settlement: SettlementResponseDTO | null = null;
  isLoading = true;
  errorMessage = '';

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadClaimData(Number(id));
    } else {
      this.isLoading = false;
      this.errorMessage = 'No Claim ID provided in the URL.';
    }
  }

  loadClaimData(claimId: number): void {
    this.isLoading = true;

    this.claimService.getClaimById(claimId).subscribe({
      next: (res) => {
        this.claim = res;
        this.isLoading = false;
        this.loadRelatedData(claimId);
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.message || 'Claim not found.';
      }
    });
  }

  private loadRelatedData(claimId: number): void {
    this.claimService.getDocuments(claimId).subscribe({
      next: (docs) => this.documents = docs,
      error: () => this.documents = []
    });

    this.claimService.getSurveyDetails(claimId).subscribe({
      next: (surveys) => this.surveys = surveys,
      error: () => this.surveys = []
    });

    this.claimService.getAssessment(claimId).subscribe({
      next: (res) => this.assessment = res,
      error: () => this.assessment = null
    });

    this.claimService.getSettlement(claimId).subscribe({
      next: (res) => this.settlement = res,
      error: () => this.settlement = null
    });
  }

  deleteClaim(): void {
    if (!this.claim) return;
    if (this.claim.claimStatus !== 'Registered') {
      alert('Only claims in "Registered" status can be deleted.');
      return;
    }
    if (confirm(`Delete Claim #${this.claim.claimNumber} (ID: ${this.claim.claimId})?`)) {
      this.claimService.deleteClaim(this.claim.claimId).subscribe({
        next: () => {
          const role = this.claimService.currentRole.toLowerCase();
          this.router.navigate([`/${role}/claims/list`]);
        },
        error: (err) => alert(err.message || 'Delete failed.')
      });
    }
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
