
import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReportsService } from '../../services/Reports/report-services';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reports.html',
  styleUrl: './reports.scss'
})
export class Reports implements OnInit {

  private service = inject(ReportsService);

  // Selected report
  selectedCategory = 'Customers';

  // Report data
  reportData: any[] = [];
  reportColumns: string[] = [];

  // UI states
  loading = false;
  scheduleLoading = false;
  errorMessage = '';
  successMessage = '';

  // Filters
  reportType = '';
  fromDate = '';
  toDate = '';
  status = '';

  // Schedule form
  scheduleReportType = 'Customers';
  scheduleType = 'Daily';
  nextRunDate = '';
  emailTo = '';

  // Schedule list
  schedules: any[] = [];

  // Edit schedule
  editingScheduleId: number | null = null;

  // Report categories
  categories = [
    {
      name: 'Customers',
      icon: 'bi-people',
      description: 'Customer registration and KYC records',
      endpoint: 'customers'
    },
    {
      name: 'Policies',
      icon: 'bi-file-earmark-text',
      description: 'Policy status and policy records',
      endpoint: 'policies'
    },
    {
      name: 'Premiums',
      icon: 'bi-currency-rupee',
      description: 'Premium collection and payment records',
      endpoint: 'premiums'
    },
    {
      name: 'Claims',
      icon: 'bi-clipboard-check',
      description: 'Claim registration and settlement records',
      endpoint: 'claims'
    },
    {
      name: 'Underwriting',
      icon: 'bi-shield-check',
      description: 'Underwriting decisions and verification',
      endpoint: 'underwriting'
    },
    {
      name: 'Agents',
      icon: 'bi-person-badge',
      description: 'Agent performance and sales records',
      endpoint: 'agents'
    },
    {
      name: 'Financial',
      icon: 'bi-graph-up-arrow',
      description: 'Financial and revenue reports',
      endpoint: 'financial'
    }
  ];

  // Report options
  reportOptions: { [key: string]: string[] } = {
    Customers: [
      'Customer Registration Report',
      'KYC Pending Report',
      'Active Customers Report',
      'Inactive Customers Report',
      'City-wise Customer Report',
      'State-wise Customer Report'
    ],

    Policies: [
      'Issued Policies Report',
      'Active Policies Report',
      'Expired Policies Report',
      'Cancelled Policies Report',
      'Renewal Due Report',
      'Product-wise Policy Report',
      'Branch-wise Policy Report'
    ],

    Premiums: [
      'Daily Premium Collection',
      'Monthly Premium Collection',
      'Yearly Premium Collection',
      'Pending Premium Report',
      'Overdue Premium Report',
      'Premium Frequency Report',
      'Refund Report'
    ],

    Claims: [
      'Registered Claims Report',
      'Pending Claims Report',
      'Approved Claims Report',
      'Rejected Claims Report',
      'Settled Claims Report',
      'High Value Claims Report',
      'Fraud Investigation Report'
    ],

    Underwriting: [
      'Pending Underwriting Report',
      'Approved Underwriting Report',
      'Rejected Underwriting Report',
      'Medical Pending Report',
      'High Risk Customers Report',
      'Underwriter Performance Report'
    ],

    Agents: [
      'Agent Sales Report',
      'Premium Collection Report',
      'Claim Ratio Report',
      'Active Customers Report',
      'Commission Report',
      'Monthly Performance Report'
    ],

    Financial: [
      'Premium Income Report',
      'Claim Payments Report',
      'Outstanding Premium Report',
      'Refund Report',
      'Branch Revenue Report',
      'Monthly Profit Summary'
    ]
  };

  // Schedule options
  scheduleOptions = [
    'Daily',
    'Weekly',
    'Monthly',
    'Yearly'
  ];

  ngOnInit(): void {
    this.loadSchedules();
  }

  // Available report options
  get availableReports(): string[] {
    return this.reportOptions[this.selectedCategory] || [];
  }

  // Selected category
  selectCategory(category: string): void {
    this.selectedCategory = category;

    this.reportType = '';
    this.fromDate = '';
    this.toDate = '';
    this.status = '';

    this.reportData = [];
    this.reportColumns = [];

    this.errorMessage = '';
    this.successMessage = '';
  }

  // View report
  viewReport(category: string): void {
    this.selectCategory(category);

    // Select first report type
    this.reportType = this.availableReports[0] || '';

    this.generateReport();
  }

