import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import {
  AssessmentResponseDto,
  ClaimDocumentResponseDto,
  ClaimResponseDto,
  ClaimTrackingStep,
  CreateAssessmentDto,
  CreateClaimDocumentDto,
  CreateClaimDto,
  CreateInvestigationDto,
  CreateSurveyDto,
  InvestigationReportDto,
  InvestigationResponseDto,
  SettlementDTO,
  SettlementResponseDTO,
  SurveyReportDto,
  SurveyResponseDto,
  UpdateClaimDto
} from './claim.models';

export * from './claim.models';

export type UserRole = 'Customer' | 'Admin' | 'Agent';

// Helper to reliably extract error strings returned by ASP.NET Core controllers
export function extractErrorMessage(err: any): string {
  if (!err) return 'An unexpected error occurred.';
  if (typeof err === 'string') return err;
  if (err instanceof HttpErrorResponse) {
    if (err.status === 0) {
      return `Failed to connect to backend at ${err.url || 'https://localhost:7232'}. Please verify your .NET backend is running with CORS enabled.`;
    }
    if (err.error) {
      if (typeof err.error === 'string') return err.error;
      if (err.error.message) return err.error.message;
      if (err.error.msg) return err.error.msg;
      if (err.error.title) return err.error.title;
      if (err.error.errors) {
        const msgs = Object.values(err.error.errors).flat().join(', ');
        if (msgs) return msgs;
      }
    }
    return `Server Error (${err.status}: ${err.statusText || 'Error'})`;
  }
  if (err.message) return err.message;
  return 'An error occurred while communicating with the server.';
}

@Injectable({
  providedIn: 'root'
})
export class ClaimService {
  private http = inject(HttpClient);

  // Active Role State
  private currentRoleSubject = new BehaviorSubject<UserRole>('Customer');
  public currentRole$ = this.currentRoleSubject.asObservable();
  readonly roleSignal = signal<UserRole>('Customer');

  get currentRole(): UserRole {
    return this.roleSignal();
  }

  setRole(role: UserRole): void {
    this.roleSignal.set(role);
    this.currentRoleSubject.next(role);
  }

  // Configurable backend URL (Default: https://localhost:7232/api)
  private _apiBaseUrl = 'https://localhost:7232/api';
  readonly apiBaseUrlSignal = signal<string>(this.getInitialApiUrl());

  get baseUrl(): string {
    return this.apiBaseUrlSignal();
  }

  setApiUrl(url: string): void {
    const cleanUrl = url.trim().replace(/\/+$/, '');
    this.apiBaseUrlSignal.set(cleanUrl);
    if (typeof localStorage !== 'undefined') {
      localStorage.setItem('API_BASE_URL', cleanUrl);
    }
  }

  private getInitialApiUrl(): string {
    if (typeof localStorage !== 'undefined') {
      const saved = localStorage.getItem('API_BASE_URL');
      if (saved) return saved;
    }
    return this._apiBaseUrl;
  }

  // Backend connection status
  readonly backendOnlineSignal = signal<boolean | null>(null);

  // In-memory local fallback store for offline development/fallback
  private localClaims: ClaimResponseDto[] = [];
  private localDocuments: ClaimDocumentResponseDto[] = [];
  private localSurveys: SurveyResponseDto[] = [];
  private localInvestigations: InvestigationResponseDto[] = [];
  private localAssessments: AssessmentResponseDto[] = [];
  private localSettlements: SettlementResponseDTO[] = [];

  private generateClaimNumber(): string {
    const hex = Math.random().toString(16).substring(2, 14).toUpperCase().padEnd(12, '0');
    return `CLM-${hex}`;
  }

  // ==========================================
  // 1. Claims CRUD (PDF Section 2 & 5: ClaimsController)
  // ==========================================

