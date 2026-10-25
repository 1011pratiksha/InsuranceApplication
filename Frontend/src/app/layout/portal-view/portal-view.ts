import { CommonModule } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { filter } from 'rxjs/operators';
import { ClaimService, UserRole } from '../../services/claim-management/claim-service';

export interface ModuleRecord {
  col1: string;
  col2: string;
  col3: string;
  col4: string;
  status: string;
  statusClass: string;
}

@Component({
  selector: 'app-portal-view',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './portal-view.html',
  styleUrl: './portal-view.scss'
})
export class PortalViewComponent {
  private router = inject(Router);
  claimService = inject(ClaimService);

  get currentRole(): UserRole {
    return this.claimService.currentRole;
  }

  // Reactive URL signal
  urlSignal = signal<string>(this.router.url);

  constructor() {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        this.urlSignal.set(event.urlAfterRedirects || event.url);
      });
  }

  isDashboard = computed(() => {
    const url = this.urlSignal().split('?')[0];
    return url.endsWith('/dashboard');
  });

  // Compute breadcrumbs, title, category, and description from the URL
  moduleInfo = computed(() => {
    const url = this.urlSignal().split('?')[0];
    const segments = url.split('/').filter(s => s.length > 0);

    const roleName = segments[0] ? this.formatSegment(segments[0]) : this.currentRole;
    const categoryName = segments[1] ? this.formatSegment(segments[1]) : 'Dashboard';
    const subSectionName = segments[2] ? this.formatSegment(segments[2]) : null;

    let title = subSectionName
      ? `${subSectionName}`
      : `${categoryName}`;

    if (categoryName.toLowerCase() === 'dashboard') {
      title = `${roleName} Dashboard`;
    }

    const subtitle = subSectionName
      ? `Manage ${subSectionName.toLowerCase()} under ${categoryName}`
      : `Operational module view for ${categoryName}`;

    return {
      url,
      roleName,
      categoryName,
      subSectionName,
      title,
      subtitle,
      breadcrumbs: segments.map(s => this.formatSegment(s))
    };
  });

  // Role-specific quick navigation links
  roleQuickLinks = computed(() => {
    const role = this.currentRole;
    if (role === 'Customer') {
      return [
        { title: 'My Policies', desc: 'View active coverage & download documents', route: '/customer/policies/list' },
        { title: 'Pay Premium', desc: 'Review schedule and pay due installment', route: '/customer/premiums/pay' },
        { title: 'Request Quote', desc: 'Calculate premium for new health or motor policy', route: '/customer/quotes/request' },
        { title: 'Raise Claim', desc: 'Submit incident report & upload bills', route: '/customer/claims/raise' },
        { title: 'KYC Verification', desc: 'Aadhaar / PAN submission status', route: '/customer/kyc/status' }
      ];
    } else if (role === 'Agent') {
      return [
        { title: 'Register Customer', desc: 'Onboard new client profile & submit KYC', route: '/agent/customers/register' },
        { title: 'Generate Quote', desc: 'Create customized proposal with premium breakup', route: '/agent/quotes/generate' },
        { title: 'Collect Premium', desc: 'Record renewal or advance collection', route: '/agent/premiums/collect' },
        { title: 'Commission Summary', desc: 'Track pending and payable commission', route: '/agent/commissions/list' },
        { title: 'Payout History', desc: 'Review bank transfers and payment advice', route: '/agent/payouts/history' }
      ];
    } else {
      return [
        { title: 'User Management', desc: 'Manage staff, agents, and access permissions', route: '/admin/users/list' },
        { title: 'KYC Document Queue', desc: 'Pending document verification & approvals', route: '/admin/kyc/pending' },
        { title: 'Underwriting Queue', desc: 'Medical & financial risk assessment', route: '/admin/underwriting/cases' },
        { title: 'Issue Policy', desc: 'Approve cases and issue formal policy pack', route: '/admin/policies/issue' },
        { title: 'Bulk Commission Payout', desc: 'Reconcile and process agent payouts', route: '/admin/commissions/payouts' }
      ];
    }
  });

  // Module table headers configuration with empty data state
  tableData = computed(() => {
    const cat = this.moduleInfo().categoryName.toLowerCase();
    let headers: string[] = [];

    if (cat.includes('user') || cat.includes('agent')) {
      headers = ['User / Agent ID', 'Full Name & Email', 'Role & Dept', 'Assigned Territory', 'Status'];
    } else if (cat.includes('kyc')) {
      headers = ['Customer Name', 'Document Type', 'Document ID', 'Submission Date', 'Verification State'];
    } else if (cat.includes('product')) {
      headers = ['Product Plan', 'Category', 'Sum Insured Range', 'Premium Frequency', 'Status'];
    } else if (cat.includes('quote')) {
      headers = ['Quote Reference', 'Customer / Prospect', 'Sum Insured', 'Gross Premium', 'Status'];
    } else if (cat.includes('application') || cat.includes('underwriting')) {
      headers = ['Application No', 'Applicant Name', 'Selected Product', 'Risk Score', 'Underwriting State'];
    } else if (cat.includes('polic')) {
      headers = ['Policy Number', 'Insured Name', 'Coverage Type', 'Validity Term', 'Policy Status'];
    } else if (cat.includes('premium')) {
      headers = ['Schedule / Invoice', 'Policy Number', 'Due Date', 'Installment Amount', 'Payment Status'];
    } else if (cat.includes('commission') || cat.includes('payout')) {
      headers = ['Transaction Ref', 'Agent Name & Code', 'Policy Number', 'Commission (Rate)', 'Payout State'];
    } else {
      headers = ['Record ID', 'Module Entity / Title', 'Assigned Owner', 'Last Activity', 'Operational Status'];
    }

    return {
      headers,
      rows: [] as ModuleRecord[]
    };
  });

  private formatSegment(seg: string): string {
    return seg
      .split('-')
      .map(w => w.charAt(0).toUpperCase() + w.slice(1))
      .join(' ');
  }
}
