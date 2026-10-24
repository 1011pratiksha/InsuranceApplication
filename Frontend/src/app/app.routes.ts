import { Routes } from '@angular/router';
import { CustomerRegistrationComponent } from './features/customer-registration/customer-registration/customer-registration.component';

export const routes: Routes = [
  { path: '', component: CustomerRegistrationComponent },
  { path: 'customer-registration', component: CustomerRegistrationComponent },
  { path: 'customer/c-profile', component: CustomerRegistrationComponent },
  { path: 'c-profile', component: CustomerRegistrationComponent }
];

