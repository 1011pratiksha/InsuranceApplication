
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from './layout/sidebar/sidebar';
import { ClaimService, UserRole } from './services/claim-management/claim-service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    SidebarComponent
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  private claimService = inject(ClaimService);

  isDropdownOpenValue = false;

  get currentRole(): UserRole {
    return this.claimService.currentRole;
  }

  
setRole(role: string): void {
  if (
    role === 'Customer' ||
    role === 'Agent' ||
    role === 'Admin'
  ) {
    this.claimService.setRole(role);
  }

  this.closeDropdown();
}


  toggleDropdown(): void {
    this.isDropdownOpenValue = !this.isDropdownOpenValue;
  }

  closeDropdown(): void {
    this.isDropdownOpenValue = false;
  }

  isDropdownOpen(): boolean {
    return this.isDropdownOpenValue;
  }
}
