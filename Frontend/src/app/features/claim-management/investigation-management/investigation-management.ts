import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  ClaimResponseDto,
  ClaimService,
  CreateInvestigationDto,
  InvestigationReportDto,
  InvestigationResponseDto
} from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-investigation-management',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './investigation-management.html',
  styleUrl: './investigation-management.scss'
})
export class InvestigationManagement implements OnInit {
  private route = inject(ActivatedRoute);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  selectedClaimId: number = 0;
  investigations: InvestigationResponseDto[] = [];

  isSaving = false;
  isSubmittingReport = false;
  successMessage = '';
  errorMessage = '';

  // Form fields strictly mapping to CreateInvestigationDto
  assignForm: CreateInvestigationDto = {
    investigatorId: null,
    investigationReason: ''
  };

  // Form fields strictly mapping to InvestigationReportDto
  reportForm: InvestigationReportDto = {
    reportPath: ''
  };

  selectedInvestigationId: number = 0;

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
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to load claims.';
      }
    });
  }

  onClaimSelect(): void {
    this.successMessage = '';
    this.errorMessage = '';
    this.investigations = [];
    this.selectedInvestigationId = 0;
  }

  // POST /api/claims/{claimId}/investigation — CreateInvestigationDto
  assignInvestigator(): void {
    if (!this.selectedClaimId || this.selectedClaimId <= 0) {
      this.errorMessage = 'Please select a valid Claim ID.';
      return;
    }
    if (!this.assignForm.investigationReason || !this.assignForm.investigationReason.trim()) {
      this.errorMessage = 'Investigation Reason is required (MaxLength: 500).';
      return;
    }

    this.isSaving = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload: CreateInvestigationDto = {
      investigatorId: this.assignForm.investigatorId ? Number(this.assignForm.investigatorId) : null,
      investigationReason: this.assignForm.investigationReason.trim()
    };

    this.claimService.assignInvestigator(this.selectedClaimId, payload).subscribe({
      next: (res) => {
        this.isSaving = false;
        this.successMessage = `Investigation assigned (ID: ${res.investigationId}, Status: ${res.investigationStatus}).`;
        this.investigations = [...this.investigations, res];
        if (this.investigations.length === 1) {
          this.selectedInvestigationId = res.investigationId;
        }
      },
      error: (err) => {
        this.isSaving = false;
        this.errorMessage = err.message || 'Failed to assign investigator.';
      }
    });
  }

  // PUT /api/investigations/{id} — InvestigationReportDto
  submitReport(): void {
    if (!this.selectedInvestigationId || this.selectedInvestigationId <= 0) {
      this.errorMessage = 'Please select an Investigation ID to submit report for.';
      return;
    }
    if (!this.reportForm.reportPath || !this.reportForm.reportPath.trim()) {
      this.errorMessage = 'Report Path is required (MaxLength: 500).';
      return;
    }

    this.isSubmittingReport = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload: InvestigationReportDto = {
      reportPath: this.reportForm.reportPath.trim()
    };

    this.claimService.submitInvestigationReport(this.selectedInvestigationId, payload).subscribe({
      next: (res) => {
        this.isSubmittingReport = false;
        this.successMessage = `Investigation Report submitted for ID #${res.investigationId}. Status: ${res.investigationStatus}.`;
        // Update local list
        const idx = this.investigations.findIndex(i => i.investigationId === res.investigationId);
        if (idx >= 0) this.investigations[idx] = res;
      },
      error: (err) => {
        this.isSubmittingReport = false;
        this.errorMessage = err.message || 'Failed to submit investigation report.';
      }
    });
  }

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }
}
