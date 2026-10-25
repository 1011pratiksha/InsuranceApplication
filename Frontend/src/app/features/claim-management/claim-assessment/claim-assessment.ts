import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  AssessmentResponseDto,
  ClaimResponseDto,
  ClaimService,
  CreateAssessmentDto
} from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claim-assessment',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './claim-assessment.html',
  styleUrl: './claim-assessment.scss'
})
export class ClaimAssessmentComponent implements OnInit {
  private route = inject(ActivatedRoute);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  selectedClaimId: number = 0;
  assessment: AssessmentResponseDto | null = null;

  isSubmitting = false;
  isLoading = false;
  successMessage = '';
  errorMessage = '';

  // Form fields strictly mapping to CreateAssessmentDto
  formData: CreateAssessmentDto = {
    assessedAmount: 0,
    deductible: 0,
    depreciation: 0,
    nonPayable: 0,
    netPayable: 0
  };

  // Auto-computed NetPayable from formula in specification:
  // NetPayable = AssessedAmount - Deductible - Depreciation - NonPayable
  get computedNetPayable(): number {
    return Math.max(
      0,
      (this.formData.assessedAmount || 0)
      - (this.formData.deductible || 0)
      - (this.formData.depreciation || 0)
      - (this.formData.nonPayable || 0)
    );
  }

  ngOnInit(): void {
    this.claimService.getAllClaims().subscribe({
      next: (claims) => {
        this.claims = claims;
        const paramClaimId = this.route.snapshot.queryParamMap.get('claimId');
        if (paramClaimId) {
          this.selectedClaimId = Number(paramClaimId);
        } else if (claims.length > 0) {
          this.selectedClaimId = claims[0].claimId;
        }
        this.loadAssessment();
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to load claims.';
      }
    });
  }

  loadAssessment(): void {
    if (!this.selectedClaimId) return;
    this.isLoading = true;
    this.assessment = null;
    this.claimService.getAssessment(this.selectedClaimId).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.assessment = res;
        // Pre-fill form with existing values
        this.formData = {
          assessedAmount: res.assessedAmount,
          deductible: res.deductible,
          depreciation: res.depreciation,
          nonPayable: res.nonPayable,
          netPayable: res.netPayable
        };
      },
      error: () => {
        this.isLoading = false;
        this.assessment = null;
      }
    });
  }

  onClaimSelect(): void {
    this.successMessage = '';
    this.errorMessage = '';
    this.loadAssessment();
  }

  onAmountChange(): void {
    // Auto-update netPayable display field
    this.formData.netPayable = this.computedNetPayable;
  }

  // POST /api/claims/{claimId}/assessment — CreateAssessmentDto
  submitAssessment(): void {
    if (!this.selectedClaimId || this.selectedClaimId <= 0) {
      this.errorMessage = 'Please select a valid Claim ID.';
      return;
    }
    if (!this.formData.assessedAmount || this.formData.assessedAmount < 0.01) {
      this.errorMessage = 'Assessed Amount must be >= 0.01.';
      return;
    }
    if (this.formData.deductible < 0) {
      this.errorMessage = 'Deductible must be >= 0.';
      return;
    }
    if (this.formData.depreciation < 0) {
      this.errorMessage = 'Depreciation must be >= 0.';
      return;
    }
    if (this.formData.nonPayable < 0) {
      this.errorMessage = 'Non Payable must be >= 0.';
      return;
    }
    if (this.computedNetPayable < 0.01) {
      this.errorMessage = 'Net Payable (Assessed - Deductible - Depreciation - NonPayable) must be >= 0.01.';
      return;
    }

    this.isSubmitting = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload: CreateAssessmentDto = {
      assessedAmount: Number(this.formData.assessedAmount),
      deductible: Number(this.formData.deductible),
      depreciation: Number(this.formData.depreciation),
      nonPayable: Number(this.formData.nonPayable),
      netPayable: this.computedNetPayable
    };

    this.claimService.addAssessment(this.selectedClaimId, payload).subscribe({
      next: (res) => {
        this.isSubmitting = false;
        this.assessment = res;
        this.successMessage = `Assessment saved for Claim ID #${res.claimId}. Net Payable: $${res.netPayable.toFixed(2)}.`;
      },
      error: (err) => {
        this.isSubmitting = false;
        this.errorMessage = err.message || 'Failed to save assessment.';
      }
    });
  }

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }
}