  // Generate report
  generateReport(): void {
    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const filter: any = {};

    if (this.fromDate) {
      filter.fromDate = this.fromDate;
    }

    if (this.toDate) {
      filter.toDate = this.toDate;
    }

    if (this.status) {
      filter.status = this.status;
    }

    if (this.reportType) {
      filter.reportType = this.reportType;
    }

    let request;

    switch (this.selectedCategory) {
      case 'Customers':
        request = this.service.GetCustomerReports(filter);
        break;

      case 'Policies':
        request = this.service.GetPolicyReports(filter);
        break;

      case 'Premiums':
        request = this.service.GetPremiumReports(filter);
        break;

      case 'Claims':
        request = this.service.GetClaimsReports(filter);
        break;

      case 'Underwriting':
        request = this.service.GetUnderwritingReports(filter);
        break;

      case 'Agents':
        request = this.service.GetAgentReports(filter);
        break;

      case 'Financial':
        request = this.service.GetFinancialReports(filter);
        break;

      default:
        this.loading = false;
        this.errorMessage = 'Please select a report category.';
        return;
    }

    request.subscribe({
      next: (response: any) => {
        const data = this.extractData(response);

        this.reportData = this.normalizeData(data);

        this.reportColumns = this.reportData.length
          ? Object.keys(this.reportData[0])
          : [];

        this.loading = false;

        if (this.reportData.length === 0) {
          this.successMessage = 'Report generated successfully. No records found.';
        } else {
          this.successMessage = 'Report generated successfully.';
        }
      },

      error: (error: any) => {
        console.error('Report API error:', error);

        this.reportData = [];
        this.reportColumns = [];

        this.loading = false;

        this.errorMessage =
          error?.error?.message ||
          error?.error?.Message ||
          'Unable to load report. Please check the API connection.';
      }
    });
  }

  // Extract ApiResponse.Data
  private extractData(response: any): any {
    return response?.data ??
      response?.Data ??
      response;
  }

  // Normalize response to table rows
  private normalizeData(data: any): any[] {
    if (Array.isArray(data)) {
      return data;
    }

    if (data && typeof data === 'object') {

      // Common API response collection properties
      const collectionKeys = [
        'items',
        'Items',
        'records',
        'Records',
        'results',
        'Results'
      ];

      for (const key of collectionKeys) {
        if (Array.isArray(data[key])) {
          return data[key];
        }
      }

      // Single object response
      return [data];
    }

    return [];
  }

  // Format column names
  formatColumnName(column: string): string {
    return column
      .replace(/([a-z])([A-Z])/g, '$1 $2')
      .replace(/_/g, ' ')
      .replace(/\b\w/g, char => char.toUpperCase());
  }

  // Format cell values
  formatValue(value: any): string {
    if (value === null || value === undefined) {
      return '-';
    }

    if (typeof value === 'boolean') {
      return value ? 'Yes' : 'No';
    }

    if (typeof value === 'object') {
      return JSON.stringify(value);
    }

    return String(value);
  }

  // Reset filters
  resetFilters(): void {
    this.reportType = '';
    this.fromDate = '';
    this.toDate = '';
    this.status = '';

    this.reportData = [];
    this.reportColumns = [];

    this.errorMessage = '';
    this.successMessage = '';
  }

  // Export report to CSV
  exportCSV(): void {
    if (!this.reportData.length) {
      this.errorMessage = 'No report data available to export.';
      return;
    }

    const columns = this.reportColumns;

    const header = columns
      .map(column => this.escapeCSV(this.formatColumnName(column)))
      .join(',');

    const rows = this.reportData.map(row =>
      columns
        .map(column => this.escapeCSV(this.formatValue(row[column])))
        .join(',')
    );

    const csvContent = [header, ...rows].join('\r\n');

    const blob = new Blob(
      ['\uFEFF' + csvContent],
      { type: 'text/csv;charset=utf-8;' }
    );

    const url = window.URL.createObjectURL(blob);

    const link = document.createElement('a');

    link.href = url;

    link.download =
      `${this.selectedCategory.toLowerCase()}-report-${new Date()
        .toISOString()
        .slice(0, 10)}.csv`;

    link.click();

    window.URL.revokeObjectURL(url);
  }

