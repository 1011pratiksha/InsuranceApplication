
import { Routes } from '@angular/router';

import { MainLayout } from './shared/main-layout/main-layout';

import { CustomerRegistrationComponent } from './features/customer-registration/customer-registration/customer-registration.component';
import { PolicyIssueComponent } from './features/policy-issuance/admin/policy-issue/policy-issue.component';
import { MyPoliciesComponent } from './features/policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from './features/policy-issuance/customer/policy-details/policy-details.component';

import { Policyserviceingss } from './features/policy-servicing/policyserviceingss/policyserviceingss';
import { Adminservicerequest } from './features/policy-servicing/adminservicerequest/adminservicerequest';
import { CustomerEndorsement } from './features/policy-servicing/customer-endorsementng/customer-endorsementng';
import { Policyclosure } from './features/policy-closure/policyclosure/policyclosure';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
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
      {
        path: 'admin/closure',
        component: Policyclosure
      },
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
      }
    ]
  },
  {
    path: '',
    redirectTo: 'customer-registration',
    pathMatch: 'full'
  }
];
