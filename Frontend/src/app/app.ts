import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs/operators';
import { SidebarComponent } from './layout/sidebar/sidebar';
import { ClaimService, UserRole } from './services/claim-management/claim-service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, SidebarComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  private router = inject(Router);
  claimService = inject(ClaimService);

  isDropdownOpen = signal<boolean>(false);

  get currentRole(): UserRole {
    return this.claimService.currentRole;
  }

  toggleDropdown(): void {
    this.isDropdownOpen.update(open => !open);
  }

  closeDropdown(): void {
    this.isDropdownOpen.set(false);
  }

  constructor() {
    this.syncRoleWithUrl(this.router.url);
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.syncRoleWithUrl(event.urlAfterRedirects || event.url);
      });
  }

  private syncRoleWithUrl(url: string): void {
    if (!url) return;
    if (url.startsWith('/customer') || url.startsWith('/profile')) {
      if (this.claimService.currentRole !== 'Customer') {
        this.claimService.setRole('Customer');
      }
    } else if (url.startsWith('/agent')) {
      if (this.claimService.currentRole !== 'Agent') {
        this.claimService.setRole('Agent');
      }
    } else if (url.startsWith('/admin')) {
      if (this.claimService.currentRole !== 'Admin') {
        this.claimService.setRole('Admin');
      }
    }
  }

  setRole(role: string): void {
    const r = role as UserRole;
    this.claimService.setRole(r);
    if (r === 'Customer') {
      this.router.navigate(['/customer/dashboard']);
    } else if (r === 'Agent') {
      this.router.navigate(['/agent/dashboard']);
    } else {
      this.router.navigate(['/admin/dashboard']);
    }
  }
}
