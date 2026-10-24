import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerRegistrationService } from '../../../services/customer-registration/customer-registration.service';
import {
  Customer,
  CustomerDetail,
  CreateCustomerRequest,
  CreateAddressRequest,
  CreateNomineeRequest,
  UploadDocumentRequest,
  CustomerAddress,
  Nominee,
  CustomerDocument
} from '../../../services/customer-registration/customer-registration.model';

@Component({
  selector: 'app-customer-registration',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-registration.component.html',
  styleUrl: './customer-registration.component.css'
})
export class CustomerRegistrationComponent implements OnInit {

  private service = inject(CustomerRegistrationService);
  private router = inject(Router);

  activeTab: 'register' | 'directory' = 'register';
  currentStep: number = 1;

  loading: boolean = false;
  submitting: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';

  customers: Customer[] = [];
  selectedCustomer: CustomerDetail | null = null;
  searchQuery: string = '';

  registeredCustomerId: number | null = null;
  registeredCustomerCode: string = '';
  addedAddresses: CustomerAddress[] = [];
  addedNominees: Nominee[] = [];
  addedDocuments: CustomerDocument[] = [];

  personalForm: CreateCustomerRequest = {
    firstName: '',
    middleName: '',
    lastName: '',
    dateOfBirth: '',
    gender: '',
    maritalStatus: '',
    occupation: '',
    annualIncome: 0,
    mobileNumber: '',
    email: ''
  };

  addressForm: CreateAddressRequest = {
    addressType: '',
    addressLine1: '',
    addressLine2: '',
    city: '',
    district: '',
    state: '',
    country: '',
    pincode: ''
  };

  nomineeForm: CreateNomineeRequest = {
    nomineeName: '',
    relationship: '',
    dateOfBirth: '',
    mobileNumber: '',
    sharePercentage: 0,
    guardianName: ''
  };

  documentForm: UploadDocumentRequest = {
    documentType: '',
    documentNumber: '',
    fileName: '',
    filePath: ''
  };

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loading = true;

