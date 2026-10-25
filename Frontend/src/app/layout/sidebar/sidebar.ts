import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NavigationEnd, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { filter } from 'rxjs/operators';
import { ClaimService, UserRole } from '../../services/claim-management/claim-service';
import { NavItem, NavSubItem, getNavItemsForRole } from './nav-menu.config';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, FormsModule],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss'
})
export class SidebarComponent {
  private router = inject(Router);
  claimService = inject(ClaimService);

  // Search filter query
  searchQuery = signal<string>('');

  // Expanded menu item IDs map
  expandedState = signal<Record<string, boolean>>({});

  // Active role signal or computed
  get currentRole(): UserRole {
    return this.claimService.currentRole;
  }

  // Raw nav items for the current role
  private currentRawItems = computed<NavItem[]>(() => {
    return getNavItemsForRole(this.currentRole);
  });

  // Filtered nav items based on search query
  filteredNavItems = computed<NavItem[]>(() => {
    const query = this.searchQuery().trim().toLowerCase();
    const items = this.currentRawItems();

    if (!query) {
      return items;
    }

    const result: NavItem[] = [];
    for (const item of items) {
      const itemMatches = item.label.toLowerCase().includes(query) || (item.route ? item.route.toLowerCase().includes(query) : false);
      const matchedSubItems = item.subItems?.filter(sub =>
        sub.label.toLowerCase().includes(query) || sub.route.toLowerCase().includes(query)
      );

      if (itemMatches || (matchedSubItems && matchedSubItems.length > 0)) {
        const itemCopy: NavItem = {
          ...item,
          subItems: matchedSubItems && matchedSubItems.length > 0 ? matchedSubItems : item.subItems
        };
        result.push(itemCopy);
      }
    }
    return result;
  });

  constructor() {
    // Listen to router events to auto-expand active parent item
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.autoExpandActiveRoute(event.urlAfterRedirects || event.url);
      });

    // Initial check
    setTimeout(() => {
      this.autoExpandActiveRoute(this.router.url);
    }, 100);
  }

  isExpanded(itemId: string): boolean {
    // If search is active, always expand matching items
    if (this.searchQuery().trim().length > 0) {
      return true;
    }
    return !!this.expandedState()[itemId];
  }

  toggleExpand(item: NavItem, event?: Event): void {
    if (event) {
      event.stopPropagation();
    }
    const current = this.isExpanded(item.id);
    this.expandedState.update(state => ({
      ...state,
      [item.id]: !current
    }));
  }

  onParentClick(item: NavItem): void {
    if (item.subItems && item.subItems.length > 0) {
      this.toggleExpand(item);
      // If item has a direct route and is not on it, also navigate
      if (item.route) {
        this.router.navigateByUrl(item.route);
      }
    } else if (item.route) {
      this.router.navigateByUrl(item.route);
    }
  }

  expandAll(): void {
    const updated: Record<string, boolean> = {};
    for (const item of this.currentRawItems()) {
      if (item.subItems && item.subItems.length > 0) {
        updated[item.id] = true;
      }
    }
    this.expandedState.set(updated);
  }

  collapseAll(): void {
    this.expandedState.set({});
  }

  clearSearch(): void {
    this.searchQuery.set('');
  }

  private autoExpandActiveRoute(url: string): void {
    if (!url) return;
    const items = this.currentRawItems();
    for (const item of items) {
      if (item.subItems) {
        const hasActiveChild = item.subItems.some(sub => url.startsWith(sub.route));
        if (hasActiveChild || (item.route && url.startsWith(item.route))) {
          this.expandedState.update(state => ({
            ...state,
            [item.id]: true
          }));
          break;
        }
      }
    }
  }
}