  createClaim(dto: CreateClaimDto): Observable<ClaimResponseDto> {
    return this.http.post<ClaimResponseDto>(`${this.baseUrl}/Claims`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          // Backend responded with HTTP error (e.g. 400 Bad Request: "Invalid incident date.")
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        // Offline / Network fallback
        this.backendOnlineSignal.set(false);
        const today = new Date().toISOString().split('T')[0];
        if (dto.incidentDate > today) {
          return throwError(() => new Error('Invalid incident date: Incident date must be in the past or today.'));
        }
        const newId = this.localClaims.length ? Math.max(...this.localClaims.map(c => c.claimId)) + 1 : 1;
        const newClaim: ClaimResponseDto = {
          claimId: newId,
          claimNumber: this.generateClaimNumber(),
          policyId: dto.policyId,
          customerId: dto.customerId,
          claimType: dto.claimType,
          incidentDate: dto.incidentDate,
          claimDate: new Date().toISOString(),
          estimatedLoss: dto.estimatedLoss,
          claimStatus: 'Registered'
        };
        this.localClaims.unshift(newClaim);
        return of(newClaim);
      })
    );
  }

  getAllClaims(): Observable<ClaimResponseDto[]> {
    return this.http.get<ClaimResponseDto[]>(`${this.baseUrl}/Claims`).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        return of(this.localClaims);
      })
    );
  }

  getClaimById(claimId: number): Observable<ClaimResponseDto> {
    return this.http.get<ClaimResponseDto>(`${this.baseUrl}/Claims/${claimId}`).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const found = this.localClaims.find(c => c.claimId === Number(claimId));
        return found ? of(found) : throwError(() => new Error('Claim not found.'));
      })
    );
  }

  updateClaim(claimId: number, dto: UpdateClaimDto): Observable<ClaimResponseDto> {
    return this.http.put<ClaimResponseDto>(`${this.baseUrl}/Claims/${claimId}`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const claim = this.localClaims.find(c => c.claimId === Number(claimId));
        if (!claim) return throwError(() => new Error('Claim not found.'));
        if (claim.claimStatus !== 'Registered') {
          return throwError(() => new Error('Only registered claims can be updated.'));
        }
        claim.claimType = dto.claimType;
        claim.incidentDate = dto.incidentDate;
        claim.estimatedLoss = dto.estimatedLoss;
        return of(claim);
      })
    );
  }

  deleteClaim(claimId: number): Observable<string> {
    return this.http.delete(`${this.baseUrl}/Claims/${claimId}`, { responseType: 'text' }).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const index = this.localClaims.findIndex(c => c.claimId === Number(claimId));
        if (index === -1) return throwError(() => new Error('Claim not found.'));
        if (this.localClaims[index].claimStatus !== 'Registered') {
          return throwError(() => new Error('Only registered claims can be deleted.'));
        }
        this.localClaims.splice(index, 1);
        return of('Claim deleted successfully.');
      })
    );
  }

  // ==========================================
  // 2. Documents (PDF Section 2 & 5: ClaimDocumentsController)
  // ==========================================

  addDocument(claimId: number, dto: CreateClaimDocumentDto): Observable<ClaimDocumentResponseDto> {
    return this.http.post<ClaimDocumentResponseDto>(`${this.baseUrl}/claims/${claimId}/documents`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const newDoc: ClaimDocumentResponseDto = {
          documentId: this.localDocuments.length + 1,
          claimId: Number(claimId),
          documentName: dto.documentName,
          filePath: dto.filePath || null,
          verificationStatus: 'Pending',
          uploadedDate: new Date().toISOString()
        };
        this.localDocuments.push(newDoc);
        return of(newDoc);
      })
    );
  }

  getDocuments(claimId: number): Observable<ClaimDocumentResponseDto[]> {
    return this.http.get<ClaimDocumentResponseDto[]>(`${this.baseUrl}/claims/${claimId}/documents`).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        return of(this.localDocuments.filter(d => d.claimId === Number(claimId)));
      })
    );
  }

  verifyDocuments(claimId: number, documentIds: number[]): Observable<ClaimDocumentResponseDto[]> {
    return this.http.put<ClaimDocumentResponseDto[]>(`${this.baseUrl}/claims/${claimId}/documents/verify`, documentIds).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const updated: ClaimDocumentResponseDto[] = [];
        this.localDocuments.forEach(d => {
          if (d.claimId === Number(claimId) && documentIds.includes(d.documentId)) {
            d.verificationStatus = 'Verified';
            updated.push(d);
          }
        });
        return of(updated);
      })
    );
  }

  // ==========================================
  // 3. Surveys (PDF Section 2 & 5: SurveysController)
  // ==========================================

  assignSurveyor(claimId: number, dto: CreateSurveyDto): Observable<SurveyResponseDto> {
    return this.http.post<SurveyResponseDto>(`${this.baseUrl}/claims/${claimId}/survey`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const survey: SurveyResponseDto = {
          surveyId: this.localSurveys.length + 1,
          claimId: Number(claimId),
          surveyorId: dto.surveyorId,
          assignedDate: new Date().toISOString(),
          inspectionDate: dto.inspectionDate || null,
          reportPath: null,
          remarks: null,
          surveyStatus: 'Assigned'
        };
        this.localSurveys.push(survey);
        return of(survey);
      })
    );
  }

  getSurveyDetails(claimId: number): Observable<SurveyResponseDto[]> {
    return this.http.get<SurveyResponseDto[]>(`${this.baseUrl}/claims/${claimId}/survey`).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        return of(this.localSurveys.filter(s => s.claimId === Number(claimId)));
      })
    );
  }

  submitSurveyReport(surveyId: number, dto: SurveyReportDto): Observable<SurveyResponseDto> {
    return this.http.put<SurveyResponseDto>(`${this.baseUrl}/surveys/${surveyId}`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const survey = this.localSurveys.find(s => s.surveyId === Number(surveyId));
        if (!survey) return throwError(() => new Error('Survey assignment not found.'));
        survey.inspectionDate = dto.inspectionDate || survey.inspectionDate;
        survey.reportPath = dto.reportPath || null;
        survey.remarks = dto.remarks;
        survey.surveyStatus = 'Completed';
        return of(survey);
      })
    );
  }

  // ==========================================
  // 4. Investigations (PDF Section 2 & 5: InvestigationsController)
  // ==========================================

  assignInvestigator(claimId: number, dto: CreateInvestigationDto): Observable<InvestigationResponseDto> {
    return this.http.post<InvestigationResponseDto>(`${this.baseUrl}/claims/${claimId}/investigation`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const inv: InvestigationResponseDto = {
          investigationId: this.localInvestigations.length + 1,
          claimId: Number(claimId),
          investigatorId: dto.investigatorId || null,
          investigationReason: dto.investigationReason,
          investigationStatus: 'Pending',
          createdDate: new Date().toISOString(),
          assignedDate: new Date().toISOString(),
          completedDate: null,
          reportPath: null
        };
        this.localInvestigations.push(inv);
        return of(inv);
      })
    );
  }

  submitInvestigationReport(id: number, dto: InvestigationReportDto): Observable<InvestigationResponseDto> {
    return this.http.put<InvestigationResponseDto>(`${this.baseUrl}/investigations/${id}`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const inv = this.localInvestigations.find(i => i.investigationId === Number(id));
        if (!inv) return throwError(() => new Error('Investigation not found.'));
        inv.reportPath = dto.reportPath;
        inv.investigationStatus = 'Completed';
        inv.completedDate = new Date().toISOString();
        return of(inv);
      })
    );
  }

  // ==========================================
  // 5. Assessments (PDF Section 2 & 5: ClaimAssessmentController)
  // ==========================================

  addAssessment(claimId: number, dto: CreateAssessmentDto): Observable<AssessmentResponseDto> {
    return this.http.post<AssessmentResponseDto>(`${this.baseUrl}/claims/${claimId}/assessment`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const assessment: AssessmentResponseDto = {
          assessmentId: this.localAssessments.length + 1,
          claimId: Number(claimId),
          assessedAmount: dto.assessedAmount,
          deductible: dto.deductible,
          depreciation: dto.depreciation,
          nonPayable: dto.nonPayable,
          netPayable: dto.netPayable
        };
        const existingIdx = this.localAssessments.findIndex(a => a.claimId === Number(claimId));
        if (existingIdx >= 0) {
          this.localAssessments[existingIdx] = assessment;
        } else {
          this.localAssessments.push(assessment);
        }
        return of(assessment);
      })
    );
  }

  getAssessment(claimId: number): Observable<AssessmentResponseDto> {
    return this.http.get<AssessmentResponseDto>(`${this.baseUrl}/claims/${claimId}/assessment`).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const assessment = this.localAssessments.find(a => a.claimId === Number(claimId));
        return assessment ? of(assessment) : throwError(() => new Error('Assessment not found.'));
      })
    );
  }

  // ==========================================
  // 6. Approval & Reject (PDF Section 2 & 5: ClaimSettlementController)
  // ==========================================

  approveClaim(claimId: number): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.baseUrl}/claims/${claimId}/approve`, {}).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const claim = this.localClaims.find(c => c.claimId === Number(claimId));
        if (!claim) return throwError(() => new Error('Claim not found.'));
        if (claim.claimStatus === 'Rejected') return throwError(() => new Error('A rejected claim cannot be approved.'));
        if (claim.claimStatus === 'Settled') return throwError(() => new Error('A settled claim cannot be approved again.'));
        if (claim.claimStatus === 'Approved') return throwError(() => new Error('Claim is already approved.'));
        claim.claimStatus = 'Approved';
        return of({ message: 'Claim approved successfully.' });
      })
    );
  }

  rejectClaim(claimId: number): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.baseUrl}/claims/${claimId}/reject`, {}).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const claim = this.localClaims.find(c => c.claimId === Number(claimId));
        if (!claim) return throwError(() => new Error('Claim not found.'));
        if (claim.claimStatus === 'Settled') return throwError(() => new Error('A settled claim cannot be rejected.'));
        if (claim.claimStatus === 'Rejected') return throwError(() => new Error('Claim is already rejected.'));
        claim.claimStatus = 'Rejected';
        return of({ message: 'Claim rejected successfully.' });
      })
    );
  }

  // ==========================================
  // 7. Settlement & Payout (PDF Section 2 & 5: ClaimSettlementController)
  // ==========================================

  processSettlement(claimId: number, dto: SettlementDTO): Observable<SettlementResponseDTO> {
    return this.http.post<SettlementResponseDTO>(`${this.baseUrl}/claims/${claimId}/settlement`, dto).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const claim = this.localClaims.find(c => c.claimId === Number(claimId));
        if (!claim) return throwError(() => new Error('Claim not found.'));
        if (claim.claimStatus !== 'Approved') return throwError(() => new Error('Only approved claims can be settled.'));
        
        const assessment = this.localAssessments.find(a => a.claimId === Number(claimId));
        if (!assessment) return throwError(() => new Error('Claim assessment not found.'));
        if (assessment.netPayable <= 0) return throwError(() => new Error('Net payable amount must be greater than zero.'));

        const allowedModes = ['NEFT', 'RTGS', 'IMPS', 'UPI', 'Cheque'];
        if (!allowedModes.includes(dto.paymentMode)) {
          return throwError(() => new Error('Payment mode must be NEFT, RTGS, IMPS, UPI, or Cheque.'));
        }

        const now = new Date();
        const settlement: SettlementResponseDTO = {
          settlementId: this.localSettlements.length + 1,
          claimId: Number(claimId),
          approvedAmount: assessment.netPayable,
          paymentMode: dto.paymentMode,
          transactionReference: `TXN-${claimId}-${now.getTime()}`,
          settlementDate: now.toISOString(),
          settlementStatus: 'Settled'
        };
        this.localSettlements.push(settlement);
        claim.claimStatus = 'Settled';
        return of(settlement);
      })
    );
  }

  getSettlement(claimId: number): Observable<SettlementResponseDTO> {
    return this.http.get<SettlementResponseDTO>(`${this.baseUrl}/claims/${claimId}/settlement`).pipe(
      tap(() => this.backendOnlineSignal.set(true)),
      catchError((err: HttpErrorResponse) => {
        if (err.status !== 0) {
          return throwError(() => new Error(extractErrorMessage(err)));
        }
        this.backendOnlineSignal.set(false);
        const found = this.localSettlements.find(s => s.claimId === Number(claimId));
        return found ? of(found) : throwError(() => new Error('Settlement not found.'));
      })
    );
  }

  // ==========================================
  // Helper / Tracking Timeline (Section 1 Flow)
  // ==========================================

  getClaimTrackingSteps(claimIdOrNumber: number | string): Observable<ClaimTrackingStep[]> {
    return new Observable(obs => {
      this.getAllClaims().subscribe({
        next: (claims) => {
          const claim = claims.find(c => 
            c.claimId === Number(claimIdOrNumber) || 
            c.claimNumber.toUpperCase() === String(claimIdOrNumber).trim().toUpperCase()
          );

          if (!claim) {
            obs.next([]);
            obs.complete();
            return;
          }

          const isRegistered = !!claim;
          const isApproved = claim.claimStatus === 'Approved' || claim.claimStatus === 'Settled';
          const isSettled = claim.claimStatus === 'Settled';

          const steps: ClaimTrackingStep[] = [
            {
              stepNumber: 1,
              title: '1. Registered',
              description: `Claim #${claim.claimNumber} (Policy ID: ${claim.policyId}, Customer ID: ${claim.customerId})`,
              completed: isRegistered,
              active: claim.claimStatus === 'Registered',
              date: claim.claimDate
            },
            {
              stepNumber: 2,
              title: '2. Documents Uploaded & Verified',
              description: 'Supporting documents pending or verified by claims officer',
              completed: isApproved || isSettled,
              active: claim.claimStatus === 'Registered'
            },
            {
              stepNumber: 3,
              title: '3. Surveyor Assigned & Report Submitted',
              description: 'Surveyor assigned to inspect incident and submit field report',
              completed: isApproved || isSettled,
              active: false
            },
            {
              stepNumber: 4,
              title: '4. Special Investigation (Optional)',
              description: 'Special Investigation Unit (SIU) review for flagged claims',
              completed: isApproved || isSettled,
              active: false
            },
            {
              stepNumber: 5,
              title: '5. Loss Assessment Calculation',
              description: 'NetPayable = AssessedAmount - Deductible - Depreciation - NonPayable',
              completed: isApproved || isSettled,
              active: false
            },
            {
              stepNumber: 6,
              title: '6. Senior Officer Decision',
              description: isApproved ? 'Claim Approved' : (claim.claimStatus === 'Rejected' ? 'Claim Rejected' : 'Awaiting Approval / Rejection'),
              completed: isApproved,
              active: claim.claimStatus === 'Approved'
            },
            {
              stepNumber: 7,
              title: '7. Settlement & Payout',
              description: isSettled ? 'Disbursed via electronic fund transfer' : 'Awaiting payout disbursement',
              completed: isSettled,
              active: isApproved && !isSettled
            }
          ];

          obs.next(steps);
          obs.complete();
        },
        error: () => {
          obs.next([]);
          obs.complete();
        }
      });
    });
  }

  // Compatibility aliases
  getClaims(): Observable<ClaimResponseDto[]> {
    return this.getAllClaims();
  }
}
