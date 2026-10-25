import { Routes } from '@angular/router';
import { MainLayoutComponent } from './shared/layout/main-layout.component';
import { roleGuard } from './core/guards/role.guard';

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

// Dashboards
import {AdminDashboard} from './features/admin-dashboard/admin-dashboard'
import {AgentDashboard} from './features/agent-dashboard/agent-dashboard'
import {CustomerDashboard} from './features/customer-dashboard/customer-dashboard'



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
  // ── Authentication pages (outside sidebar layout) ──
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: 'two-factor',
    component: TwoFactor
  },
  {
    path: 'forgot-password',
    component: ForgotPassword
  },
  {
    path: 'reset-password',
    component: ResetPassword
  },
  {
    path: 'login-success',
    component: LoginSuccess
  },
  {
    path: 'register',
    component: CustomerRegistrationComponent
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },

  // ── Application pages inside MainLayout with Role-based Sidebar ──
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      // Role-based modular navigation prefixes (from PDF)
      {
        path: 'customer',
        canActivate: [roleGuard('customer')],
        loadChildren: () => import('./features/customer/customer.routes').then(m => m.CUSTOMER_ROUTES)
      },
      {
        path: 'agent',
        canActivate: [roleGuard('agent')],
        loadChildren: () => import('./features/Agent/agent.routes').then(m => m.AGENT_ROUTES)
      },
      {
        path: 'admin',
        canActivate: [roleGuard('admin')],
        loadChildren: () => import('./features/admin/admin.routes').then(m => m.ADMIN_ROUTES)
      },

      // Customer Registration and Profile
      {
        path: 'customer-registration',
        component: CustomerRegistrationComponent
      },
      {
        path: 'customer/c-profile',
        component: CustomerRegistrationComponent
      },
      {
        path: 'c-profile',
        component: CustomerRegistrationComponent
      },

      // Customer Policy Issuance
      {
        path: 'customer/my-policies',
        component: MyPoliciesComponent
      },
      {
        path: 'customer/policy-details/:id',
        component: PolicyDetailsComponent
      },

      // Admin Policy Issuance
      {
        path: 'admin/policy-issuance',
        component: PolicyIssueComponent
      },

      // Policy Servicing
      {
        path: 'customer/svc',
        component: Policyserviceingss
      },
      {
        path: 'admin/service-requests',
        component: Adminservicerequest
      },
      {
        path: 'customer/endoresement',
        component: CustomerEndorsement
      },

      // Policy Closure
      {
        path: 'admin/closure',
        component: Policyclosure
      },

      // Customer Claims
      {
        path: 'customer/dashboard',
        component: ClaimsDashboard
      },
      {
        path: 'customer/claims/register',
        component: ClaimRegistration
      },
      {
        path: 'customer/claims/list',
        component: ClaimList
      },
      {
        path: 'customer/claims/documents',
        component: DocumentUpload
      },
      {
        path: 'customer/claims/tracking',
        component: ClaimTracking
      },
      {
        path: 'customer/claims/details/:id',
        component: ClaimDetails
      },

      // Agent Dashboard
      {
        path: 'agent/dashboard',
        component: AgentDashboard
      },

      // Agent Module Pages
      {
        path: 'agent/assigned-customers',
        component: AssignedCustomersComponent
      },
      {
        path: 'agent/profile',
        component: Agent
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

      // Agent Claims
      {
        path: 'agent/claims/register',
        component: ClaimRegistration
      },
      {
        path: 'agent/claims/list',
        component: ClaimList
      },
      {
        path: 'agent/claims/tracking',
        component: ClaimTracking
      },
      {
        path: 'agent/claims/details/:id',
        component: ClaimDetails
      },

      // Admin Dashboard
      {
        path: 'admin/dashboard',
        component: ClaimsDashboard
      },

      // Admin Claims
      {
        path: 'admin/claims/list',
        component: ClaimList
      },
      {
        path: 'admin/claims/details/:id',
        component: ClaimDetails
      },
      {
        path: 'admin/claims/documents',
        component: DocumentUpload
      },
      {
        path: 'admin/claims/survey',
        component: SurveyManagement
      },
      {
        path: 'admin/claims/investigation',
        component: InvestigationManagement
      },
      {
        path: 'admin/claims/assessment',
        component: ClaimAssessmentComponent
      },
      {
        path: 'admin/claims/approval',
        component: ClaimApproval
      },
      {
        path: 'admin/claims/settlement',
        component: ClaimSettlement
      },
      {
        path: 'admin/claims/tracking',
        component: ClaimTracking
      },

      // General Claims Routes
      {
        path: 'claims/dashboard',
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
        path: 'claims/details',
        component: ClaimDetails
      },
      {
        path: 'claims/documents',
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
        path: 'audit-logs',
        component: AuditLogs
      },
      {
        path: 'support',
        component: Support
      },
      {
        path: 'customer-support',
        component: CustomerSupport
      },
      {
        path: 'agent-support',
        component: AgentSupport
      },
       { path: 'admin', component: AdminDashboard },
       { path: 'agent', component: AgentDashboard },
       { path: 'customer', component: CustomerDashboard }
    ]
  },

  // Fallback for unknown URLs
  { 
    path: '**',
    redirectTo: 'login'
  },
        // Premium Payment and Schedule
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
];
