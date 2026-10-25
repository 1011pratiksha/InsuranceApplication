import { SidebarMenuItem } from '../models/sidebar-menu.model';

// ─────────────────────────────────────────────
// 1. CUSTOMER SIDEBAR
// ─────────────────────────────────────────────
export const CUSTOMER_MENU: SidebarMenuItem[] = [
  {
    label: 'Dashboard',
    icon: 'dashboard',
    route: '/customer/dashboard'
  },
  {
    label: 'My Profile',
    icon: 'person',
    children: [
      { label: 'Personal Details', route: '/customer/profile/personal' },
      { label: 'Contact Details', route: '/customer/profile/contact' },
      { label: 'Address Details', route: '/customer/profile/address' },
      { label: 'Occupation Details', route: '/customer/profile/occupation' },
      { label: 'Nominee Details', route: '/customer/profile/nominees' },
      { label: 'My Documents', route: '/customer/profile/documents' }
    ]
  },
  {
    label: 'KYC',
    icon: 'verified_user',
    children: [
      { label: 'Submit KYC', route: '/customer/kyc/submit' },
      { label: 'KYC Status & Timeline', route: '/customer/kyc/status' },
      { label: 'Re-upload Documents', route: '/customer/kyc/reupload' }
    ]
  },
  {
    label: 'Insurance Products',
    icon: 'shield',
    children: [
      { label: 'Browse Products', route: '/customer/products/browse' },
      { label: 'Product Details', route: '/customer/products/details' },
      { label: 'Compare Products', route: '/customer/products/compare' }
    ]
  },
  {
    label: 'Quotes',
    icon: 'request_quote',
    children: [
      { label: 'Request Quote', route: '/customer/quotes/request' },
      { label: 'My Quotes', route: '/customer/quotes/list' },
      { label: 'Quote Details', route: '/customer/quotes/details' },
      { label: 'Convert to Application', route: '/customer/quotes/convert' }
    ]
  },
  {
    label: 'My Applications',
    icon: 'description',
    children: [
      { label: 'Application List', route: '/customer/applications/list' },
      { label: 'Underwriting Status', route: '/customer/applications/status' }
    ]
  },
  {
    label: 'My Policies',
    icon: 'policy',
    children: [
      { label: 'Policy List', route: '/customer/policies/list' },
      { label: 'Policy Details', route: '/customer/policies/details' },
      { label: 'Policy Documents', route: '/customer/policies/documents' },
      { label: 'Policy History', route: '/customer/policies/history' }
    ]
  },
  {
    label: 'Premium & Payments',
    icon: 'payments',
    children: [
      { label: 'Premium Schedule', route: '/customer/premiums/schedule' },
      { label: 'Pay Premium', route: '/customer/premiums/pay' },
      { label: 'Payment History', route: '/customer/premiums/history' },
      { label: 'Receipts', route: '/customer/premiums/receipts' },
      { label: 'Refund Status', route: '/customer/premiums/refunds' }
    ]
  },
  {
    label: 'Policy Services',
    icon: 'support_agent',
    children: [
      { label: 'New Service Request', route: '/customer/services/new' },
      { label: 'Request Tracking', route: '/customer/services/tracking' },
      { label: 'Policy Cancellation', route: '/customer/services/cancellation' }
    ]
  },
  {
    label: 'Renewals',
    icon: 'autorenew',
    children: [
      { label: 'Upcoming Renewals', route: '/customer/renewals/upcoming' },
      { label: 'Renew Policy', route: '/customer/renewals/renew' }
    ]
  },
  {
    label: 'Claims',
    icon: 'gavel',
    children: [
      { label: 'Raise Claim', route: '/customer/claims/raise' },
      { label: 'My Claims', route: '/customer/claims/list' },
      { label: 'Claim Details & Status', route: '/customer/claims/details' },
      { label: 'Upload Documents', route: '/customer/claims/upload' },
      { label: 'Settlement Details', route: '/customer/claims/settlement' }
    ]
  },
  {
    label: 'Reports',
    icon: 'bar_chart',
    route: '/customer/reports'
  },
  {
    label: 'Notifications',
    icon: 'notifications',
    route: '/customer/notifications'
  },
  {
    label: 'Account Settings',
    icon: 'settings',
    route: '/customer/settings'
  }
];

