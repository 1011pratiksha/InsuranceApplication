import { Routes } from '@angular/router';
import { PolicyIssueComponent } from '../policy-issuance/admin/policy-issue/policy-issue.component';
import { FeaturePlaceholderComponent } from '../../shared/components/feature-placeholder/feature-placeholder.component';

export const ADMIN_ROUTES: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    component: FeaturePlaceholderComponent,
    data: { title: 'Admin Executive Dashboard', category: 'Overview', icon: 'dashboard', description: 'Enterprise KPIs, pending approvals, claims pipeline, and revenue metrics.' }
  },
  // User Management
  {
    path: 'users',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Users & Create User', category: 'User Management', icon: 'manage_accounts', description: 'System user provisioning, employee directory, and access provisioning.' }
      },
      {
        path: 'roles',
        component: FeaturePlaceholderComponent,
        data: { title: 'Roles & Permissions', category: 'User Management', icon: 'shield_lock', description: 'RBAC role matrix, fine-grained menu and API endpoint permissions.' }
      },
      {
        path: 'agents',
        component: FeaturePlaceholderComponent,
        data: { title: 'Agents Management', category: 'User Management', icon: 'badge', description: 'Agent onboarding, license verification, and status (Pending/Active/Inactive/Suspended).' }
      },
      {
        path: 'assignment',
        component: FeaturePlaceholderComponent,
        data: { title: 'Agent–Customer Assignment & Transfer', category: 'User Management', icon: 'transfer_within_a_station', description: 'Portfolio mapping, orphan customer reassignment, and branch transfers.' }
      },
      {
        path: 'policy-links',
        component: FeaturePlaceholderComponent,
        data: { title: 'Agent Policy Links', category: 'User Management', icon: 'link', description: 'Audit policy attribution, agent commission linking, and code modifications.' }
      }
    ]
  },
  // Customers
  {
    path: 'customers',
    component: FeaturePlaceholderComponent,
    data: { title: 'Customers Directory', category: 'Customer Management', icon: 'groups', description: 'Centralized customer database, unified KYC status, policies, and interaction history.' }
  },
  // KYC Management
  {
    path: 'kyc',
    children: [
      { path: '', redirectTo: 'pending', pathMatch: 'full' },
      {
        path: 'pending',
        component: FeaturePlaceholderComponent,
        data: { title: 'Pending KYC Queue', category: 'KYC Management', icon: 'pending_actions', description: 'Document verification queue for Aadhaar, PAN, Passport, and CKYC registry.' }
      },
      {
        path: 'review',
        component: FeaturePlaceholderComponent,
        data: { title: 'Approve / Reject KYC', category: 'KYC Management', icon: 'fact_check', description: 'Identity verification review, biometric audit, and rejection reason logging.' }
      },
      {
        path: 'rules',
        component: FeaturePlaceholderComponent,
        data: { title: 'KYC Rules Config', category: 'KYC Management', icon: 'tune', description: 'Thresholds for mandatory CKYC verification, photo matching, and PEP screening.' }
      },
      {
        path: 'audit',
        component: FeaturePlaceholderComponent,
        data: { title: 'KYC Audit Log', category: 'KYC Management', icon: 'history_edu', description: 'Tamper-evident logs of verification timestamps, officer signatures, and changes.' }
      }
    ]
  },
  // Product Management
  {
    path: 'products',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Products & Categories', category: 'Product Management', icon: 'inventory', description: 'Master product configuration, category hierarchies, and product life cycles.' }
      },
      {
        path: 'coverage',
        component: FeaturePlaceholderComponent,
        data: { title: 'Coverage, Benefits & Riders', category: 'Product Management', icon: 'health_and_safety', description: 'Define core benefits, optional riders, exclusions, and waiting periods.' }
      },
      {
        path: 'eligibility',
        component: FeaturePlaceholderComponent,
        data: { title: 'Sum Insured & Eligibility Rules', category: 'Product Management', icon: 'rule', description: 'Minimum/maximum entry age, sum insured slabs, and occupation restrictions.' }
      },
      {
        path: 'frequency',
        component: FeaturePlaceholderComponent,
        data: { title: 'Premium Frequency Configuration', category: 'Product Management', icon: 'calendar_month', description: 'Configure Annual, Semi-Annual, Quarterly, and Monthly loading factors.' }
      },
      {
        path: 'commission',
        component: FeaturePlaceholderComponent,
        data: { title: 'Commission Configuration', category: 'Product Management', icon: 'monetization_on', description: 'Product-specific commission tiers for year 1, renewal years, and incentives.' }
      },
      {
        path: 'activation',
        component: FeaturePlaceholderComponent,
        data: { title: 'Product Activation & Launch', category: 'Product Management', icon: 'toggle_on', description: 'IRDA approval reference, sandbox testing, and go-live deployment.' }
      }
    ]
  },
  // Quotes
  {
    path: 'quotes',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'All Quotes & High-Value Approvals', category: 'Quote Management', icon: 'request_quote', description: 'Enterprise quote registry, sum insured anomaly alerts, and underwriter sign-offs.' }
      },
      {
        path: 'rules',
        component: FeaturePlaceholderComponent,
        data: { title: 'Premium Rules, Discount & GST Config', category: 'Quote Management', icon: 'calculate', description: 'Actuarial formula engines, no-claim bonus grids, GST slabs, and discount limits.' }
      },
      {
        path: 'expiry',
        component: FeaturePlaceholderComponent,
        data: { title: 'Quote Expiry Settings', category: 'Quote Management', icon: 'timer', description: 'Configurable quote validity windows (15/30/45 days) and rate lock policies.' }
      }
    ]
  },
  // Underwriting
  {
    path: 'underwriting',
    children: [
      { path: '', redirectTo: 'queue', pathMatch: 'full' },
      {
        path: 'queue',
        component: FeaturePlaceholderComponent,
        data: { title: 'Underwriting Queue & Case Details', category: 'Underwriting', icon: 'fact_check', description: 'Triaged proposals awaiting medical, financial, or lifestyle risk assessment.' }
      },
      {
        path: 'assessment',
        component: FeaturePlaceholderComponent,
        data: { title: 'Medical & Financial Assessment', category: 'Underwriting', icon: 'clinical_notes', description: 'Pathology reports, MER evaluation, ITR filings, and debt-to-income analysis.' }
      },
      {
        path: 'risk',
        component: FeaturePlaceholderComponent,
        data: { title: 'Risk Score & Loading', category: 'Underwriting', icon: 'speed', description: 'Automated scoring engine, BMI loadings, occupational hazards, and smoker surcharges.' }
      },
      {
        path: 'decision',
        component: FeaturePlaceholderComponent,
        data: { title: 'Underwriting Decision', category: 'Underwriting', icon: 'how_to_reg', description: 'Approve, Reject, Refer to Chief Underwriter, or propose Counter-Offer terms.' }
      },
      {
        path: 'rules',
        component: FeaturePlaceholderComponent,
        data: { title: 'Underwriting Rules Engine', category: 'Underwriting', icon: 'tune', description: 'STP (Straight-Through-Processing) limits and rule matrix configuration.' }
      },
      {
        path: 'audit',
        component: FeaturePlaceholderComponent,
        data: { title: 'Underwriting Audit Log', category: 'Underwriting', icon: 'history', description: 'Complete decision history, underwriter notes, and modified terms log.' }
      }
    ]
  },
  // Policy Issuance
  {
    path: 'policies',
    children: [
      { path: '', redirectTo: 'issue', pathMatch: 'full' },
      { path: 'issue', component: PolicyIssueComponent },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy List & Master Details', category: 'Policy Issuance', icon: 'policy', description: 'Central policy master repository with coverage, riders, and nominees.' }
      },
      {
        path: 'endorsements',
        component: FeaturePlaceholderComponent,
        data: { title: 'Endorsements & Policy History', category: 'Policy Issuance', icon: 'edit_document', description: 'Financial and non-financial endorsement processing and revision logs.' }
      },
      {
        path: 'templates',
        component: FeaturePlaceholderComponent,
        data: { title: 'Document Templates', category: 'Policy Issuance', icon: 'draw', description: 'Manage dynamic PDF policy schedules, stamp paper bindings, and email templates.' }
      }
    ]
  },
  // Premium & Finance
  {
    path: 'premiums',
    children: [
      { path: '', redirectTo: 'schedules', pathMatch: 'full' },
      {
        path: 'schedules',
        component: FeaturePlaceholderComponent,
        data: { title: 'Premium Schedules & Installments', category: 'Premium & Finance', icon: 'calendar_month', description: 'Company-wide installment forecasts, billing cycles, and NACH/e-mandate batches.' }
      },
      {
        path: 'verification',
        component: FeaturePlaceholderComponent,
        data: { title: 'Payment Verification & Offline Receipts', category: 'Premium & Finance', icon: 'price_check', description: 'Cheque clearance reconciliation, NEFT matching, and manual receipt creation.' }
      },
      {
        path: 'grace',
        component: FeaturePlaceholderComponent,
        data: { title: 'Grace Period & Late Fee Config', category: 'Premium & Finance', icon: 'more_time', description: 'Configure 15/30-day grace periods, interest rates for revival, and penalty waivers.' }
      },
      {
        path: 'reminders',
        component: FeaturePlaceholderComponent,
        data: { title: 'Automated Reminders', category: 'Premium & Finance', icon: 'send_time_extension', description: 'Trigger SMS, WhatsApp, and Email notices for upcoming and overdue premiums.' }
      },
      {
        path: 'refunds',
        component: FeaturePlaceholderComponent,
        data: { title: 'Refund Management', category: 'Premium & Finance', icon: 'currency_exchange', description: 'Approve cooling-off period refunds, excess premium payouts, and NEFT authorizations.' }
      }
    ]
  },
  // Policy Servicing
  {
    path: 'servicing',
    children: [
      { path: '', redirectTo: 'queue', pathMatch: 'full' },
      {
        path: 'queue',
        component: FeaturePlaceholderComponent,
        data: { title: 'Service Request Queue & Details', category: 'Policy Servicing', icon: 'build', description: 'Customer service tickets: address modifications, contact edits, nominee updates.' }
      },
      {
        path: 'approve',
        component: FeaturePlaceholderComponent,
        data: { title: 'Approve Risk-Related / Special Requests', category: 'Policy Servicing', icon: 'approval', description: 'Supervisory sign-off on sum insured enhancements and risk changes.' }
      },
      {
        path: 'cancellations',
        component: FeaturePlaceholderComponent,
        data: { title: 'Cancellations & Surrenders', category: 'Policy Servicing', icon: 'cancel', description: 'Surrender value calculation, free-look cancellations, and policy termination.' }
      }
    ]
  },
  // Renewals & Closure
  {
    path: 'renewals',
    children: [
      { path: '', redirectTo: 'due', pathMatch: 'full' },
      {
        path: 'due',
        component: FeaturePlaceholderComponent,
        data: { title: 'Renewal Due List', category: 'Renewals & Closure', icon: 'autorenew', description: 'Portfolio-wide renewal calendar, persistency tracking, and notice generation.' }
      },
      {
        path: 'process',
        component: FeaturePlaceholderComponent,
        data: { title: 'Renewal Processing', category: 'Renewals & Closure', icon: 'published_with_changes', description: 'Batch policy renewal processing, NCB rollover, and schedule generation.' }
      },
      {
        path: 'closure',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy Closure', category: 'Renewals & Closure', icon: 'lock', description: 'Maturity processing, death claim closure, and lapsed policy archive.' }
      }
    ]
  },
  // Claims
  {
    path: 'claims',
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        component: FeaturePlaceholderComponent,
        data: { title: 'Claims Master List & Details', category: 'Claims Management', icon: 'gavel', description: 'Enterprise claim queue with SLA breach timers, severity tags, and claim history.' }
      },
      {
        path: 'verification',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy & Document Verification', category: 'Claims Management', icon: 'verified', description: 'Verify policy active status, premium paid to date, coverage inclusions, and limits.' }
      },
      {
        path: 'surveyor',
        component: FeaturePlaceholderComponent,
        data: { title: 'Surveyor Assignment & Report', category: 'Claims Management', icon: 'assignment_ind', description: 'Assign licensed loss adjusters, receive field survey photos, and estimate reports.' }
      },
      {
        path: 'investigation',
        component: FeaturePlaceholderComponent,
        data: { title: 'Investigation (Fraud / Suspicious)', category: 'Claims Management', icon: 'screen_search_desktop', description: 'Forensic checks, hospital verification, anti-fraud algorithms, and investigation notes.' }
      },
      {
        path: 'assessment',
        component: FeaturePlaceholderComponent,
        data: { title: 'Assessment & Decision', category: 'Claims Management', icon: 'rate_review', description: 'Calculate admissible losses, apply deductibles and co-pay, and approve/reject claim.' }
      },
      {
        path: 'settlement',
        component: FeaturePlaceholderComponent,
        data: { title: 'Settlement & Payment Release', category: 'Claims Management', icon: 'payments', description: 'Authorize payout disbursement via banking gateway with UTR reference.' }
      },
      {
        path: 'history',
        component: FeaturePlaceholderComponent,
        data: { title: 'Claim History & Analytics', category: 'Claims Management', icon: 'history', description: 'Comprehensive loss ratios, turnaround time (TAT) metrics, and claim audit trails.' }
      }
    ]
  },
  // Commission & Payouts
  {
    path: 'commissions',
    children: [
      { path: '', redirectTo: 'rules', pathMatch: 'full' },
      {
        path: 'rules',
        component: FeaturePlaceholderComponent,
        data: { title: 'Commission Rules Engine', category: 'Commission & Payouts', icon: 'rule', description: 'Rule maker/checker, agent tier commission matrix, and clawback regulations.' }
      },
      {
        path: 'review',
        component: FeaturePlaceholderComponent,
        data: { title: 'Calculate / Review Commissions', category: 'Commission & Payouts', icon: 'checklist', description: 'Monthly commission computation batch, approval sign-off, and reversals.' }
      },
      {
        path: 'payouts',
        component: FeaturePlaceholderComponent,
        data: { title: 'Process Bulk Payouts', category: 'Commission & Payouts', icon: 'monetization_on', description: 'Generate banking NACH payout files, TDS deduction batches, and payment receipts.' }
      },
      {
        path: 'history',
        component: FeaturePlaceholderComponent,
        data: { title: 'Payout History', category: 'Commission & Payouts', icon: 'account_balance', description: 'Archived payout runs with disbursement status, transaction IDs, and TDS logs.' }
      },
      {
        path: 'reconcile',
        component: FeaturePlaceholderComponent,
        data: { title: 'Reconciliation', category: 'Commission & Payouts', icon: 'balance', description: 'Bank statement auto-reconciliation, failed payout retries, and ledger adjustments.' }
      }
    ]
  },
  // Master Data
  {
    path: 'masters',
    children: [
      { path: '', redirectTo: 'department', pathMatch: 'full' },
      {
        path: 'department',
        component: FeaturePlaceholderComponent,
        data: { title: 'Department, Designation & Branch', category: 'Master Data', icon: 'domain', description: 'Organizational hierarchy, branches, cost centers, and departmental mapping.' }
      },
      {
        path: 'product-type',
        component: FeaturePlaceholderComponent,
        data: { title: 'Product, Policy Type & Risk Category', category: 'Master Data', icon: 'category', description: 'Taxonomic categorization of insurance products and risk grades.' }
      },
      {
        path: 'coverage',
        component: FeaturePlaceholderComponent,
        data: { title: 'Coverage & Riders Master', category: 'Master Data', icon: 'security', description: 'Standardized rider codes, critical illness bundles, and add-on libraries.' }
      },
      {
        path: 'payment-mode',
        component: FeaturePlaceholderComponent,
        data: { title: 'Premium Frequency & Payment Mode', category: 'Master Data', icon: 'payment', description: 'Supported gateways, payment frequencies, bank accounts, and merchant IDs.' }
      },
      {
        path: 'claim-type',
        component: FeaturePlaceholderComponent,
        data: { title: 'Claim Type & Document Master', category: 'Master Data', icon: 'folder', description: 'Document checklist requirements by claim classification and policy type.' }
      },
      {
        path: 'geography',
        component: FeaturePlaceholderComponent,
        data: { title: 'Country, State, City & Tax Master', category: 'Master Data', icon: 'map', description: 'Geographical boundaries, postal codes, and applicable state GST tax definitions.' }
      }
    ]
  },
  // Reports & Analytics
  {
    path: 'reports',
    children: [
      { path: '', redirectTo: 'analytics', pathMatch: 'full' },
      {
        path: 'customer',
        component: FeaturePlaceholderComponent,
        data: { title: 'Customer Reports', category: 'Reports & Analytics', icon: 'groups', description: 'Customer demographics, policy uptake, retention, and churn analysis.' }
      },
      {
        path: 'policy',
        component: FeaturePlaceholderComponent,
        data: { title: 'Policy Reports', category: 'Reports & Analytics', icon: 'policy', description: 'Total portfolio premium, active vs lapsed breakdown, and product mix.' }
      },
      {
        path: 'premium',
        component: FeaturePlaceholderComponent,
        data: { title: 'Premium & Collection Reports', category: 'Reports & Analytics', icon: 'paid', description: 'Collection efficiency, overdue premiums, and grace period recovery rates.' }
      },
      {
        path: 'claims',
        component: FeaturePlaceholderComponent,
        data: { title: 'Claims Ratio Reports', category: 'Reports & Analytics', icon: 'gavel', description: 'Incurred claims ratio (ICR), average settlement time, and repudiation rates.' }
      },
      {
        path: 'financial',
        component: FeaturePlaceholderComponent,
        data: { title: 'Underwriting, Agent & Financial Reports', category: 'Reports & Analytics', icon: 'analytics', description: 'Underwriting STP percentages, agent commission payouts, and profitability.' }
      },
      {
        path: 'analytics',
        component: FeaturePlaceholderComponent,
        data: { title: 'Dashboard Analytics', category: 'Reports & Analytics', icon: 'insights', description: 'Interactive visual analytics, charts, predictive renewal models, and KPI widgets.' }
      },
      {
        path: 'export',
        component: FeaturePlaceholderComponent,
        data: { title: 'Scheduled Reports & Data Export', category: 'Reports & Analytics', icon: 'file_download', description: 'Automated periodic email deliveries in Excel, CSV, and PDF formats.' }
      }
    ]
  },
  // Administration
  {
    path: 'system',
    children: [
      { path: '', redirectTo: 'notifications', pathMatch: 'full' },
      {
        path: 'notifications',
        component: FeaturePlaceholderComponent,
        data: { title: 'Notification Templates', category: 'Administration', icon: 'mail', description: 'SMS, Email, and Push notification template editors with dynamic merge tags.' }
      },
      {
        path: 'config',
        component: FeaturePlaceholderComponent,
        data: { title: 'System Configuration', category: 'Administration', icon: 'settings_suggest', description: 'Application parameters, SMTP credentials, API keys, and maintenance toggles.' }
      },
      {
        path: 'audit',
        component: FeaturePlaceholderComponent,
        data: { title: 'System Audit Logs', category: 'Administration', icon: 'receipt_long', description: 'Enterprise security audit trail: logins, privilege escalations, and critical mutations.' }
      },
      {
        path: 'auth',
        component: FeaturePlaceholderComponent,
        data: { title: 'Authentication Settings', category: 'Administration', icon: 'security', description: 'MFA policy enforcement, SSO / SAML configurations, and password complexity rules.' }
      }
    ]
  }
];
