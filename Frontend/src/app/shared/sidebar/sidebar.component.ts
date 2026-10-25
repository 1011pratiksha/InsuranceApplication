import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent {
  readonly auth = inject(AuthService);

  /** Whether the sidebar is collapsed to icon-only mode */
  collapsed = signal(false);

  /** Track which top-level groups are expanded (by label) */
  expandedGroups = signal<Set<string>>(new Set());

  toggleSidebar(): void {
    this.collapsed.update(v => !v);
    // Collapse all groups when sidebar collapses
    if (this.collapsed()) {
      this.expandedGroups.set(new Set());
    }
  }

  toggleGroup(label: string): void {
    // Auto-expand sidebar if collapsed when clicking a group
    if (this.collapsed()) {
      this.collapsed.set(false);
    }
    this.expandedGroups.update(set => {
      const next = new Set(set);
      if (next.has(label)) {
        next.delete(label);
      } else {
        next.add(label);
      }
      return next;
    });
  }

  isGroupExpanded(label: string): boolean {
    return this.expandedGroups().has(label);
  }

  getUserInitial(): string {
    const name = this.auth.userName();
    return name ? name.charAt(0).toUpperCase() : 'U';
  }
}
