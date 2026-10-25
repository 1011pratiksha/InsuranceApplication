
import { Routes } from '@angular/router';

// Existing Policy Issuance pages
import { PolicyIssueComponent } from './features/policy-issuance/admin/policy-issue/policy-issue.component';
import { MyPoliciesComponent } from './features/policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from './features/policy-issuance/customer/policy-details/policy-details.component';

// KYC Admin pages
import { KycAdminDashboardComponent } from './features/kyc-verification/admin/kyc-dashboard/kyc-dashboard.component';
import { KycRequestsComponent } from './features/kyc-verification/admin/kyc-requests/kyc-requests.component';

// KYC Customer page
import { KycSubmitComponent } from './features/kyc-verification/customer/kyc-submit/kyc-submit.component';

export const routes: Routes = [
  // Open Customer KYC Submission when visiting the root URL
  {
    path: '',
    redirectTo: 'kyc/customer/submit',
    pathMatch: 'full'
  },

  // Existing Policy Issuance routes
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

  // KYC Admin routes
  {
    path: 'kyc/admin/dashboard',
    component: KycAdminDashboardComponent
  },
  {
    path: 'kyc/admin/requests',
    component: KycRequestsComponent
  },

  // KYC Customer route
  {
    path: 'kyc/customer/submit',
    component: KycSubmitComponent
  },

  // Redirect unknown URLs to the Customer KYC Submission page
  {
    path: '**',
    redirectTo: 'kyc/customer/submit'
  }
];
