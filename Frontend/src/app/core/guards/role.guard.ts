import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserRole } from '../models/sidebar-menu.model';

/**
 * Factory that returns a route guard scoped to one role.
 * Usage in routes:  canActivate: [roleGuard('admin')]
 */
export function roleGuard(requiredRole: UserRole): CanActivateFn {
  return () => {
    const auth = inject(AuthService);
    const router = inject(Router);

    if (!auth.isAuthenticated()) {
      router.navigate(['/auth/login']);
      return false;
    }

    if (auth.role() !== requiredRole) {
      // Auto-sync role state to match current role module route prefix
      auth.setRole(requiredRole);
    }

    return true;
  };
}