  // Escape CSV values
  private escapeCSV(value: string): string {
    const escaped = value.replace(/"/g, '""');

    return `"${escaped}"`;
  }

  // Load saved schedules
  loadSchedules(): void {
    this.scheduleLoading = true;

    this.service.GetReportSchedules().subscribe({
      next: (response: any) => {
        const data = this.extractData(response);

        this.schedules = this.normalizeData(data);

        this.scheduleLoading = false;
      },

      error: (error: any) => {
        console.error('Schedule API error:', error);

        this.schedules = [];

        this.scheduleLoading = false;
      }
    });
  }

  // Add or update schedule
  saveSchedule(): void {
    this.errorMessage = '';
    this.successMessage = '';

    if (!this.scheduleReportType) {
      this.errorMessage = 'Please select report type.';
      return;
    }

    if (!this.scheduleType) {
      this.errorMessage = 'Please select schedule type.';
      return;
    }

    if (!this.nextRunDate) {
      this.errorMessage = 'Please select next run date.';
      return;
    }

    if (!this.emailTo || !this.isValidEmail(this.emailTo)) {
      this.errorMessage = 'Please enter a valid email address.';
      return;
    }

    const scheduleData = {
      reportType: this.scheduleReportType,
      scheduleType: this.scheduleType,
      nextRunDate: this.nextRunDate,
      emailTo: this.emailTo
    };

    if (this.editingScheduleId !== null) {

      this.service
        .UpdateReportSchedule(this.editingScheduleId, scheduleData)
        .subscribe({
          next: () => {
            this.successMessage = 'Schedule updated successfully.';

            this.resetScheduleForm();
            this.loadSchedules();
          },

          error: (error: any) => {
            console.error(error);

            this.errorMessage =
              error?.error?.message ||
              'Unable to update schedule.';
          }
        });

    } else {

      this.service.CreateReportSchedule(scheduleData).subscribe({
        next: () => {
          this.successMessage = 'Schedule created successfully.';

          this.resetScheduleForm();
          this.loadSchedules();
        },

        error: (error: any) => {
          console.error(error);

          this.errorMessage =
            error?.error?.message ||
            'Unable to create schedule.';
        }
      });
    }
  }

  // Edit schedule
  editSchedule(schedule: any): void {
    const id =
      schedule.id ??
      schedule.Id ??
      schedule.reportScheduleId ??
      schedule.ReportScheduleId;

    if (id === undefined || id === null) {
      this.errorMessage = 'Schedule ID not found.';
      return;
    }

    this.editingScheduleId = Number(id);

    this.scheduleReportType =
      schedule.reportType ??
      schedule.ReportType ??
      'Customers';

    this.scheduleType =
      schedule.scheduleType ??
      schedule.ScheduleType ??
      'Daily';

    this.nextRunDate = this.toDateInputValue(
      schedule.nextRunDate ??
      schedule.NextRunDate
    );

    this.emailTo =
      schedule.emailTo ??
      schedule.EmailTo ??
      '';

    this.errorMessage = '';
    this.successMessage = '';

    document.getElementById('scheduled-reports')?.scrollIntoView({
      behavior: 'smooth',
      block: 'start'
    });
  }

  // Delete schedule
  deleteSchedule(schedule: any): void {
    const id =
      schedule.id ??
      schedule.Id ??
      schedule.reportScheduleId ??
      schedule.ReportScheduleId;

    if (id === undefined || id === null) {
      this.errorMessage = 'Schedule ID not found.';
      return;
    }

    const confirmed = window.confirm(
      'Are you sure you want to delete this schedule?'
    );

    if (!confirmed) {
      return;
    }

    this.service.DeleteReportSchedule(Number(id)).subscribe({
      next: () => {
        this.successMessage = 'Schedule deleted successfully.';

        this.loadSchedules();
      },

      error: (error: any) => {
        console.error(error);

        this.errorMessage =
          error?.error?.message ||
          'Unable to delete schedule.';
      }
    });
  }

  // Cancel schedule editing
  cancelEdit(): void {
    this.resetScheduleForm();
  }

  // Reset schedule form
  resetScheduleForm(): void {
    this.editingScheduleId = null;

    this.scheduleReportType = 'Customers';
    this.scheduleType = 'Daily';
    this.nextRunDate = '';
    this.emailTo = '';
  }

  // Validate email
  private isValidEmail(email: string): boolean {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }

  // Convert date for input type date
  private toDateInputValue(value: any): string {
    if (!value) {
      return '';
    }

    const date = new Date(value);

    if (isNaN(date.getTime())) {
      return '';
    }

    return date.toISOString().slice(0, 10);
  }

  // Get schedule property
  getScheduleValue(schedule: any, property: string): any {
    const lower = property.charAt(0).toLowerCase() + property.slice(1);

    return schedule?.[lower] ??
      schedule?.[property] ??
      '-';
  }

  // Get schedule ID
  getScheduleId(schedule: any): any {
    return schedule?.id ??
      schedule?.Id ??
      schedule?.reportScheduleId ??
      schedule?.ReportScheduleId ??
      '-';
  }

  // Get schedule status
  getScheduleStatus(schedule: any): string {
    return schedule?.status ??
      schedule?.Status ??
      'Active';
  }

  // Get status CSS
  getStatusClass(schedule: any): string {
    const status = String(this.getScheduleStatus(schedule)).toLowerCase();

    if (status === 'active') {
      return 'status-active';
    }

    if (status === 'inactive') {
      return 'status-inactive';
    }

    return 'status-default';
  }
}






// import { Component } from '@angular/core';
// import { CommonModule } from '@angular/common';
// import {
//   FormControl,
//   FormGroup,
//   ReactiveFormsModule
// } from '@angular/forms';
// import { ReportsService } from '../../services/Reports/report-services';


// @Component({
//   selector: 'app-reports',
//   standalone: true,
//   imports: [CommonModule,ReactiveFormsModule],
//   templateUrl: './reports.html',
//   styleUrl: './reports.scss'
// })
// export class Reports {

//   constructor(
//     private service: ReportsService
//   ) {}

//   // Selected category
//   selectedCategory = 'Customer';

//   // Report data
//   reportData: any[] = [];

//   // Loading
//   loading = false;

//   // Error message
//   errorMessage = '';

//   // Report generated message
//   successMessage = '';

//   // Export object for HTML
//   Object = Object;

//   // Filters
//   reportForm = new FormGroup({
//     reportType: new FormControl(''),
//     fromDate: new FormControl(''),
//     toDate: new FormControl(''),
//     status: new FormControl('')
//   });

//   // Report categories
//   categories = [
//     {
//       name: 'Customer',
//       icon: 'bi-people',
//       description: 'Customer information and registration'
//     },
//     {
//       name: 'Policy',
//       icon: 'bi-file-earmark-text',
//       description: 'Policy status and renewal'
//     },
//     {
//       name: 'Premium',
//       icon: 'bi-currency-rupee',
//       description: 'Premium collection and payment'
//     },
//     {
//       name: 'Claims',
//       icon: 'bi-clipboard-check',
//       description: 'Claims processing and settlement'
//     },
//     {
//       name: 'Underwriting',
//       icon: 'bi-shield-check',
//       description: 'Underwriting and risk assessment'
//     },
//     {
//       name: 'Agent',
//       icon: 'bi-person-badge',
//       description: 'Agent sales and performance'
//     },
//     {
//       name: 'Financial',
//       icon: 'bi-graph-up-arrow',
//       description: 'Financial and revenue reports'
//     }
//   ];

//   // Report options according to BRD
//   reportOptions: any = {

//     Customer: [
//       'Customer Registration Report',
//       'Customer KYC Pending Report',
//       'Active Customers',
//       'Inactive Customers',
//       'City-wise Customers',
//       'State-wise Customers',
//       'Product-wise Customers'
//     ],

//     Policy: [
//       'Policy Issued Report',
//       'Active Policies',
//       'Expired Policies',
//       'Cancelled Policies',
//       'Renewal Due Report',
//       'Product-wise Policy Report',
//       'Branch-wise Policy Report'
//     ],

//     Premium: [
//       'Daily Collection',
//       'Monthly Collection',
//       'Yearly Collection',
//       'Pending Premium',
//       'Overdue Premium',
//       'Premium Frequency Report',
//       'Refund Report'
//     ],

//     Claims: [
//       'Claims Registered',
//       'Pending Claims',
//       'Approved Claims',
//       'Rejected Claims',
//       'Settled Claims',
//       'High Value Claims',
//       'Fraud Investigation Report'
//     ],

//     Underwriting: [
//       'Pending Cases',
//       'Approved Cases',
//       'Rejected Cases',
//       'Medical Pending',
//       'High Risk Customers',
//       'Underwriter Performance'
//     ],

//     Agent: [
//       'Agent Sales',
//       'Premium Collection',
//       'Claim Ratio',
//       'Active Customers',
//       'Commission Report',
//       'Monthly Performance'
//     ],

//     Financial: [
//       'Premium Income',
//       'Claim Payments',
//       'Outstanding Premium',
//       'Refund Report',
//       'Branch Revenue',
//       'Monthly Profit Summary'
//     ]

//   };

//   // Available reports
//   get availableReports(): string[] {

//     return this.reportOptions[this.selectedCategory] || [];

//   }

//   // Selected category details
//   get selectedCategoryDetails() {

//     return this.categories.find(
//       category => category.name === this.selectedCategory
//     );

//   }

//   // Total reports available
//   get totalReports(): number {

//     return this.availableReports.length;

//   }

//   // Change category
//   selectCategory(category: string) {

//     this.selectedCategory = category;

//     this.reportForm.reset();

//     this.reportData = [];

//     this.errorMessage = '';

//     this.successMessage = '';

//   }

//   // Generate report
//   GenerateReport() {

//     const formValue = this.reportForm.value;

//     const reportType = formValue.reportType;

//     if (!reportType) {

//       alert('Please select a report.');

//       return;

//     }

//     // Validate dates
//     if (
//       formValue.fromDate &&
//       formValue.toDate &&
//       formValue.fromDate > formValue.toDate
//     ) {

//       this.errorMessage =
//         'From Date cannot be greater than To Date.';

//       return;

//     }

//     this.loading = true;

//     this.errorMessage = '';

//     this.successMessage = '';

//     this.reportData = [];

//     let request: any;

//     // Select report API
//     switch (this.selectedCategory) {

//       case 'Customer':

//         request = this.service.GetCustomerReports();

//         break;

//       case 'Policy':

//         request = this.service.GetPolicyReports();

//         break;

//       case 'Premium':

//         request = this.service.GetPremiumReports();

//         break;

//       case 'Claims':

//         request = this.service.GetClaimsReports();

//         break;

//       case 'Underwriting':

//         request = this.service.GetUnderwritingReports();

//         break;

//       case 'Agent':

//         request = this.service.GetAgentReports();

//         break;

//       case 'Financial':

//         request = this.service.GetFinancialReports();

//         break;

//       default:

//         this.loading = false;

//         return;

//     }

//     // API response
//     request.subscribe({

//       next: (res: any) => {

//         console.log('Report API Response:', res);

//         const data =
//           res?.data ??
//           res?.Data ??
//           res;

//         this.reportData = Array.isArray(data)
//           ? data
//           : [];

//         this.loading = false;

//         if (this.reportData.length > 0) {

//           this.successMessage =
//             `${reportType} generated successfully.`;

//         } else {

//           this.successMessage = '';

//         }

//       },

//       error: (err: any) => {

//         console.error('Report API Error:', err);

//         this.errorMessage =
//           'Unable to load report. Please check the API connection.';

//         this.reportData = [];

//         this.loading = false;

//       }

//     });

//   }

//   // Reset filters
//   ResetFilters() {

//     this.reportForm.reset();

//     this.reportData = [];

//     this.errorMessage = '';

//     this.successMessage = '';

//   }

//   // Export CSV
//   ExportCSV() {

//     if (!this.reportData.length) {

//       alert('Generate report first.');

//       return;

//     }

//     const headers =
//       Object.keys(this.reportData[0]);

//     const csvHeaders = headers.map(
//       header => this.escapeCSV(header)
//     );

//     const rows = this.reportData.map(
//       (item: any) => {

//         return headers.map(
//           (header: string) => {

//             const value = item[header] ?? '';

//             return this.escapeCSV(value);

//           }
//         ).join(',');

//       }
//     );

//     const csv = [
//       csvHeaders.join(','),
//       ...rows
//     ].join('\r\n');

//     const blob = new Blob(
//       ['\uFEFF' + csv],
//       {
//         type: 'text/csv;charset=utf-8;'
//       }
//     );

//     const url = URL.createObjectURL(blob);

//     const link = document.createElement('a');

//     link.href = url;

//     link.download =
//       `${this.selectedCategory}-Report.csv`;

//     document.body.appendChild(link);

//     link.click();

//     document.body.removeChild(link);

//     URL.revokeObjectURL(url);

//   }

//   // Escape CSV values
//   escapeCSV(value: any): string {

//     const text = String(value ?? '');

//     return `"${text.replace(/"/g, '""')}"`;

//   }

//   // Get table columns
//   get reportColumns(): string[] {

//     if (!this.reportData.length) {

//       return [];

//     }

//     return Object.keys(this.reportData[0]);

//   }

//   // Format column heading
//   formatColumnName(column: string): string {

//     return column
//       .replace(/([A-Z])/g, ' $1')
//       .replace(/^./, char => char.toUpperCase());

//   }

// }
