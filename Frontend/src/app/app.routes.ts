
import { Routes } from '@angular/router';
import { CustomerRegistrationComponent } from './features/customer-registration/customer-registration/customer-registration.component';
import { PolicyIssueComponent } from './features/policy-issuance/admin/policy-issue/policy-issue.component';
import { MyPoliciesComponent } from './features/policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from './features/policy-issuance/customer/policy-details/policy-details.component';
import { RenewalsDueComponent } from './features/policy-renewal/renewals-due.component';

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
  },
  {
    path: 'admin/renewals-due',
    component: RenewalsDueComponent
  }
    
  { path: '', component: CustomerRegistrationComponent },
  { path: 'customer-registration', component: CustomerRegistrationComponent },
  { path: 'customer/c-profile', component: CustomerRegistrationComponent },
  { path: 'c-profile', component: CustomerRegistrationComponent }
];
