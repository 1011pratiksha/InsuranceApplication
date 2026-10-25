import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

import { CustomerSidebar } from '../customer-sidebar/customer-sidebar';
import { AdminSidebar } from '../admin-sidebar/admin-sidebar';
import { SidebarAgent } from '../sidebar-agent/sidebar-agent';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    CustomerSidebar,
    AdminSidebar,
    SidebarAgent
  ],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.scss'
})
export class MainLayout implements OnInit {
  userRole = '';

  constructor(
    @Inject(PLATFORM_ID) private platformId: object
  ) {}

  ngOnInit(): void {
    this.loadUserRole();
  }

  private loadUserRole(): void {
    // Prevent localStorage access during server-side rendering.
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const token = localStorage.getItem('token');

    if (!token) {
      this.userRole = '';
      return;
    }

    try {
      const payloadPart = token.split('.')[1];

      if (!payloadPart) {
        this.userRole = '';
        return;
      }

      const base64 = payloadPart
        .replace(/-/g, '+')
        .replace(/_/g, '/');

      const decoded = decodeURIComponent(
        atob(base64)
          .split('')
          .map(char =>
            '%' + char.charCodeAt(0).toString(16).padStart(2, '0')
          )
          .join('')
      );

      const payload = JSON.parse(decoded);

      const roleClaim =
        payload['role'] ??
        payload['roles'] ??
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

      const role = Array.isArray(roleClaim)
        ? roleClaim[0]
        : roleClaim;

      this.userRole = String(role ?? '')
        .trim()
        .toLowerCase();

    } catch (error) {
      this.userRole = '';
      console.error('Unable to read JWT role claim.', error);
    }
  }

  get isCustomer(): boolean {
    return this.userRole === 'customer';
  }

  get isAdmin(): boolean {
    return [
      'admin',
      'administrator',
      'super admin',
      'superadmin'
    ].includes(this.userRole);
  }

  get isAgent(): boolean {
    return this.userRole === 'agent';
  }
}