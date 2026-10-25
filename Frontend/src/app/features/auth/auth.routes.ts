import { Routes } from '@angular/router';
import { AuthScreenComponent } from './auth-screen.component';
import { CustomerRegistrationComponent } from '../customer-registration/customer-registration/customer-registration.component';

export const AUTH_ROUTES: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: AuthScreenComponent },
  { path: 'register', component: CustomerRegistrationComponent },
  { path: '2fa', component: AuthScreenComponent }
];
