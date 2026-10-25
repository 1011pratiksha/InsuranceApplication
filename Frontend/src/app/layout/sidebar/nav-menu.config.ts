import { UserRole } from '../../services/claim-management/claim-service';

export interface NavSubItem {
  label: string;
  route: string;
  badge?: string;
  badgeClass?: string;
}

export interface NavItem {
  id: string;
  label: string;
  route?: string;
  icon: string;
  badge?: string;
  badgeClass?: string;
  subItems?: NavSubItem[];
  expanded?: boolean;
}

// ========================================================
// 1. CUSTOMER NAVIGATION (Claims Management Backend Flow)
// ========================================================
export const CUSTOMER_NAV_ITEMS: NavItem[] = [
  {
    id: 'customer-dashboard',
    label: 'Dashboard',
    route: '/customer/dashboard',
    icon: 'dashboard'
  },
  {
    id: 'customer-claims-register',
    label: 'Register Claim',
    route: '/customer/claims/register',
    icon: 'file-text'
  },
  {
    id: 'customer-claims-list',
    label: 'My Claims',
    route: '/customer/claims/list',
    icon: 'clipboard-list'
  },
  {
    id: 'customer-claims-documents',
    label: 'Upload Documents',
    route: '/customer/claims/documents',
    icon: 'id-card'
  },
  {
    id: 'customer-claims-tracking',
    label: 'Track Claim',
    route: '/customer/claims/tracking',
    icon: 'refresh'
  }
];

// ========================================================
// 2. AGENT NAVIGATION (Claims Management Backend Flow)
// ========================================================
export const AGENT_NAV_ITEMS: NavItem[] = [
  {
    id: 'agent-dashboard',
    label: 'Dashboard',
    route: '/agent/dashboard',
    icon: 'dashboard'
  },
  {
    id: 'agent-claims-register',
    label: 'Register Claim for Customer',
    route: '/agent/claims/register',
    icon: 'file-text'
  },
  {
    id: 'agent-claims-list',
    label: 'Customer Claims Queue',
    route: '/agent/claims/list',
    icon: 'clipboard-list'
  },
  {
    id: 'agent-claims-tracking',
    label: 'Track Claims',
    route: '/agent/claims/tracking',
    icon: 'refresh'
  }
];

// ========================================================
// 3. ADMIN NAVIGATION (Claims Management Backend Flow)
// ========================================================
export const ADMIN_NAV_ITEMS: NavItem[] = [
  {
    id: 'admin-dashboard',
    label: 'Dashboard',
    route: '/admin/dashboard',
    icon: 'dashboard'
  },
  {
    id: 'admin-claims-list',
    label: 'Claims Queue',
    route: '/admin/claims/list',
    icon: 'clipboard-list'
  },
  {
    id: 'admin-claims-documents',
    label: 'Document Verification',
    route: '/admin/claims/documents',
    icon: 'id-card'
  },
  {
    id: 'admin-claims-survey',
    label: 'Surveyor Management',
    route: '/admin/claims/survey',
    icon: 'wrench'
  },
  {
    id: 'admin-claims-investigation',
    label: 'Investigation Management',
    route: '/admin/claims/investigation',
    icon: 'shield'
  },
  {
    id: 'admin-claims-assessment',
    label: 'Loss Assessment',
    route: '/admin/claims/assessment',
    icon: 'percent'
  },
  {
    id: 'admin-claims-approval',
    label: 'Claim Approval Decision',
    route: '/admin/claims/approval',
    icon: 'user-check'
  },
  {
    id: 'admin-claims-settlement',
    label: 'Settlement & Payout',
    route: '/admin/claims/settlement',
    icon: 'dollar-sign'
  },
  {
    id: 'admin-claims-tracking',
    label: 'End-to-End Tracker',
    route: '/admin/claims/tracking',
    icon: 'refresh'
  }
];

export function getNavItemsForRole(role: UserRole): NavItem[] {
  switch (role) {
    case 'Customer':
      return CUSTOMER_NAV_ITEMS;
    case 'Agent':
      return AGENT_NAV_ITEMS;
    case 'Admin':
    default:
      return ADMIN_NAV_ITEMS;
  }
}
