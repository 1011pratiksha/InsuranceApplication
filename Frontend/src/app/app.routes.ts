import { Routes } from '@angular/router';
import { CustomerRegistrationComponent } from './features/customer-registration/customer-registration/customer-registration.component';
import { PolicyIssueComponent } from './features/policy-issuance/admin/policy-issue/policy-issue.component';
import { MyPoliciesComponent } from './features/policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from './features/policy-issuance/customer/policy-details/policy-details.component';
import { Policyserviceingss } from './features/policy-servicing/policyserviceingss/policyserviceingss';
import { Adminservicerequest } from './features/policy-servicing/adminservicerequest/adminservicerequest';
import { CustomerEndorsement } from './features/policy-servicing/customer-endorsementng/customer-endorsementng';
import { Policyclosure } from './features/policy-closure/policyclosure/policyclosure';

import { ClaimsDashboard } from './features/claim-management/claims-dashboard/claims-dashboard';
import { ClaimRegistration } from './features/claim-management/claim-registration/claim-registration';
import { ClaimList } from './features/claim-management/claim-list/claim-list';
import { ClaimDetails } from './features/claim-management/claim-details/claim-details';
import { DocumentUpload } from './features/claim-management/document-upload/document-upload';
import { SurveyManagement } from './features/claim-management/survey-management/survey-management';
import { InvestigationManagement } from './features/claim-management/investigation-management/investigation-management';
import { ClaimAssessmentComponent } from './features/claim-management/claim-assessment/claim-assessment';
import { ClaimApproval } from './features/claim-management/claim-approval/claim-approval';
import { ClaimSettlement } from './features/claim-management/claim-settlement/claim-settlement';
import { ClaimTracking } from './features/claim-management/claim-tracking/claim-tracking';

export const routes: Routes = [
  { path: '', redirectTo: 'customer/dashboard', pathMatch: 'full' },

  // Customer Registration & Profile
  { path: 'customer-registration', component: CustomerRegistrationComponent },
  { path: 'customer/c-profile', component: CustomerRegistrationComponent },
  { path: 'c-profile', component: CustomerRegistrationComponent },

  // Policy Issuance
  { path: 'admin/policy-issuance', component: PolicyIssueComponent },
  { path: 'customer/my-policies', component: MyPoliciesComponent },
  { path: 'customer/policy-details/:id', component: PolicyDetailsComponent },

  // Policy Servicing
  { path: 'customer/svc', component: Policyserviceingss },
  { path: 'admin/service-requests', component: Adminservicerequest },
  { path: 'customer/endoresement', component: CustomerEndorsement },

  // Policy Closure
  { path: 'admin/closure', component: Policyclosure },

  // ==========================================
  // 1. CUSTOMER ROUTES (Claims Management)
  // ==========================================
  { path: 'customer/dashboard', component: ClaimsDashboard },
  { path: 'customer/claims/register', component: ClaimRegistration },
  { path: 'customer/claims/list', component: ClaimList },
  { path: 'customer/claims/documents', component: DocumentUpload },
  { path: 'customer/claims/tracking', component: ClaimTracking },
  { path: 'customer/claims/details/:id', component: ClaimDetails },

  // ==========================================
  // 2. AGENT ROUTES (Claims Management)
  // ==========================================
  { path: 'agent/dashboard', component: ClaimsDashboard },
  { path: 'agent/claims/register', component: ClaimRegistration },
  { path: 'agent/claims/list', component: ClaimList },
  { path: 'agent/claims/tracking', component: ClaimTracking },
  { path: 'agent/claims/details/:id', component: ClaimDetails },

  // ==========================================
  // 3. ADMIN ROUTES (Claims Management)
  // ==========================================
  { path: 'admin/dashboard', component: ClaimsDashboard },
  { path: 'admin/claims/list', component: ClaimList },
  { path: 'admin/claims/details/:id', component: ClaimDetails },
  { path: 'admin/claims/documents', component: DocumentUpload },
  { path: 'admin/claims/survey', component: SurveyManagement },
  { path: 'admin/claims/investigation', component: InvestigationManagement },
  { path: 'admin/claims/assessment', component: ClaimAssessmentComponent },
  { path: 'admin/claims/approval', component: ClaimApproval },
  { path: 'admin/claims/settlement', component: ClaimSettlement },
  { path: 'admin/claims/tracking', component: ClaimTracking },

  // Direct shortcuts
  { path: 'claims/dashboard', component: ClaimsDashboard },
  { path: 'claims/register', component: ClaimRegistration },
  { path: 'claims/list', component: ClaimList },
  { path: 'claims/details/:id', component: ClaimDetails },
  { path: 'claims/details', component: ClaimDetails },
  { path: 'claims/documents', component: DocumentUpload },
  { path: 'claims/survey', component: SurveyManagement },
  { path: 'claims/investigation', component: InvestigationManagement },
  { path: 'claims/assessment', component: ClaimAssessmentComponent },
  { path: 'claims/approval', component: ClaimApproval },
  { path: 'claims/settlement', component: ClaimSettlement },
  { path: 'claims/tracking', component: ClaimTracking },

  // Wildcard fallback
  { path: '**', redirectTo: 'customer/dashboard' }
];
