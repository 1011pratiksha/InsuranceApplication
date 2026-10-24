import { Routes } from '@angular/router';
import { Policyserviceingss } from './features/policy-servicing/policyserviceingss/policyserviceingss';
import { Adminservicerequest } from './features/policy-servicing/adminservicerequest/adminservicerequest';

export const routes: Routes = [
  { path: 'svc', component: Policyserviceingss},
  {path: 'admin/service-requests', component: Adminservicerequest}
];