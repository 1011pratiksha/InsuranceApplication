
import { UserRole } from '../../services/claim-management/claim-service';

export interface NavSubItem {
  id?: string;
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
// 1. CUSTOMER NAVIGATION
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
  },
  {
    id: 'customer-quotes',
    label: 'My Quotes',
    route: '/customer/quotes',
    icon: 'file-text'
  }
];

// ========================================================
// 2. AGENT NAVIGATION
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
  },
  {
    id: 'agent-quote-generation',
    label: 'Quote Generation',
    icon: 'file-text',
    subItems: [
      {
        id: 'agent-new-quote',
        label: 'Create New Quote',
        route: '/agent/quote-new'
      },
      {
        id: 'agent-quotes-list',
        label: 'Quotes List',
        route: '/agent/quotes'
      },
      {
        id: 'agent-generate-quote',
        label: 'Generate Quote',
        route: '/agent/quotes/generate'
      }
    ]
  }
];

// ========================================================
// 3. ADMIN NAVIGATION
// ========================================================
export const ADMIN_NAV_ITEMS: NavItem[] = [
  {
    id: 'admin-dashboard',
    label: 'Dashboard',
    route: '/admin/dashboard',
    icon: 'dashboard'
  },
  {
    id: 'admin-product-management',
    label: 'Product Management',
    route: '/admin/product-management',
    icon: 'package'
  },
  {
    id: 'admin-quote-generation',
    label: 'Quote Generation',
    icon: 'file-text',
    subItems: [
      {
        id: 'admin-quotes-list',
        label: 'Quotes List',
        route: '/admin/quote-generation'
      },
      {
        id: 'admin-quote-review',
        label: 'Quote Review',
        route: '/admin/quote-review/1'
      }
    ]
  },
  {
    id: 'admin-underwriting',
    label: 'Underwriting',
    icon: 'clipboard-list',
    subItems: [
      {
        id: 'admin-underwriting-queue',
        label: 'Underwriting Queue',
        route: '/admin/underwriting'
      },
      {
        id: 'admin-underwriting-case-review',
        label: 'Case Review',
        route: '/admin/uw-case/1'
      },
      {
        id: 'admin-underwriting-proposal',
        label: 'Proposal Form',
        route: '/admin/underwriting/proposal'
      }
    ]
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

// ========================================================
// 4. ROLE-BASED NAVIGATION
// ========================================================
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
