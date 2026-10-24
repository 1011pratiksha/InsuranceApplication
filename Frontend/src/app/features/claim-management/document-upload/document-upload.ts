import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  ClaimDocumentResponseDto,
  ClaimResponseDto,
  ClaimService,
  CreateClaimDocumentDto
} from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-document-upload',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './document-upload.html',
  styleUrl: './document-upload.scss'
})
export class DocumentUpload implements OnInit {
  private route = inject(ActivatedRoute);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  selectedClaimId: number = 1;
  documents: ClaimDocumentResponseDto[] = [];

  isUploading = false;
  isVerifying = false;
  successMessage = '';
  errorMessage = '';

  // Form fields strictly mapping to CreateClaimDocumentDto
  newDoc: CreateClaimDocumentDto = {
    documentName: '',
    filePath: ''
  };

  ngOnInit(): void {
    this.claimService.getAllClaims().subscribe({
      next: (claims) => {
        this.claims = claims;
        const paramClaimId = this.route.snapshot.queryParamMap.get('claimId');
        if (paramClaimId) {
          this.selectedClaimId = Number(paramClaimId);
        } else if (claims.length > 0) {
          this.selectedClaimId = claims[0].claimId;
        }
        this.loadDocuments();
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to fetch claims.';
      }
    });
  }

  loadDocuments(): void {
    if (!this.selectedClaimId) return;
    this.errorMessage = '';
    this.claimService.getDocuments(this.selectedClaimId).subscribe({
      next: (docs) => {
        this.documents = docs;
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to fetch documents for this claim.';
      }
    });
  }

  onClaimSelect(): void {
    this.loadDocuments();
    this.successMessage = '';
    this.errorMessage = '';
  }

  onFileSelected(event: any): void {
    const file = event.target.files?.[0];
    if (file) {
      this.newDoc.documentName = file.name;
      this.newDoc.filePath = `/uploads/claims/${this.selectedClaimId}/${file.name}`;
    }
  }

  uploadDocument(): void {
    if (!this.selectedClaimId || this.selectedClaimId <= 0) {
      this.errorMessage = 'Please select a valid Claim ID.';
      return;
    }

    if (!this.newDoc.documentName.trim()) {
      this.errorMessage = 'Document Name is required (MaxLength: 150).';
      return;
    }

    this.isUploading = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload: CreateClaimDocumentDto = {
      documentName: this.newDoc.documentName.trim(),
      filePath: this.newDoc.filePath?.trim() || null
    };

    this.claimService.addDocument(this.selectedClaimId, payload).subscribe({
      next: (res) => {
        this.isUploading = false;
        this.successMessage = `Document "${res.documentName}" uploaded successfully (ID: ${res.documentId})!`;
        this.newDoc = { documentName: '', filePath: '' };
        this.loadDocuments();
      },
      error: (err) => {
        this.isUploading = false;
        this.errorMessage = err.message || 'Failed to upload document.';
      }
    });
  }

  verifyDocument(doc: ClaimDocumentResponseDto): void {
    this.isVerifying = true;
    this.successMessage = '';
    this.errorMessage = '';

    // Backend endpoint: PUT /api/claims/{claimId}/documents/verify with List<int> documentIds
    this.claimService.verifyDocuments(this.selectedClaimId, [doc.documentId]).subscribe({
      next: () => {
        this.isVerifying = false;
        this.successMessage = `Document ID #${doc.documentId} ("${doc.documentName}") verified successfully!`;
        this.loadDocuments();
      },
      error: (err) => {
        this.isVerifying = false;
        this.errorMessage = err.message || 'Document verification failed.';
      }
    });
  }

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }
}
