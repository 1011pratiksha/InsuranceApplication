
import { Routes } from '@angular/router';
import { PremiumSchedule } from './features/premium-schedule/premium-schedule';
import { PayPremium } from './features/premium-schedule/pay-premium/pay-premium';
import { PaymentHistory } from './features/premium-schedule/payment-history/payment-history';
import { Receipts } from './features/premium-schedule/receipts/receipts';
import { RefundStatus } from './features/premium-schedule/refund-status/refund-status';
import { AdminPremiumSchedules } from './features/premium-schedule/admin-premium-schedules/admin-premium-schedules';
import { AdminPaymentVerification } from './features/premium-schedule/admin-payment-verification/admin-payment-verification';
import { AdminLateFeeConfig } from './features/premium-schedule/admin-late-fee-config/admin-late-fee-config';
import { AdminReminders } from './features/premium-schedule/admin-reminders/admin-reminders';
import { AdminRefunds } from './features/premium-schedule/admin-refunds/admin-refunds';


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

  // ==========================================
  // 1. CUSTOMER ROUTES
  // ==========================================
  { path: 'customer/dashboard', component: ClaimsDashboard },
  { path: 'customer/claims/register', component: ClaimRegistration },
  { path: 'customer/claims/list', component: ClaimList },
  { path: 'customer/claims/documents', component: DocumentUpload },
  { path: 'customer/claims/tracking', component: ClaimTracking },
  { path: 'customer/claims/details/:id', component: ClaimDetails },

  // ==========================================
  // 2. AGENT ROUTES
  // ==========================================
  { path: 'agent/dashboard', component: ClaimsDashboard },
  { path: 'agent/claims/register', component: ClaimRegistration },
  { path: 'agent/claims/list', component: ClaimList },
  { path: 'agent/claims/tracking', component: ClaimTracking },
  { path: 'agent/claims/details/:id', component: ClaimDetails },

  // ==========================================
  // 3. ADMIN ROUTES
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

  { path: '**', redirectTo: 'customer/dashboard' },
  {path :'customer/endoresement' , component: CustomerEndorsement},
 {
  path: 'admin/closure',
  component: Policyclosure
},
    
  { path: '', component: CustomerRegistrationComponent },
  { path: 'customer-registration', component: CustomerRegistrationComponent },
  { path: 'customer/c-profile', component: CustomerRegistrationComponent },
  { path: 'c-profile', component: CustomerRegistrationComponent },


 //Premium Payment And Schedule part

  { path: 'premium-schedule', component: PremiumSchedule },
  { path: 'pay-premium', component: PayPremium },
{ path: 'payment-history', component: PaymentHistory },
{ path: 'receipts', component: Receipts },
{ path: 'refund-status', component: RefundStatus },
{ path: 'admin/premium-schedules', component: AdminPremiumSchedules },
{ path: 'admin/payment-verification', component: AdminPaymentVerification },
{ path: 'admin/late-fee-config', component: AdminLateFeeConfig },
{ path: 'admin/reminders', component: AdminReminders },
{ path: 'admin/refunds', component: AdminRefunds },
  { path: '**', redirectTo: 'premium-schedule' },


   {
    path: 'admin/policy-issuance',
    component: PolicyIssueComponent
  },
  {
    path: 'customer/my-policies',
    component: MyPoliciesComponent
  },
  {
    path: 'customer/policy-details/:id',
    component: PolicyDetailsComponent
  },

  { path: 'customer/svc', component: Policyserviceingss},
  {path: 'admin/service-requests', component: Adminservicerequest},
];
