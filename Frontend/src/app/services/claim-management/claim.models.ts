// Models defined according to Section 7 of Insurance Claim Management System Specification

export interface CreateClaimDto {
  policyId: number;
  customerId: number;
  claimType: string;
  incidentDate: string; // ISO format: YYYY-MM-DD
  estimatedLoss: number;
}

export interface UpdateClaimDto {
  claimType: string;
  incidentDate: string;
  estimatedLoss: number;
}

export interface ClaimResponseDto {
  claimId: number;
  claimNumber: string;
  policyId: number;
  customerId: number;
  claimType: string;
  incidentDate: string;
  claimDate: string;
  estimatedLoss: number;
  claimStatus: 'Registered' | 'Approved' | 'Rejected' | 'Settled';
}

export type Claim = ClaimResponseDto;

export interface CreateClaimDocumentDto {
  documentName: string;
  filePath?: string | null;
}

export interface ClaimDocumentResponseDto {
  documentId: number;
  claimId: number;
  documentName: string;
  filePath?: string | null;
  verificationStatus: 'Pending' | 'Verified';
  uploadedDate: string;
}

export interface CreateSurveyDto {
  surveyorId: number;
  inspectionDate?: string | null;
}

export interface SurveyReportDto {
  inspectionDate?: string | null;
  reportPath?: string | null;
  remarks: string;
}

export interface SurveyResponseDto {
  surveyId: number;
  claimId: number;
  surveyorId: number;
  assignedDate: string;
  inspectionDate?: string | null;
  reportPath?: string | null;
  remarks?: string | null;
  surveyStatus: 'Assigned' | 'Completed';
}

export interface CreateInvestigationDto {
  investigatorId?: number | null;
  investigationReason: string;
}

export interface InvestigationReportDto {
  reportPath: string;
}

export interface InvestigationResponseDto {
  investigationId: number;
  claimId: number;
  investigatorId?: number | null;
  investigationReason: string;
  investigationStatus: 'Pending' | 'Completed';
  createdDate: string;
  assignedDate?: string | null;
  completedDate?: string | null;
  reportPath?: string | null;
}

export interface CreateAssessmentDto {
  assessedAmount: number;
  deductible: number;
  depreciation: number;
  nonPayable: number;
  netPayable: number;
}

export interface AssessmentResponseDto {
  assessmentId: number;
  claimId: number;
  assessedAmount: number;
  deductible: number;
  depreciation: number;
  nonPayable: number;
  netPayable: number;
}

export interface SettlementDTO {
  paymentMode: 'NEFT' | 'RTGS' | 'IMPS' | 'UPI' | 'Cheque';
}

export interface SettlementResponseDTO {
  settlementId: number;
  claimId: number;
  approvedAmount: number;
  paymentMode: string;
  transactionReference: string;
  settlementDate: string;
  settlementStatus: string;
}

// UI / Tracking helpers
export interface ClaimTrackingStep {
  stepNumber: number;
  title: string;
  description: string;
  completed: boolean;
  active: boolean;
  date?: string;
}
