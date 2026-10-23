
import { Routes } from '@angular/router';

import { PolicyIssueComponent } from './features/policy-issuance/admin/policy-issue/policy-issue.component';
import { MyPoliciesComponent } from './features/policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from './features/policy-issuance/customer/policy-details/policy-details.component';

export const routes: Routes = [
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
  }
];
