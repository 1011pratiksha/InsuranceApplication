import { Injectable, signal, computed } from '@angular/core';
import { UserRole, SidebarMenuItem } from '../models/sidebar-menu.model';
import { CUSTOMER_MENU, AGENT_MENU, ADMIN_MENU } from '../config/sidebar-menu.config';

/**
 * AuthService manages user authentication state and role.
 *
 * For now it provides a simple in-memory role switcher
 * so the sidebar can render correctly per role.
 * Replace the internals with real JWT / session logic later.
 */
@Injectable({ providedIn: 'root' })
export class AuthService {
  /** Current user role – defaults to 'customer' */
  private readonly _role = signal<UserRole>('customer');

  /** Whether the user is authenticated */
  private readonly _isAuthenticated = signal<boolean>(true);

  /** Display name */
  private readonly _userName = signal<string>('Tanmay');

  /** Read-only signals exposed to consumers */
  readonly role = this._role.asReadonly();
  readonly isAuthenticated = this._isAuthenticated.asReadonly();
  readonly userName = this._userName.asReadonly();

  /** Computed sidebar menu based on current role */
  readonly sidebarMenu = computed<SidebarMenuItem[]>(() => {
    switch (this._role()) {
      case 'agent':
        return AGENT_MENU;
      case 'admin':
        return ADMIN_MENU;
      default:
        return CUSTOMER_MENU;
    }
  });

  /** Computed role display label */
  readonly roleLabel = computed<string>(() => {
    switch (this._role()) {
      case 'agent':
        return 'Agent';
      case 'admin':
        return 'Administrator';
      default:
        return 'Customer';
    }
  });

  /** Switch role – useful for dev/testing and later for real auth */
  setRole(role: UserRole): void {
    this._role.set(role);
  }

  setUser(name: string): void {
    this._userName.set(name);
  }

  login(role: UserRole, name: string): void {
    this._role.set(role);
    this._userName.set(name);
    this._isAuthenticated.set(true);
  }

  logout(): void {
    this._isAuthenticated.set(false);
    this._userName.set('');
    this._role.set('customer');
  }
}
