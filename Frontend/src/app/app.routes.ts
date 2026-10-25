import { Routes } from '@angular/router';
import { MainLayoutComponent } from './shared/layout/main-layout.component';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  // Shared authentication screens outside sidebar layout
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },

  // Main application layout with role-based side navigation
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'customer',
        canActivate: [roleGuard('customer')],
        loadChildren: () => import('./features/customer/customer.routes').then(m => m.CUSTOMER_ROUTES)
      },
      {
        path: 'agent',
        canActivate: [roleGuard('agent')],
        loadChildren: () => import('./features/agent/agent.routes').then(m => m.AGENT_ROUTES)
      },
      {
        path: 'admin',
        canActivate: [roleGuard('admin')],
        loadChildren: () => import('./features/admin/admin.routes').then(m => m.ADMIN_ROUTES)
      },
      // Backward-compatibility aliases for legacy routes
      {
        path: 'customer-registration',
        redirectTo: 'auth/register',
        pathMatch: 'full'
      },
      {
        path: 'customer/c-profile',
        redirectTo: 'customer/profile/personal',
        pathMatch: 'full'
      },
      {
        path: 'c-profile',
        redirectTo: 'customer/profile/personal',
        pathMatch: 'full'
      },
      {
        path: 'kyc-verification',
        redirectTo: 'customer/kyc/submit',
        pathMatch: 'full'
      },
      {
        path: '',
        redirectTo: 'customer/dashboard',
        pathMatch: 'full'
      }
    ]
  },

  // Fallback for unmatched routes
  {
    path: '**',
    redirectTo: 'customer/dashboard'
  }
];
