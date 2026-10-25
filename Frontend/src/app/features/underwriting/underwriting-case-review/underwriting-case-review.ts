
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

import {
  UnderwritingService,
  UnderwritingCase,
  MedicalDetails,
  FinancialDetails,
  RiskScore,
  PremiumLoading,
  UnderwritingDecision,
  AuditLog,
  ApiMessage
} from '../../../services/underwriting/underwriting';

@Component({
  selector: 'app-underwriting-case-review',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './underwriting-case-review.html',
  styleUrl: './underwriting-case-review.scss'
})
export class UnderwritingCaseReview implements OnInit {
  caseId = 0;
  caseData: UnderwritingCase | null = null;
  medical: MedicalDetails | null = null;
  financial: FinancialDetails | null = null;
  risk: RiskScore | null = null;
  loadingData: PremiumLoading | null = null;
  decision: UnderwritingDecision | null = null;
  auditLogs: AuditLog[] = [];

  riskInput: RiskScore = {
    age_score: 0,
    medical_score: 0,
    occupation_score: 0,
    lifestyle_score: 0,
    financial_score: 0
  };

  decisionInput: UnderwritingDecision = {
    decision: '',
    loading_percentage: 0,
    exclusion_details: '',
    rejection_reason: ''
  };

  busy = false;
  errorMessage = '';
  successMessage = '';
  activeTab = 'overview';

  constructor(
    private readonly api: UnderwritingService,
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.caseId = Number(this.route.snapshot.paramMap.get('id'));

    if (!Number.isInteger(this.caseId) || this.caseId <= 0) {
      this.errorMessage = 'Invalid underwriting case ID.';
      return;
    }

    this.loadCase();
  }

  loadCase(): void {
    this.errorMessage = '';
    this.successMessage = '';
    this.busy = true;

    this.api.getCase(this.caseId)
      .pipe(finalize(() => {
        this.busy = false;
      }))
      .subscribe({
        next: (data: UnderwritingCase) => {
          this.caseData = data;
          this.loadRelatedData();
        },
        error: (error: unknown) => {
          this.errorMessage = this.getErrorMessage(
            error,
            'Unable to load this case.'
          );
        }
      });
  }

  loadRelatedData(): void {
    this.api.getMedical(this.caseId).subscribe({
      next: (data: MedicalDetails) => {
        this.medical = data;
      },
      error: () => {
        this.medical = null;
      }
    });

    this.api.getFinancial(this.caseId).subscribe({
      next: (data: FinancialDetails) => {
        this.financial = data;
      },
      error: () => {
        this.financial = null;
      }
    });

    this.api.getRisk(this.caseId).subscribe({
      next: (data: RiskScore) => {
        this.risk = data;
        this.riskInput = {
          age_score: data.age_score,
          medical_score: data.medical_score,
          occupation_score: data.occupation_score,
          lifestyle_score: data.lifestyle_score,
          financial_score: data.financial_score
        };
      },
      error: () => {
        this.risk = null;
      }
    });

    this.api.getPremiumLoading(this.caseId).subscribe({
      next: (data: PremiumLoading) => {
        this.loadingData = data;
      },
      error: () => {
        this.loadingData = null;
      }
    });

    this.api.getDecision(this.caseId).subscribe({
      next: (data: UnderwritingDecision) => {
        this.decision = data;
        this.decisionInput = {
          decision: data.decision,
          loading_percentage: data.loading_percentage,
          exclusion_details: data.exclusion_details ?? '',
          rejection_reason: data.rejection_reason ?? '',
          approved_by: data.approved_by
        };
      },
      error: () => {
        this.decision = null;
      }
    });

    this.api.getAuditLog(this.caseId).subscribe({
      next: (data: AuditLog[]) => {
        this.auditLogs = data ?? [];
      },
      error: () => {
        this.auditLogs = [];
      }
    });
  }

  calculateRisk(): void {
    this.resetMessages();

    const values: number[] = [
      this.riskInput.age_score,
      this.riskInput.medical_score,
      this.riskInput.occupation_score,
      this.riskInput.lifestyle_score,
      this.riskInput.financial_score
    ];

    if (
      values.some(value =>
        !Number.isFinite(value) || value < 0
      )
    ) {
      this.errorMessage =
        'Risk scores must be valid non-negative numbers.';
      return;
    }

    this.busy = true;

    this.api.calculateRisk(this.caseId, this.riskInput)
      .pipe(finalize(() => {
        this.busy = false;
      }))
      .subscribe({
        next: (response: ApiMessage) => {
          this.successMessage =
            response.msg || response.message || 'Risk score calculated.';
          this.loadRelatedData();
        },
        error: (error: unknown) => {
          this.errorMessage = this.getErrorMessage(
            error,
            'Risk calculation failed.'
          );
        }
      });
  }

  approve(): void {
    this.decisionInput.decision = 'Approved';
    this.sendDecision('approve');
  }

  approveWithLoading(): void {
    if (
      !Number.isFinite(this.decisionInput.loading_percentage) ||
      this.decisionInput.loading_percentage < 0
    ) {
      this.errorMessage =
        'Loading percentage must be a valid non-negative number.';
      return;
    }

    this.decisionInput.decision = 'ApprovedWithLoading';
    this.sendDecision('loading');
  }

  reject(): void {
    if (!this.decisionInput.rejection_reason?.trim()) {
      this.errorMessage =
        'Enter a rejection reason before rejecting the case.';
      return;
    }

    this.decisionInput.decision = 'Rejected';
    this.sendDecision('reject');
  }

  private sendDecision(
    action: 'approve' | 'loading' | 'reject'
  ): void {
    this.resetMessages();

    if (this.busy) {
      return;
    }

    const request: UnderwritingDecision = {
      decision: this.decisionInput.decision,
      loading_percentage:
        Number(this.decisionInput.loading_percentage) || 0,
      exclusion_details: this.decisionInput.exclusion_details || '',
      rejection_reason: this.decisionInput.rejection_reason || ''
    };

    let operation: Observable<ApiMessage>;

    if (action === 'approve') {
      operation = this.api.approve(this.caseId, request);
    } else if (action === 'loading') {
      operation = this.api.applyLoading(this.caseId, request);
    } else {
      operation = this.api.reject(this.caseId, request);
    }

    this.busy = true;

    operation.pipe(
      finalize(() => {
        this.busy = false;
      })
    ).subscribe({
      next: (response: ApiMessage) => {
        this.successMessage =
          response.msg || response.message || 'Decision saved successfully.';

        this.loadCase();
        this.loadRelatedData();
      },
      error: (error: unknown) => {
        this.errorMessage = this.getErrorMessage(
          error,
          'Unable to save the decision.'
        );
      }
    });
  }

  setTab(tab: string): void {
    this.activeTab = tab;
  }

  backToQueue(): void {
    this.router.navigate(['/admin/underwriting']);
  }

  private resetMessages(): void {
    this.errorMessage = '';
    this.successMessage = '';
  }

  private getErrorMessage(
    error: unknown,
    fallback: string
  ): string {
    if (
      typeof error === 'object' &&
      error !== null &&
      'error' in error
    ) {
      const body = (error as {
        error?: { msg?: string; message?: string };
      }).error;

      return body?.msg || body?.message || fallback;
    }

    return fallback;
  }
}
