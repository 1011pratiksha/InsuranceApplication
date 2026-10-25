
import { Routes } from '@angular/router';
import { MainLayoutComponent } from './shared/layout/main-layout.component';
import { roleGuard } from './core/guards/role.guard';

// Product Management, Quote Generation and Underwriting
import { ProductManagement } from './features/product-management/product-management';
import { QuoteWorkspace } from './features/quote-workspace/quote-workspace';
import { UnderwritingQueue } from './features/underwriting/underwriting-queue/underwriting-queue';
import { UnderwritingCaseReview } from './features/underwriting/underwriting-case-review/underwriting-case-review';
import { ProposalForm } from './features/underwriting/proposal-form/proposal-form';

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

export const routes: Routes = [
  // Authentication pages outside the main layout
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'two-factor', component: TwoFactor },
  { path: 'forgot-password', component: ForgotPassword },
  { path: 'reset-password', component: ResetPassword },
  { path: 'login-success', component: LoginSuccess },

  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },

  // Main application layout
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      // Product Management
      {
        path: 'admin/product-management',
        component: ProductManagement,
        canActivate: [roleGuard('admin')]
      },

      // Quote Generation - Agent
      {
        path: 'agent/quote-new',
        component: QuoteWorkspace,
        canActivate: [roleGuard('agent')],
        data: { role: 'agent', page: 'new' }
      },
      {
        path: 'agent/quotes',
        component: QuoteWorkspace,
        canActivate: [roleGuard('agent')],
        data: { role: 'agent', page: 'list' }
      },
      {
        path: 'agent/quotes/generate',
        component: QuoteWorkspace,
        canActivate: [roleGuard('agent')],
        data: { role: 'agent', page: 'new' }
      },
      {
        path: 'agent/quote-result/:id',
        component: QuoteWorkspace,
        canActivate: [roleGuard('agent')],
        data: { role: 'agent', page: 'result' }
      },
      {
        path: 'agent/quote-details/:id',
        component: QuoteWorkspace,
        canActivate: [roleGuard('agent')],
        data: { role: 'agent', page: 'detail' }
      },

      // Quote Generation - Admin
      {
        path: 'admin/quote-generation',
        component: QuoteWorkspace,
        canActivate: [roleGuard('admin')],
        data: { role: 'admin', page: 'list' }
      },
      {
        path: 'admin/quote-review/:id',
        component: QuoteWorkspace,
        canActivate: [roleGuard('admin')],
        data: { role: 'admin', page: 'review' }
      },

      // Quote Generation - Customer
      {
        path: 'customer/quotes',
        component: QuoteWorkspace,
        canActivate: [roleGuard('customer')],
        data: { role: 'customer', page: 'accept' }
      },
      {
        path: 'customer/quote-details/:id',
        component: QuoteWorkspace,
        canActivate: [roleGuard('customer')],
        data: { role: 'customer', page: 'detail' }
      },

      // Underwriting
      {
        path: 'admin/underwriting',
        component: UnderwritingQueue,
        canActivate: [roleGuard('admin')]
      },
      {
        path: 'admin/uw-case/:id',
        component: UnderwritingCaseReview,
        canActivate: [roleGuard('admin')]
      },
      {
        path: 'admin/underwriting/proposal',
        component: ProposalForm,
        canActivate: [roleGuard('admin')]
      },

      // Role-based lazy-loaded routes
      {
        path: 'customer',
        canActivate: [roleGuard('customer')],
        loadChildren: () =>
          import('./features/customer/customer.routes').then(m => m.CUSTOMER_ROUTES)
      },
      {
        path: 'agent',
        canActivate: [roleGuard('agent')],
        loadChildren: () =>
          import('./features/Agent/agent.routes').then(m => m.AGENT_ROUTES)
      },
      {
        path: 'admin',
        canActivate: [roleGuard('admin')],
        loadChildren: () =>
          import('./features/admin/admin.routes').then(m => m.ADMIN_ROUTES)
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

      // Agent Dashboard and Pages
      {
        path: 'agent/dashboard',
        component: AgentDashboard
      },
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

      // Admin Dashboard and Claims
      {
        path: 'admin/dashboard',
        component: ClaimsDashboard
      },
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

      // General Claims
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
      }
    ]
  },

  // Always keep wildcard route last
  { path: '**', redirectTo: 'login' }
];
