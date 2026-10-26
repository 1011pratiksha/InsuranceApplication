
import { Routes } from '@angular/router';
import { roleGuard } from './core/guards/role.guard';

// Layout
import { MainLayoutComponent } from './shared/layout/main-layout.component';

// Authentication
import { Login } from './features/authentication/login/login';
import { TwoFactor } from './features/authentication/two-factor/two-factor';
import { ForgotPassword } from './features/authentication/forgot-password/forgot-password';
import { ResetPassword } from './features/authentication/reset-password/reset-password';
import { LoginSuccess } from './features/authentication/login-success/login-success';

// Audit and Support
import { AuditLogs } from './features/audit-logs/audit-logs';
import { CustomerSupport } from './features/customer-support/customer-support';
import { AgentSupport } from './features/agent-support/agent-support';
import { Support } from './features/support/support';

// Claims
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

// Customer Registration
import { CustomerRegistrationComponent } from './features/customer-registration/customer-registration/customer-registration.component';

// Policy Issuance
import { PolicyIssueComponent } from './features/policy-issuance/admin/policy-issue/policy-issue.component';
import { MyPoliciesComponent } from './features/policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from './features/policy-issuance/customer/policy-details/policy-details.component';

// KYC
import { KycAdminDashboardComponent } from './features/kyc-verification/admin/kyc-dashboard/kyc-dashboard.component';
import { KycRequestsComponent } from './features/kyc-verification/admin/kyc-requests/kyc-requests.component';
import { KycSubmitComponent } from './features/kyc-verification/customer/kyc-submit/kyc-submit.component';

// Policy Servicing
import { Policyserviceingss } from './features/policy-servicing/policyserviceingss/policyserviceingss';
import { Adminservicerequest } from './features/policy-servicing/adminservicerequest/adminservicerequest';
import { CustomerEndorsement } from './features/policy-servicing/customer-endorsementng/customer-endorsementng';

// Policy Closure
import { Policyclosure } from './features/policy-closure/policyclosure/policyclosure';

// Agent Module
import { AgentDashboard } from './features/Agent/agent-dashboard/agent-dashboard';
import { AssignedCustomersComponent } from './features/Agent/assigned-customers/assigned-customers';
import { Agent } from './features/Agent/agent/agent';
import { AgentPolicies } from './features/Agent/agent-policies/agent-policies';
import { CommissionRules } from './features/Agent/commission-rules/commission-rules';
import { AgentCommissions } from './features/Agent/agent-commissions/agent-commissions';

// Role Dashboards
import { AdminDashboard } from './features/admin-dashboard/admin-dashboard';
import { CustomerDashboard } from './features/customer-dashboard/customer-dashboard';

// Premium Payment and Schedule
import { PremiumSchedule } from './features/premium-schedule/premium-schedule/premium-schedule';
import { PayPremium } from './features/premium-schedule/pay-premium/pay-premium';
import { PaymentHistory } from './features/premium-schedule/payment-history/payment-history';
import { Receipts } from './features/premium-schedule/receipts/receipts';
import { RefundStatus } from './features/premium-schedule/refund-status/refund-status';
import { AdminPremiumSchedules } from './features/premium-schedule/admin-premium-schedules/admin-premium-schedules';
import { AdminPaymentVerification } from './features/premium-schedule/admin-payment-verification/admin-payment-verification';
import { AdminLateFeeConfig } from './features/premium-schedule/admin-late-fee-config/admin-late-fee-config';
import { AdminReminders } from './features/premium-schedule/admin-reminders/admin-reminders';
import { AdminRefunds } from './features/premium-schedule/admin-refunds/admin-refunds';

export const routes: Routes = [
  // Root
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  // Authentication - outside the main layout
  { path: 'login', component: Login },
  { path: 'two-factor', component: TwoFactor },
  { path: 'forgot-password', component: ForgotPassword },
  { path: 'reset-password', component: ResetPassword },
  { path: 'login-success', component: LoginSuccess },
  { path: 'register', component: CustomerRegistrationComponent },

  // Role dashboards
  {
    path: 'admin/dashboard',
    component: AdminDashboard
  },
  {
    path: 'agent/dashboard',
    component: AgentDashboard
  },
  {
    path: 'customer/dashboard',
    component: CustomerDashboard
  },

  // KYC
  {
    path: 'kyc/admin/dashboard',
    component: KycAdminDashboardComponent
  },
  {
    path: 'kyc/admin/requests',
    component: KycRequestsComponent
  },
  {
    path: 'kyc/customer/submit',
    component: KycSubmitComponent
  },

  // Policy Issuance
  {
    path: 'admin/policies',
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

  // Policy Servicing
  {
    path: 'policy-servicing',
    component: Policyserviceingss
  },
  {
    path: 'admin/service-requests',
    component: Adminservicerequest
  },
  {
    path: 'customer/endorsements',
    component: CustomerEndorsement
  },

  // Policy Closure
  {
    path: 'policy-closure',
    component: Policyclosure
  },

  // Agent Module
  {
    path: 'agent/overview',
    component: Agent
  },
  {
    path: 'agent/assigned-customers',
    component: AssignedCustomersComponent
  },
  {
    path: 'agent/policies',
    component: AgentPolicies
  },
  {
    path: 'agent/commission-rules',
    component: CommissionRules
  },
  {
    path: 'agent/commissions',
    component: AgentCommissions
  },

  // Claims
  {
    path: 'claims',
    component: ClaimsDashboard
  },
  {
    path: 'claims/register',
    component: ClaimRegistration
  },
  {
    path: 'claims/list',
    component: ClaimList
  },
  {
    path: 'claims/details/:id',
    component: ClaimDetails
  },
  {
    path: 'claims/document-upload',
    component: DocumentUpload
  },
  {
    path: 'claims/survey',
    component: SurveyManagement
  },
  {
    path: 'claims/investigation',
    component: InvestigationManagement
  },
  {
    path: 'claims/assessment',
    component: ClaimAssessmentComponent
  },
  {
    path: 'claims/approval',
    component: ClaimApproval
  },
  {
    path: 'claims/settlement',
    component: ClaimSettlement
  },
  {
    path: 'claims/tracking',
    component: ClaimTracking
  },

  // Audit and Support
  {
    path: 'admin/audit-logs',
    component: AuditLogs
  },
  {
    path: 'customer/support',
    component: CustomerSupport
  },
  {
    path: 'agent/support',
    component: AgentSupport
  },
  {
    path: 'support',
    component: Support
  },

  // Premium Payment and Schedule
  {
    path: 'premium/schedule',
    component: PremiumSchedule
  },
  {
    path: 'premium/pay',
    component: PayPremium
  },
  {
    path: 'premium/history',
    component: PaymentHistory
  },
  {
    path: 'premium/receipts',
    component: Receipts
  },
  {
    path: 'premium/refund-status',
    component: RefundStatus
  },
  {
    path: 'admin/premium-schedules',
    component: AdminPremiumSchedules
  },
  {
    path: 'admin/payment-verification',
    component: AdminPaymentVerification
  },
  {
    path: 'admin/late-fee-config',
    component: AdminLateFeeConfig
  },
  {
    path: 'admin/reminders',
    component: AdminReminders
  },
  {
    path: 'admin/refunds',
    component: AdminRefunds
  },

  // Fallback
  {
    path: '**',
    redirectTo: 'login'
  }
];