    this.service.getAllCustomers().subscribe({
      next: (response: any) => {
        this.customers = Array.isArray(response)
          ? response
          : response?.data ?? response?.items ?? [];
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  searchCustomers(): void {
    if (!this.searchQuery.trim()) {
      this.loadCustomers();
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    this.service.searchCustomers(this.searchQuery.trim()).subscribe({
      next: (response: any) => {
        this.customers = response?.data ?? (Array.isArray(response) ? response : []);
        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage = err?.error?.message ?? 'Search failed.';
        this.loading = false;
      }
    });
  }

  switchTab(tab: 'register' | 'directory'): void {
    this.activeTab = tab;
    this.errorMessage = '';
    this.successMessage = '';
    if (tab === 'directory') {
      this.loadCustomers();
    }
  }

  viewCustomerDetails(id: number): void {
    this.loading = true;
    this.errorMessage = '';

    this.service.getCustomerById(id).subscribe({
      next: (response: CustomerDetail) => {
        this.selectedCustomer = response;
        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage = err?.error?.message ?? 'Failed to load customer details.';
        this.loading = false;
      }
    });
  }

  closeCustomerDetails(): void {
    this.selectedCustomer = null;
  }

  submitPersonalDetails(): void {
    if (this.submitting) return;

    if (!this.personalForm.firstName || !this.personalForm.lastName || !this.personalForm.email || !this.personalForm.mobileNumber) {
      this.errorMessage = 'Please fill in all mandatory personal fields.';
      return;
    }

    this.submitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.service.createCustomer(this.personalForm).subscribe({
      next: (response: any) => {
        this.submitting = false;
        this.successMessage = response?.message ?? 'Customer registered successfully.';

        this.service.getAllCustomers().subscribe({
          next: (list: Customer[]) => {
            const found = list.find(c => c.email.toLowerCase() === this.personalForm.email.toLowerCase());
            if (found) {
              this.registeredCustomerId = found.customerId;
              this.registeredCustomerCode = found.customerCode;
            }
            this.currentStep = 2;
          },
          error: () => {
            this.currentStep = 2;
          }
        });
      },
      error: (err: any) => {
        this.errorMessage = err?.error?.message ?? 'Failed to register customer.';
        this.submitting = false;
      }
    });
  }

  submitAddress(): void {
    if (this.submitting) return;

    if (!this.registeredCustomerId) {
      this.errorMessage = 'No registered customer found. Please complete Step 1 first.';
      return;
    }

    if (!this.addressForm.addressLine1 || !this.addressForm.city || !this.addressForm.state || !this.addressForm.pincode) {
      this.errorMessage = 'Please fill in all mandatory address fields.';
      return;
    }

    this.submitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.service.addAddress(this.registeredCustomerId, this.addressForm).subscribe({
      next: (response: any) => {
        this.submitting = false;
        this.successMessage = response?.message ?? 'Address saved successfully.';
        this.loadCustomerAddresses(this.registeredCustomerId!);
        this.currentStep = 3;
      },
      error: (err: any) => {
        this.errorMessage = err?.error?.message ?? 'Failed to save address.';
        this.submitting = false;
      }
    });
  }

  loadCustomerAddresses(customerId: number): void {
    this.service.getAddressesByCustomerId(customerId).subscribe({
      next: (list: CustomerAddress[]) => { this.addedAddresses = list ?? []; },
      error: () => {}
    });
  }

  submitNominee(): void {
    if (this.submitting) return;

    if (!this.registeredCustomerId) {
      this.errorMessage = 'No registered customer found. Please complete Step 1 first.';
      return;
    }

    if (!this.nomineeForm.nomineeName || !this.nomineeForm.relationship || !this.nomineeForm.dateOfBirth) {
      this.errorMessage = 'Please fill in mandatory nominee details.';
      return;
    }

    this.submitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.service.addNominee(this.registeredCustomerId, this.nomineeForm).subscribe({
      next: (response: any) => {
        this.submitting = false;
        this.successMessage = response?.message ?? 'Nominee added successfully.';
        this.loadCustomerNominees(this.registeredCustomerId!);
        this.currentStep = 4;
      },
      error: (err: any) => {
        this.errorMessage = err?.error?.message ?? 'Failed to add nominee.';
        this.submitting = false;
      }
    });
  }

  loadCustomerNominees(customerId: number): void {
    this.service.getNomineesByCustomerId(customerId).subscribe({
      next: (list: Nominee[]) => { this.addedNominees = list ?? []; },
      error: () => {}
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files?.[0];
    if (file) {
      this.documentForm.fileName = file.name;
      this.documentForm.filePath = `/uploads/documents/${file.name}`;
    }
  }

  submitDocument(): void {
    if (this.submitting) return;

    if (!this.registeredCustomerId) {
      this.errorMessage = 'No registered customer found. Please complete Step 1 first.';
      return;
    }

    if (!this.documentForm.fileName) {
      this.errorMessage = 'Please choose a document file to upload.';
      return;
    }

    this.submitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.service.uploadDocument(this.registeredCustomerId, this.documentForm).subscribe({
      next: (response: any) => {
        this.submitting = false;
        this.successMessage = response?.message ?? 'Document uploaded successfully.';
        this.loadCustomerDocuments(this.registeredCustomerId!);
      },
      error: (err: any) => {
        this.errorMessage = err?.error?.message ?? 'Failed to upload document.';
        this.submitting = false;
      }
    });
  }

  loadCustomerDocuments(customerId: number): void {
    this.service.getDocumentsByCustomerId(customerId).subscribe({
      next: (list: CustomerDocument[]) => { this.addedDocuments = list ?? []; },
      error: () => {}
    });
  }

  goToStep(step: number): void {
    if (step > 1 && !this.registeredCustomerId) {
      this.errorMessage = 'Please complete customer registration first.';
      return;
    }
    this.currentStep = step;
    this.errorMessage = '';
    this.successMessage = '';
  }

  startNewRegistration(): void {
    this.currentStep = 1;
    this.registeredCustomerId = null;
    this.registeredCustomerCode = '';
    this.addedAddresses = [];
    this.addedNominees = [];
    this.addedDocuments = [];
    this.errorMessage = '';
    this.successMessage = '';

    this.personalForm = {
      firstName: '',
      middleName: '',
      lastName: '',
      dateOfBirth: '',
      gender: '',
      maritalStatus: '',
      occupation: '',
      annualIncome: 0,
      mobileNumber: '',
      email: ''
    };

    this.addressForm = {
      addressType: '',
      addressLine1: '',
      addressLine2: '',
      city: '',
      district: '',
      state: '',
      country: '',
      pincode: ''
    };

    this.nomineeForm = {
      nomineeName: '',
      relationship: '',
      dateOfBirth: '',
      mobileNumber: '',
      sharePercentage: 0,
      guardianName: ''
    };

    this.documentForm = {
      documentType: '',
      documentNumber: '',
      fileName: '',
      filePath: ''
    };
  }

  proceedToKYC(): void {
    this.router.navigate(['/kyc-verification']);
  }

  completeKYC(): void {
    this.router.navigate(['/kyc-verification']);
  }

  saveChanges(): void {
    if (this.submitting) return;

    if (!this.personalForm.firstName || !this.personalForm.lastName || !this.personalForm.email || !this.personalForm.mobileNumber) {
      this.errorMessage = 'Please fill in all mandatory personal fields.';
      return;
    }

    this.submitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    const customerPayload: CreateCustomerRequest = { ...this.personalForm };

    this.service.createCustomer(customerPayload).subscribe({
      next: (response: any) => {
        this.submitting = false;
        this.successMessage = response?.message ?? 'Profile saved successfully.';

        this.service.getAllCustomers().subscribe({
          next: (list: Customer[]) => {
            const found = list.find(c => c.email.toLowerCase() === this.personalForm.email.toLowerCase());
            if (found) {
              this.registeredCustomerId = found.customerId;
              this.registeredCustomerCode = found.customerCode;
            }
          },
          error: () => {}
        });
      },
      error: (err: any) => {
        this.errorMessage = err?.error?.message ?? err?.error?.title ?? err?.message ?? 'Failed to save profile.';
        this.submitting = false;
        console.error('Save error:', err);
      }
    });
  }
}