// ─────────────────────────────────────────────
// 2. AGENT SIDEBAR
// ─────────────────────────────────────────────
export const AGENT_MENU: SidebarMenuItem[] = [
  {
    label: 'Dashboard',
    icon: 'dashboard',
    route: '/agent/dashboard'
  },
  {
    label: 'My Profile',
    icon: 'badge',
    route: '/agent/profile'
  },
  {
    label: 'My Customers',
    icon: 'groups',
    children: [
      { label: 'Customer List', route: '/agent/customers/list' },
      { label: 'Register New Customer', route: '/agent/customers/register' },
      { label: 'Customer Profile', route: '/agent/customers/profile' },
      { label: 'Submit KYC for Customer', route: '/agent/customers/kyc' }
    ]
  },
  {
    label: 'Products',
    icon: 'shield',
    children: [
      { label: 'Product List', route: '/agent/products/list' },
      { label: 'Product Details', route: '/agent/products/details' }
    ]
  },
  {
    label: 'Quotes',
    icon: 'request_quote',
    children: [
      { label: 'Generate Quote', route: '/agent/quotes/generate' },
      { label: 'My Quotes', route: '/agent/quotes/list' },
      { label: 'Quote Details', route: '/agent/quotes/details' },
      { label: 'Convert to Application', route: '/agent/quotes/convert' }
    ]
  },
  {
    label: 'Applications & Underwriting',
    icon: 'fact_check',
    children: [
      { label: 'Application List', route: '/agent/applications/list' },
      { label: 'Underwriting Status', route: '/agent/applications/status' }
    ]
  },
  {
    label: 'Policies',
    icon: 'policy',
    children: [
      { label: 'Policies Sold', route: '/agent/policies/list' },
      { label: 'Policy Details', route: '/agent/policies/details' },
      { label: 'Generate Policy', route: '/agent/policies/generate' }
    ]
  },
  {
    label: 'Premium Collection',
    icon: 'account_balance_wallet',
    children: [
      { label: 'Due Premiums', route: '/agent/premiums/due' },
      { label: 'Collect Premium', route: '/agent/premiums/collect' },
      { label: 'Collection History', route: '/agent/premiums/history' }
    ]
  },
  {
    label: 'Service Requests',
    icon: 'support_agent',
    children: [
      { label: 'Submit Request', route: '/agent/services/submit' },
      { label: 'Track Requests', route: '/agent/services/tracking' }
    ]
  },
  {
    label: 'Claims',
    icon: 'gavel',
    children: [
      { label: 'Register Claim', route: '/agent/claims/register' },
      { label: 'Track Claims', route: '/agent/claims/tracking' }
    ]
  },
  {
    label: 'My Commissions',
    icon: 'monetization_on',
    children: [
      { label: 'Commission List', route: '/agent/commissions/list' },
      { label: 'Commission Details', route: '/agent/commissions/details' },
      { label: 'Commission Rates', route: '/agent/commissions/rates' }
    ]
  },
  {
    label: 'Payouts',
    icon: 'receipt_long',
    children: [
      { label: 'Payout History', route: '/agent/payouts/history' },
      { label: 'Payout Details', route: '/agent/payouts/details' }
    ]
  },
  {
    label: 'Reports',
    icon: 'bar_chart',
    route: '/agent/reports'
  },
  {
    label: 'Notifications',
    icon: 'notifications',
    route: '/agent/notifications'
  },
  {
    label: 'Account Settings',
    icon: 'settings',
    route: '/agent/settings'
  }
];

