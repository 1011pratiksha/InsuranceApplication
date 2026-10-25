import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  ClaimResponseDto,
  ClaimService,
  CreateSurveyDto,
  SurveyReportDto,
  SurveyResponseDto
} from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-survey-management',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './survey-management.html',
  styleUrl: './survey-management.scss'
})
export class SurveyManagement implements OnInit {
  private route = inject(ActivatedRoute);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  selectedClaimId: number = 0;
  surveys: SurveyResponseDto[] = [];

  isSaving = false;
  isSubmittingReport = false;
  successMessage = '';
  errorMessage = '';

  // Form fields strictly mapping to CreateSurveyDto
  assignForm: CreateSurveyDto = {
    surveyorId: 1,
    inspectionDate: null
  };

  // Form fields strictly mapping to SurveyReportDto
  reportForm: SurveyReportDto = {
    inspectionDate: null,
    reportPath: null,
    remarks: ''
  };

  selectedSurveyId: number = 0;

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
        this.loadSurveys();
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to load claims.';
      }
    });
  }

  loadSurveys(): void {
    if (!this.selectedClaimId) return;
    this.errorMessage = '';
    this.claimService.getSurveyDetails(this.selectedClaimId).subscribe({
      next: (surveys) => {
        this.surveys = surveys;
        if (surveys.length > 0) {
          this.selectedSurveyId = surveys[0].surveyId;
        }
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to load surveys for this claim.';
        this.surveys = [];
      }
    });
  }

  onClaimSelect(): void {
    this.loadSurveys();
    this.successMessage = '';
    this.errorMessage = '';
  }

  // POST /api/claims/{claimId}/survey — CreateSurveyDto
  assignSurveyor(): void {
    if (!this.selectedClaimId || this.selectedClaimId <= 0) {
      this.errorMessage = 'Please select a valid Claim ID.';
      return;
    }
    if (!this.assignForm.surveyorId || this.assignForm.surveyorId <= 0) {
      this.errorMessage = 'Surveyor ID must be a positive integer.';
      return;
    }

    this.isSaving = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload: CreateSurveyDto = {
      surveyorId: Number(this.assignForm.surveyorId),
      inspectionDate: this.assignForm.inspectionDate || null
    };

    this.claimService.assignSurveyor(this.selectedClaimId, payload).subscribe({
      next: (res) => {
        this.isSaving = false;
        this.successMessage = `Surveyor ID #${res.surveyorId} assigned successfully (Survey ID: ${res.surveyId}, Status: ${res.surveyStatus}).`;
        this.loadSurveys();
      },
      error: (err) => {
        this.isSaving = false;
        this.errorMessage = err.message || 'Failed to assign surveyor.';
      }
    });
  }

  // PUT /api/surveys/{surveyId} — SurveyReportDto
  submitReport(): void {
    if (!this.selectedSurveyId || this.selectedSurveyId <= 0) {
      this.errorMessage = 'Please select a Survey ID to submit report for.';
      return;
    }
    if (!this.reportForm.remarks || !this.reportForm.remarks.trim()) {
      this.errorMessage = 'Remarks are required (MaxLength: 500).';
      return;
    }

    this.isSubmittingReport = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload: SurveyReportDto = {
      inspectionDate: this.reportForm.inspectionDate || null,
      reportPath: this.reportForm.reportPath?.trim() || null,
      remarks: this.reportForm.remarks.trim()
    };

    this.claimService.submitSurveyReport(this.selectedSurveyId, payload).subscribe({
      next: (res) => {
        this.isSubmittingReport = false;
        this.successMessage = `Survey Report submitted for Survey ID #${res.surveyId}. Status: ${res.surveyStatus}.`;
        this.loadSurveys();
      },
      error: (err) => {
        this.isSubmittingReport = false;
        this.errorMessage = err.message || 'Failed to submit survey report.';
      }
    });
  }

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }
}
