
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { firstValueFrom } from 'rxjs';

interface CreatedKyc {
  kycId: number;
  customerId: number;
  kycNumber?: string;
  verificationStatus?: string;
}

interface KycDocumentUpload {
  documentType: string;
  documentNumber: string;
  file: File | null;
}

@Component({
  selector: 'app-kyc-submit',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './kyc-submit.component.html',
  styleUrl: './kyc-submit.component.css'
})
export class KycSubmitComponent {
  // TODO: Replace this with your actual KYC backend URL from launchSettings.json.
  private readonly apiUrl = 'https://localhost:7121/api/v1/kyc';

  // Temporary test ID. Replace with the logged-in customer's ID when
  // authentication/customer identity is integrated.
  customerId = 1;

  aadhaarNumber = '';
  panNumber = '';
  passportNumber = '';
  drivingLicenceNumber = '';
  consentToCkyc = false;

  // These files correspond to the upload controls on the form.
  aadhaarFile: File | null = null;
  panFile: File | null = null;
  addressProofFile: File | null = null;
  selfieFile: File | null = null;

  isSubmitting = false;
  message = '';
  messageType: 'success' | 'error' = 'error';
  createdKycId: number | null = null;

  constructor(private readonly http: HttpClient) {}

  // Read a file selected by the customer and check its size and format.
  onFileSelected(event: Event, field: string): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0] ?? null;

    if (!file) {
      this.setFile(field, null);
      return;
    }

    const allowedExtensions = ['.pdf', '.jpg', '.jpeg', '.png'];
    const extension = file.name
      .substring(file.name.lastIndexOf('.'))
      .toLowerCase();

    if (!allowedExtensions.includes(extension)) {
      this.showError('Only PDF, JPG, JPEG and PNG files are allowed.');
      input.value = '';
      this.setFile(field, null);
      return;
    }

    if (file.size === 0 || file.size > 5 * 1024 * 1024) {
      this.showError('Select a non-empty file no larger than 5 MB.');
      input.value = '';
      this.setFile(field, null);
      return;
    }

    this.setFile(field, file);
    this.message = '';
  }

  private setFile(field: string, file: File | null): void {
    switch (field) {
      case 'aadhaar':
        this.aadhaarFile = file;
        break;
      case 'pan':
        this.panFile = file;
        break;
      case 'address':
        this.addressProofFile = file;
        break;
      case 'selfie':
        this.selfieFile = file;
        break;
    }
  }

  // Create the KYC request first, then upload each selected document.
  async submitKyc(): Promise<void> {
    this.message = '';

    if (this.customerId < 1) {
      this.showError('A valid customer ID is required.');
      return;
    }

    if (!this.aadhaarNumber.trim() || !this.panNumber.trim()) {
      this.showError('Enter both Aadhaar and PAN numbers.');
      return;
    }

    if (!this.aadhaarFile || !this.panFile || !this.addressProofFile) {
      this.showError(
        'Please select Aadhaar, PAN and address-proof documents.'
      );
      return;
    }

    if (!this.consentToCkyc) {
      this.showError('Please provide consent to CKYC verification.');
      return;
    }

    const documents: KycDocumentUpload[] = [
      {
        documentType: 'Aadhaar',
        documentNumber: this.aadhaarNumber.trim(),
        file: this.aadhaarFile
      },
      {
        documentType: 'PAN',
        documentNumber: this.panNumber.trim(),
        file: this.panFile
      },
      {
        documentType: 'Address Proof',
        documentNumber: 'ADDRESS-PROOF',
        file: this.addressProofFile
      }
    ];

    if (this.passportNumber.trim()) {
      documents.push({
        documentType: 'Passport',
        documentNumber: this.passportNumber.trim(),
        file: null
      });
    }

    if (this.drivingLicenceNumber.trim()) {
      documents.push({
        documentType: 'Driving Licence',
        documentNumber: this.drivingLicenceNumber.trim(),
        file: null
      });
    }

    // Optional document numbers need matching uploaded files to be sent.
    // The backend requires a file for every document upload.
    if (this.passportNumber.trim() || this.drivingLicenceNumber.trim()) {
      this.showError(
        'Passport and driving licence numbers are optional, but this form ' +
        'needs their file-upload controls before those documents can be submitted.'
      );
      return;
    }

    this.isSubmitting = true;

    try {
      // API 1: create a KYC request for the customer.
      const created = await firstValueFrom(
        this.http.post<CreatedKyc>(this.apiUrl, {
          customerId: this.customerId
        })
      );

      this.createdKycId = created.kycId;

      // API 2: upload each required document as multipart/form-data.
      for (const document of documents) {
        if (!document.file) {
          continue;
        }

        const formData = new FormData();
        formData.append('KycId', String(created.kycId));
        formData.append('DocumentType', document.documentType);
        formData.append('DocumentNumber', document.documentNumber);
        formData.append('File', document.file, document.file.name);

        await firstValueFrom(
          this.http.post(`${this.apiUrl}/documents`, formData)
        );
      }

      this.messageType = 'success';
      this.message =
        `KYC request ${created.kycNumber ?? '#' + created.kycId} was created ` +
        'and the selected documents were uploaded. Your request is awaiting review.';
    } catch (error: unknown) {
      this.messageType = 'error';
      this.message = this.getErrorMessage(error);

      if (this.createdKycId !== null) {
        this.message +=
          ` Your KYC request ID is ${this.createdKycId}. ` +
          'Some documents may not have uploaded; check the request before retrying.';
      }
    } finally {
      this.isSubmitting = false;
    }
  }

  private getErrorMessage(error: unknown): string {
    if (error instanceof HttpErrorResponse) {
      const apiMessage = error.error?.message;

      if (typeof apiMessage === 'string' && apiMessage.trim()) {
        return apiMessage;
      }

      if (error.status === 0) {
        return 'Cannot reach the KYC API. Check that the backend is running, ' +
          'the API URL is correct, and CORS is configured.';
      }

      return `KYC request failed (HTTP ${error.status}).`;
    }

    return 'An unexpected error occurred while submitting KYC.';
  }

  private showError(message: string): void {
    this.messageType = 'error';
    this.message = message;
  }
}
