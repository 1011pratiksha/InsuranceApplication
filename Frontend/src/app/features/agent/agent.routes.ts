import { Routes } from '@angular/router';
import { CustomerRegistrationComponent } from '../customer-registration/customer-registration/customer-registration.component';
import { FeaturePlaceholderComponent } from '../../shared/components/feature-placeholder/feature-placeholder.component';

export const AGENT_ROUTES: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    component: FeaturePlaceholderComponent,
    data: { title: 'Agent Dashboard', category: 'Overview', icon: 'dashboard', description: 'Monitor your customer portfolio, policies sold, commission balance, and pending tasks.' }
  },
  {
    path: 'profile',
    component: FeaturePlaceholderComponent,
    data: { title: 'Agent Profile', category: 'My Profile', icon: 'badge', description: 'Agent code, license number, validity, IRDA certification, joining date, and status.' }
  },
  // My Customers
  {
    path: 'customers',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Customer List / Search', category: 'My Customers', icon: 'groups', description: 'Search and manage all customers mapped to your agency code.' }
      },
      {
        path: 'register',
        component: CustomerRegistrationComponent
      },
      {
        path: 'profile',
        component: FeaturePlaceholderComponent,
        data: { title: 'Customer Profile', category: 'My Customers', icon: 'person', description: 'Personal details, address, nominee, and occupational classification.' }
      },
      {
        path: 'kyc',
        component: FeaturePlaceholderComponent,
        data: { title: 'Submit KYC for Customer', category: 'My Customers', icon: 'verified_user', description: 'Upload CKYC / Aadhaar / PAN documents on behalf of your customer.' }
      }
    ]
  },
  // Products
  {
    path: 'products',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Product Catalog', category: 'Products', icon: 'shield', description: 'Browse available insurance products, eligibility rules, and marketing collateral.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Product Details', category: 'Products', icon: 'info', description: 'Comprehensive coverage, benefits, terms, exclusions, and premium charts.' }
      }
    ]
  },
  // Quotes
  {
    path: 'quotes',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'generate',
        component: FeaturePlaceholderComponent,
        data: { title: 'Generate Quote', category: 'Quotes', icon: 'calculate', description: 'Generate instant premium quotes with personalized rider combinations for clients.' }
      },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'My Quotes', category: 'Quotes', icon: 'request_quote', description: 'Saved quotes and proposal drafts created for your prospect customers.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Quote Details & Premium Breakup', category: 'Quotes', icon: 'receipt', description: 'Itemized premium breakup showing basic cover, riders, discounts, and GST.' }
      },
      {
        path: 'convert',
        component: FeaturePlaceholderComponent,
        data: { title: 'Convert to Application', category: 'Quotes', icon: 'assignment_turned_in', description: 'Fast-track quote to formal policy application with pre-filled details.' }
      }
    ]
  },
  // Applications & Underwriting
  {
    path: 'applications',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Application List', category: 'Applications & Underwriting', icon: 'description', description: 'Track all customer policy applications initiated under your agency.' }
      },
      {
        path: 'status',
        component: FeaturePlaceholderComponent,
        data: { title: 'Underwriting Status', category: 'Applications & Underwriting', icon: 'fact_check', description: 'Monitor medical checkups, financial review, and underwriter decisions.' }
      }
    ]
  },
  // Policies
  {
    path: 'policies',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policies Sold / Assigned', category: 'Policies', icon: 'policy', description: 'List of active, lapsed, and surrendered policies in your agency book.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy Details', category: 'Policies', icon: 'content_paste', description: 'Full view of policy schedule, sum assured, riders, and nominees.' }
      },
      {
        path: 'generate',
        component: FeaturePlaceholderComponent,
        data: { title: 'Generate Policy', category: 'Policies', icon: 'note_add', description: 'Initiate policy generation after underwriting clearance and first premium payment.' }
      }
    ]
  },
  // Premium Collection
  {
    path: 'premiums',
    children: [
      { path: '', redirectTo: 'due', pathMatch: 'full' },
      {
        path: 'due',
        component: FeaturePlaceholderComponent,
        data: { title: 'Due Premiums', category: 'Premium Collection', icon: 'pending_actions', description: 'Upcoming and overdue renewal premiums for your assigned customers.' }
      },
      {
        path: 'collect',
        component: FeaturePlaceholderComponent,
        data: { title: 'Collect Premium', category: 'Premium Collection', icon: 'account_balance_wallet', description: 'Record premium collection via cheque, DD, POS, or digital payment link.' }
      },
      {
        path: 'history',
        component: FeaturePlaceholderComponent,
        data: { title: 'Collection History', category: 'Premium Collection', icon: 'history', description: 'Audit trail of premiums collected and deposited to company accounts.' }
      }
    ]
  },
  // Service Requests
  {
    path: 'services',
    children: [
      { path: '', redirectTo: 'tracking', pathMatch: 'full' },
      {
        path: 'submit',
        component: FeaturePlaceholderComponent,
        data: { title: 'Submit Request for Customer', category: 'Service Requests', icon: 'post_add', description: 'Raise endorsement requests such as nominee update, address change, or bank mandate.' }
      },
      {
        path: 'tracking',
        component: FeaturePlaceholderComponent,
        data: { title: 'Track Requests', category: 'Service Requests', icon: 'sync_alt', description: 'Real-time status of service requests submitted for your clients.' }
      }
    ]
  },
  // Claims
  {
    path: 'claims',
    children: [
      { path: '', redirectTo: 'tracking', pathMatch: 'full' },
      {
        path: 'register',
        component: FeaturePlaceholderComponent,
        data: { title: 'Register Claim for Customer', category: 'Claims', icon: 'add_circle', description: 'Assist customer in lodging a claim intimation and incident documentation.' }
      },
      {
        path: 'tracking',
        component: FeaturePlaceholderComponent,
        data: { title: 'Track Claims', category: 'Claims', icon: 'timeline', description: 'Follow claim assessment, surveyor visits, and settlement disbursement.' }
      }
    ]
  },
  // My Commissions
  {
    path: 'commissions',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Commission List', category: 'My Commissions', icon: 'monetization_on', description: 'Commission records broken down by Pending, Approved, Payable, Paid, and Reversed.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Commission Details', category: 'My Commissions', icon: 'receipt_long', description: 'Itemized calculation formula: Premium x Commission Rate % = Earned Payout.' }
      },
      {
        path: 'rates',
        component: FeaturePlaceholderComponent,
        data: { title: 'Commission Rates', category: 'My Commissions', icon: 'percent', description: 'Official commission percentage grid by product category and policy year.' }
      }
    ]
  },
  // Payouts
  {
    path: 'payouts',
    children: [
      { path: '', redirectTo: 'history', pathMatch: 'full' },
      {
        path: 'history',
        component: FeaturePlaceholderComponent,
        data: { title: 'Payout History', category: 'Payouts', icon: 'account_balance', description: 'Historical disbursement records, bank credits, TDS deductions, and statements.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Payout Details', category: 'Payouts', icon: 'info', description: 'Transaction reference, payment mode (NEFT/RTGS), TDS certificate, and status.' }
      }
    ]
  },
  // Reports
  {
    path: 'reports',
    component: FeaturePlaceholderComponent,
    data: { title: 'Agent Reports', category: 'Reports', icon: 'bar_chart', description: 'Sales performance analytics, persistency ratio, and commission earnings reports.' }
  },
  // Notifications
  {
    path: 'notifications',
    component: FeaturePlaceholderComponent,
    data: { title: 'Notifications', category: 'Account', icon: 'notifications', description: 'Alerts for newly approved policies, commission credits, and renewal reminders.' }
  },
  // Settings
  {
    path: 'settings',
    component: FeaturePlaceholderComponent,
    data: { title: 'Account Settings', category: 'Account', icon: 'settings', description: 'Manage password, profile details, and authentication security options.' }
  }
];