// ─────────────────────────────────────────────
// 3. ADMIN SIDEBAR
// ─────────────────────────────────────────────
export const ADMIN_MENU: SidebarMenuItem[] = [
  {
    label: 'Dashboard',
    icon: 'dashboard',
    route: '/admin/dashboard'
  },
  {
    label: 'User Management',
    icon: 'manage_accounts',
    children: [
      { label: 'Users / Create User', route: '/admin/users/list' },
      { label: 'Roles & Permissions', route: '/admin/users/roles' },
      { label: 'Agent Management', route: '/admin/users/agents' },
      { label: 'Agent–Customer Assignment', route: '/admin/users/assignment' },
      { label: 'Agent Policy Links', route: '/admin/users/policy-links' }
    ]
  },
  {
    label: 'Customers',
    icon: 'groups',
    route: '/admin/customers'
  },
  {
    label: 'KYC Management',
    icon: 'verified_user',
    children: [
      { label: 'Pending Queue', route: '/admin/kyc/pending' },
      { label: 'Approve / Reject', route: '/admin/kyc/review' },
      { label: 'KYC Rules Config', route: '/admin/kyc/rules' },
      { label: 'KYC Audit Log', route: '/admin/kyc/audit' }
    ]
  },
  {
    label: 'Product Management',
    icon: 'inventory',
    children: [
      { label: 'Products & Categories', route: '/admin/products/list' },
      { label: 'Coverage, Benefits, Riders', route: '/admin/products/coverage' },
      { label: 'Sum Insured & Eligibility', route: '/admin/products/eligibility' },
      { label: 'Premium Frequency', route: '/admin/products/frequency' },
      { label: 'Commission Configuration', route: '/admin/products/commission' },
      { label: 'Product Activation', route: '/admin/products/activation' }
    ]
  },
  {
    label: 'Quotes',
    icon: 'request_quote',
    children: [
      { label: 'All Quotes', route: '/admin/quotes/list' },
      { label: 'Premium Rules & Config', route: '/admin/quotes/rules' },
      { label: 'Quote Expiry Settings', route: '/admin/quotes/expiry' }
    ]
  },
  {
    label: 'Underwriting',
    icon: 'fact_check',
    children: [
      { label: 'Case Queue', route: '/admin/underwriting/queue' },
      { label: 'Assessment', route: '/admin/underwriting/assessment' },
      { label: 'Risk Score & Loading', route: '/admin/underwriting/risk' },
      { label: 'Decision', route: '/admin/underwriting/decision' },
      { label: 'Rules Config', route: '/admin/underwriting/rules' },
      { label: 'Audit Log', route: '/admin/underwriting/audit' }
    ]
  },
  {
    label: 'Policy Issuance',
    icon: 'policy',
    children: [
      { label: 'Issue Policy', route: '/admin/policies/issue' },
      { label: 'Policy List & Details', route: '/admin/policies/list' },
      { label: 'Endorsements & History', route: '/admin/policies/endorsements' },
      { label: 'Document Templates', route: '/admin/policies/templates' }
    ]
  },
  {
    label: 'Premium & Finance',
    icon: 'account_balance',
    children: [
      { label: 'Premium Schedules', route: '/admin/premiums/schedules' },
      { label: 'Payment Verification', route: '/admin/premiums/verification' },
      { label: 'Grace Period & Late Fee', route: '/admin/premiums/grace' },
      { label: 'Reminders', route: '/admin/premiums/reminders' },
      { label: 'Refund Management', route: '/admin/premiums/refunds' }
    ]
  },
  {
    label: 'Policy Servicing',
    icon: 'build',
    children: [
      { label: 'Service Request Queue', route: '/admin/servicing/queue' },
      { label: 'Approve Requests', route: '/admin/servicing/approve' },
      { label: 'Cancellations', route: '/admin/servicing/cancellations' }
    ]
  },
  {
    label: 'Renewals & Closure',
    icon: 'autorenew',
    children: [
      { label: 'Renewal Due List', route: '/admin/renewals/due' },
      { label: 'Renewal Processing', route: '/admin/renewals/process' },
      { label: 'Policy Closure', route: '/admin/renewals/closure' }
    ]
  },
  {
    label: 'Claims',
    icon: 'gavel',
    children: [
      { label: 'Claim List & Details', route: '/admin/claims/list' },
      { label: 'Document Verification', route: '/admin/claims/verification' },
      { label: 'Surveyor & Report', route: '/admin/claims/surveyor' },
      { label: 'Investigation', route: '/admin/claims/investigation' },
      { label: 'Assessment & Decision', route: '/admin/claims/assessment' },
      { label: 'Settlement & Payment', route: '/admin/claims/settlement' },
      { label: 'Claim History', route: '/admin/claims/history' }
    ]
  },
  {
    label: 'Commission & Payouts',
    icon: 'monetization_on',
    children: [
      { label: 'Commission Rules', route: '/admin/commissions/rules' },
      { label: 'Calculate / Review', route: '/admin/commissions/review' },
      { label: 'Bulk Payouts', route: '/admin/commissions/payouts' },
      { label: 'Payout History', route: '/admin/commissions/history' },
      { label: 'Reconciliation', route: '/admin/commissions/reconcile' }
    ]
  },
  {
    label: 'Master Data',
    icon: 'storage',
    children: [
      { label: 'Department & Branch', route: '/admin/masters/department' },
      { label: 'Product & Policy Type', route: '/admin/masters/product-type' },
      { label: 'Coverage & Rider', route: '/admin/masters/coverage' },
      { label: 'Premium & Payment Mode', route: '/admin/masters/payment-mode' },
      { label: 'Claim Type & Document', route: '/admin/masters/claim-type' },
      { label: 'Geography & Tax', route: '/admin/masters/geography' }
    ]
  },
  {
    label: 'Reports & Analytics',
    icon: 'analytics',
    children: [
      { label: 'Customer Reports', route: '/admin/reports/customer' },
      { label: 'Policy Reports', route: '/admin/reports/policy' },
      { label: 'Premium Reports', route: '/admin/reports/premium' },
      { label: 'Claims Reports', route: '/admin/reports/claims' },
      { label: 'Dashboard Analytics', route: '/admin/reports/analytics' },
      { label: 'Scheduled & Export', route: '/admin/reports/export' }
    ]
  },
  {
    label: 'Administration',
    icon: 'admin_panel_settings',
    children: [
      { label: 'Notification Templates', route: '/admin/system/notifications' },
      { label: 'System Configuration', route: '/admin/system/config' },
      { label: 'Audit Logs', route: '/admin/system/audit' },
      { label: 'Authentication Settings', route: '/admin/system/auth' }
    ]
  }
];
