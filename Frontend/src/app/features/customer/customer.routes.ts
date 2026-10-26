import { Routes } from '@angular/router';
import { MyPoliciesComponent } from '../policy-issuance/customer/my-policies/my-policies.component';
import { PolicyDetailsComponent } from '../policy-issuance/customer/policy-details/policy-details.component';
import { FeaturePlaceholderComponent } from '../../shared/components/feature-placeholder/feature-placeholder.component';

export const CUSTOMER_ROUTES: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    component: FeaturePlaceholderComponent,
    data: { title: 'Customer Dashboard', category: 'Overview', icon: 'dashboard', description: 'Monitor your policies, upcoming premium dues, and claims at a glance.' }
  },
  // My Profile
  {
    path: 'profile',
    children: [
      { path: '', redirectTo: 'personal', pathMatch: 'full' },
      {
        path: 'personal',
        component: FeaturePlaceholderComponent,
        data: { title: 'Personal Details', category: 'My Profile', icon: 'person', description: 'View and manage your registered personal details, full name, date of birth, and identity records.' }
      },
      {
        path: 'contact',
        component: FeaturePlaceholderComponent,
        data: { title: 'Contact Details', category: 'My Profile', icon: 'contact_phone', description: 'Manage your verified email addresses, mobile numbers, and communication preferences.' }
      },
      {
        path: 'address',
        component: FeaturePlaceholderComponent,
        data: { title: 'Address Details', category: 'My Profile', icon: 'home_pin', description: 'Manage permanent, current, and communication residential addresses.' }
      },
      {
        path: 'occupation',
        component: FeaturePlaceholderComponent,
        data: { title: 'Occupation Details', category: 'My Profile', icon: 'work', description: 'Review and update your employer, annual income, and occupational risk classification.' }
      },
      {
        path: 'nominees',
        component: FeaturePlaceholderComponent,
        data: { title: 'Nominee Details', category: 'My Profile', icon: 'diversity_1', description: 'Manage registered policy nominees, relationships, and share percentages.' }
      },
      {
        path: 'documents',
        component: FeaturePlaceholderComponent,
        data: { title: 'My Documents', category: 'My Profile', icon: 'folder_shared', description: 'Access uploaded identity proofs, address documents, and income verification files.' }
      }
    ]
  },
  // KYC
  {
    path: 'kyc',
    children: [
      { path: '', redirectTo: 'status', pathMatch: 'full' },
      {
        path: 'submit',
        component: FeaturePlaceholderComponent,
        data: { title: 'Submit KYC', category: 'KYC Verification', icon: 'fingerprint', description: 'Upload Aadhaar, PAN card, Passport, or Driving License for automated CKYC verification.' }
      },
      {
        path: 'status',
        component: FeaturePlaceholderComponent,
        data: { title: 'KYC Status & Timeline', category: 'KYC Verification', icon: 'schedule', description: 'Track your real-time KYC verification progress and timeline milestones.' }
      },
      {
        path: 'reupload',
        component: FeaturePlaceholderComponent,
        data: { title: 'Re-upload Rejected Documents', category: 'KYC Verification', icon: 'cloud_upload', description: 'Provide replacement documentation for any unverified or rejected KYC items.' }
      }
    ]
  },
  // Products
  {
    path: 'products',
    children: [
      { path: '', redirectTo: 'browse', pathMatch: 'full' },
      {
        path: 'browse',
        component: FeaturePlaceholderComponent,
        data: { title: 'Browse Products', category: 'Insurance Products', icon: 'shield', description: 'Explore Life, Health, Motor, and Home insurance plans with category filters.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Product Details', category: 'Insurance Products', icon: 'info', description: 'Detailed coverage limits, optional riders, terms, and tax saving benefits.' }
      },
      {
        path: 'compare',
        component: FeaturePlaceholderComponent,
        data: { title: 'Compare Products', category: 'Insurance Products', icon: 'compare_arrows', description: 'Side-by-side comparison of coverage limits, deductibles, and premiums.' }
      }
    ]
  },
  // Quotes
  {
    path: 'quotes',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'request',
        component: FeaturePlaceholderComponent,
        data: { title: 'Request Quote', category: 'Quotes', icon: 'calculate', description: 'Instant premium calculator based on age, coverage amount, and optional riders.' }
      },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'My Quotes', category: 'Quotes', icon: 'request_quote', description: 'Review your saved and generated insurance quotes.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Quote Details', category: 'Quotes', icon: 'receipt', description: 'Detailed quote breakdown including base premium, GST, and applicable discounts.' }
      },
      {
        path: 'convert',
        component: FeaturePlaceholderComponent,
        data: { title: 'Convert to Application', category: 'Quotes', icon: 'assignment_turned_in', description: 'Proceed directly from a generated quote to full policy application proposal.' }
      }
    ]
  },
  // My Applications
  {
    path: 'applications',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Application List', category: 'My Applications', icon: 'format_list_bulleted', description: 'Overview of all your submitted insurance policy applications.' }
      },
      {
        path: 'status',
        component: FeaturePlaceholderComponent,
        data: { title: 'Underwriting Status Tracker', category: 'My Applications', icon: 'troubleshoot', description: 'Real-time assessment tracking from proposal submission to policy issuance.' }
      }
    ]
  },
  // My Policies
  {
    path: 'policies',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', component: MyPoliciesComponent },
      { path: 'details/:id', component: PolicyDetailsComponent },
      { path: 'details', component: PolicyDetailsComponent },
      {
        path: 'documents',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy Documents & Download', category: 'My Policies', icon: 'download', description: 'Download official digital policy bonds, certificates, and endorsements.' }
      },
      {
        path: 'history',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy History', category: 'My Policies', icon: 'history', description: 'Historical record of endorsements, premium updates, and term extensions.' }
      }
    ]
  },
  // Premium & Payments
  {
    path: 'premiums',
    children: [
      { path: '', redirectTo: 'schedule', pathMatch: 'full' },
      {
        path: 'schedule',
        component: FeaturePlaceholderComponent,
        data: { title: 'Premium Schedule', category: 'Premium & Payments', icon: 'calendar_month', description: 'Installment due dates, grace period limits, and payment frequency breakdown.' }
      },
      {
        path: 'pay',
        component: FeaturePlaceholderComponent,
        data: { title: 'Pay Premium', category: 'Premium & Payments', icon: 'credit_card', description: 'Secure payment gateway for UPI, Net Banking, Debit/Credit Card premium checkout.' }
      },
      {
        path: 'history',
        component: FeaturePlaceholderComponent,
        data: { title: 'Payment History', category: 'Premium & Payments', icon: 'paid', description: 'Complete ledger of all past premium transactions and settlements.' }
      },
      {
        path: 'receipts',
        component: FeaturePlaceholderComponent,
        data: { title: 'Payment Receipts', category: 'Premium & Payments', icon: 'receipt_long', description: 'Instant downloadable Section 80C/80D tax receipts for paid premiums.' }
      },
      {
        path: 'refunds',
        component: FeaturePlaceholderComponent,
        data: { title: 'Refund Status', category: 'Premium & Payments', icon: 'currency_exchange', description: 'Track refund processing for cancelled policies or overpayments.' }
      }
    ]
  },
  // Policy Services
  {
    path: 'services',
    children: [
      { path: '', redirectTo: 'tracking', pathMatch: 'full' },
      {
        path: 'new',
        component: FeaturePlaceholderComponent,
        data: { title: 'New Service Request', category: 'Policy Services', icon: 'add_task', description: 'Submit endorsements for address change, nominee updates, or rider adjustments.' }
      },
      {
        path: 'tracking',
        component: FeaturePlaceholderComponent,
        data: { title: 'Request Tracking', category: 'Policy Services', icon: 'pending_actions', description: 'Monitor pending review and approval of submitted service requests.' }
      },
      {
        path: 'cancellation',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy Cancellation', category: 'Policy Services', icon: 'cancel', description: 'Request policy surrender, cooling-off free look cancellation, or termination.' }
      }
    ]
  },
  // Renewals
  {
    path: 'renewals',
    children: [
      { path: '', redirectTo: 'upcoming', pathMatch: 'full' },
      {
        path: 'upcoming',
        component: FeaturePlaceholderComponent,
        data: { title: 'Upcoming Renewals', category: 'Renewals', icon: 'event_upcoming', description: 'Policies due for annual renewal within the next 60 days.' }
      },
      {
        path: 'renew',
        component: FeaturePlaceholderComponent,
        data: { title: 'Renew Policy', category: 'Renewals', icon: 'autorenew', description: 'One-click renewal with seamless premium payment and policy extension.' }
      }
    ]
  },
  // Claims
  {
    path: 'claims',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'raise',
        component: FeaturePlaceholderComponent,
        data: { title: 'Raise Claim', category: 'Claims', icon: 'add_circle', description: 'Initiate a new cashless or reimbursement claim with incident details.' }
      },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'My Claims', category: 'Claims', icon: 'gavel', description: 'View all ongoing and settled claims filed across your active policies.' }
      },
      {
        path: 'details',
        component: FeaturePlaceholderComponent,
        data: { title: 'Claim Details & Status Tracker', category: 'Claims', icon: 'timeline', description: 'Step-by-step progress tracking from claim intimation to final settlement.' }
      },
      {
        path: 'upload',
        component: FeaturePlaceholderComponent,
        data: { title: 'Upload Documents', category: 'Claims', icon: 'file_upload', description: 'Upload hospital bills, discharge summaries, FIR, or repair estimates.' }
      },
      {
        path: 'settlement',
        component: FeaturePlaceholderComponent,
        data: { title: 'Settlement Details', category: 'Claims', icon: 'task_alt', description: 'Approved claim amount breakdown, deductions, and NEFT transfer confirmation.' }
      }
    ]
  },
  // Reports
  {
    path: 'reports',
    component: FeaturePlaceholderComponent,
    data: { title: 'Customer Reports', category: 'Reports', icon: 'bar_chart', description: 'Download consolidated policy, premium payment, and tax deduction statements.' }
  },
  // Notifications
  {
    path: 'notifications',
    component: FeaturePlaceholderComponent,
    data: { title: 'Notifications', category: 'Account', icon: 'notifications', description: 'Alerts regarding upcoming premium dues, policy status updates, and claims.' }
  },
  // Settings
  {
    path: 'settings',
    component: FeaturePlaceholderComponent,
    data: { title: 'Account Settings', category: 'Account', icon: 'settings', description: 'Change password, two-factor authentication (2FA), and Google Authenticator.' }
  }
];
