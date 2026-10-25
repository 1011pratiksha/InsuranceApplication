import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ClaimResponseDto, ClaimService, CreateClaimDto } from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claim-registration',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './claim-registration.html',
  styleUrl: './claim-registration.scss'
})
export class ClaimRegistration {
  private router = inject(Router);
  claimService = inject(ClaimService);

  isSubmitting = false;
  successClaim: ClaimResponseDto | null = null;
  errorMessage = '';

  // Form fields strictly mapping to CreateClaimDto
  formData: CreateClaimDto = {
    policyId: 101,
    customerId: 501,
    claimType: 'Motor',
    incidentDate: new Date().toISOString().split('T')[0],
    estimatedLoss: 5000
  };

  onSubmit(): void {
    if (!this.formData.policyId || this.formData.policyId <= 0) {
      this.errorMessage = 'Policy ID must be a positive integer.';
      return;
    }

    if (!this.formData.customerId || this.formData.customerId <= 0) {
      this.errorMessage = 'Customer ID must be a positive integer.';
      return;
    }

    if (!this.formData.claimType || !this.formData.claimType.trim()) {
      this.errorMessage = 'Claim Type is required.';
      return;
    }

    if (!this.formData.incidentDate) {
      this.errorMessage = 'Incident Date is required.';
      return;
    }

    const today = new Date().toISOString().split('T')[0];
    if (this.formData.incidentDate > today) {
      this.errorMessage = 'Incident Date must be in the past or today (IncidentDate <= DateTime.UtcNow.Date).';
      return;
    }

    if (!this.formData.estimatedLoss || this.formData.estimatedLoss <= 0) {
      this.errorMessage = 'Estimated Loss must be greater than 0.';
      return;
    }

    this.isSubmitting = true;
    this.errorMessage = '';

    const payload: CreateClaimDto = {
      policyId: Number(this.formData.policyId),
      customerId: Number(this.formData.customerId),
      claimType: this.formData.claimType.trim(),
      incidentDate: this.formData.incidentDate,
      estimatedLoss: Number(this.formData.estimatedLoss)
    };

    this.claimService.createClaim(payload).subscribe({
      next: (createdClaim) => {
        this.isSubmitting = false;
        this.successClaim = createdClaim;
      },
      error: (err) => {
        this.isSubmitting = false;
        this.errorMessage = err.message || 'Failed to submit claim. Verify backend connection & CORS.';
      }
    });
  }

  resetForm(): void {
    this.successClaim = null;
    this.formData = {
      policyId: 101,
      customerId: 501,
      claimType: 'Motor',
      incidentDate: new Date().toISOString().split('T')[0],
      estimatedLoss: 5000
    };
    this.errorMessage = '';
  }

  goToUploadDocs(): void {
    if (this.successClaim) {
      this.router.navigate(['/customer/claims/documents'], { queryParams: { claimId: this.successClaim.claimId } });
    }
  }

  goToList(): void {
    const role = this.claimService.currentRole.toLowerCase();
    this.router.navigate([`/${role}/claims/list`]);
  }
}
