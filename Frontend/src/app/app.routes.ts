import { Routes } from '@angular/router';
import { AuditLogs } from './features/audit-logs/audit-logs';
import { CustomerSupport } from './features/customer-support/customer-support';
import { AgentSupport } from './features/agent-support/agent-support';
import { Support } from './features/support/support';
import { CustomerRegistrationComponent } from './features/customer-registration/customer-registration/customer-registration.component';
import { PolicyIssueComponent } from './features/policy-issuance/admin/policy-issue/policy-issue.component';
import { MyPoliciesComponent } from './features/policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from './features/policy-issuance/customer/policy-details/policy-details.component';

export const routes: Routes = [
  {
    path: 'admin/policy-issuance',
    component: PolicyIssueComponent,
  },
  {
    path: 'customer/my-policies',
    component: MyPoliciesComponent,
  },
  {
    path: 'customer/policy-details/:id',
    component: PolicyDetailsComponent,
  },
  {
    path: 'audit-logs',
    component: AuditLogs,
  },
  {
    path: 'support',
    component: Support,
  },
  {
    path: 'customer-support',
    component: CustomerSupport,
  },
  {
    path: 'agent-support',
    component: AgentSupport,
  },

  { path: '', component: CustomerRegistrationComponent },
  { path: 'customer-registration', component: CustomerRegistrationComponent },
  { path: 'customer/c-profile', component: CustomerRegistrationComponent },
  { path: 'c-profile', component: CustomerRegistrationComponent },
];
