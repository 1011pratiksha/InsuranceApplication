import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ClaimResponseDto, ClaimService, UpdateClaimDto } from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claim-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './claim-list.html',
  styleUrl: './claim-list.scss'
})
export class ClaimList implements OnInit {
  private router = inject(Router);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  filteredClaims: ClaimResponseDto[] = [];
  isLoading = true;
  errorMessage = '';
  successMessage = '';

  searchTerm = '';
  selectedStatus = 'All';
  selectedType = 'All';

  statuses = ['All', 'Registered', 'Approved', 'Rejected', 'Settled'];
  types = ['All', 'Motor', 'Health', 'Property', 'Life', 'Travel'];

  // Update Modal State (Strictly UpdateClaimDto fields: claimType, incidentDate, estimatedLoss)
  editingClaim: ClaimResponseDto | null = null;
  updateForm: UpdateClaimDto = {
    claimType: 'Motor',
    incidentDate: '',
    estimatedLoss: 0
  };
  isUpdating = false;

  ngOnInit(): void {
    this.loadClaims();
  }

  loadClaims(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.claimService.getAllClaims().subscribe({
      next: (data) => {
        this.claims = data;
        this.applyFilter();
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.message || 'Failed to fetch claims from backend.';
      }
    });
  }

  applyFilter(): void {
    this.filteredClaims = this.claims.filter(claim => {
      const term = this.searchTerm.toLowerCase();
      const matchesSearch = !this.searchTerm ||
        String(claim.claimId).includes(term) ||
        (claim.claimNumber || '').toLowerCase().includes(term) ||
        String(claim.policyId).includes(term) ||
        String(claim.customerId).includes(term);

      const matchesStatus = this.selectedStatus === 'All' || claim.claimStatus === this.selectedStatus;
      const matchesType = this.selectedType === 'All' || claim.claimType === this.selectedType;

      return matchesSearch && matchesStatus && matchesType;
    });
  }

  onSearchChange(): void {
    this.applyFilter();
  }

  setStatusFilter(status: string): void {
    this.selectedStatus = status;
    this.applyFilter();
  }

  viewDetails(claimId: number): void {
    const role = this.claimService.currentRole.toLowerCase();
    this.router.navigate([`/${role}/claims/details`, claimId]);
  }

  trackClaim(claim: ClaimResponseDto): void {
    const role = this.claimService.currentRole.toLowerCase();
    this.router.navigate([`/${role}/claims/tracking`], { queryParams: { q: claim.claimNumber } });
  }

  openEditModal(claim: ClaimResponseDto): void {
    if (claim.claimStatus !== 'Registered') {
      alert('Business Rule: Only claims in "Registered" status can be updated.');
      return;
    }
    this.editingClaim = claim;
    this.updateForm = {
      claimType: claim.claimType,
      incidentDate: claim.incidentDate ? claim.incidentDate.split('T')[0] : '',
      estimatedLoss: claim.estimatedLoss
    };
  }

  closeEditModal(): void {
    this.editingClaim = null;
  }

  saveClaimUpdate(): void {
    if (!this.editingClaim) return;
    this.isUpdating = true;
    this.errorMessage = '';

    const payload: UpdateClaimDto = {
      claimType: this.updateForm.claimType,
      incidentDate: this.updateForm.incidentDate,
      estimatedLoss: Number(this.updateForm.estimatedLoss)
    };

    this.claimService.updateClaim(this.editingClaim.claimId, payload).subscribe({
      next: (updated) => {
        this.isUpdating = false;
        this.successMessage = `Claim #${updated.claimNumber} updated successfully.`;
        this.closeEditModal();
        this.loadClaims();
      },
      error: (err) => {
        this.isUpdating = false;
        this.errorMessage = err.message || 'Failed to update claim.';
      }
    });
  }

  deleteClaim(claim: ClaimResponseDto, event: Event): void {
    event.stopPropagation();
    if (claim.claimStatus !== 'Registered') {
      alert('Business Rule: Only claims in "Registered" status can be deleted.');
      return;
    }

    if (confirm(`Are you sure you want to delete Registered Claim #${claim.claimNumber} (ID: ${claim.claimId})?`)) {
      this.claimService.deleteClaim(claim.claimId).subscribe({
        next: (msg) => {
          this.successMessage = msg || 'Claim deleted successfully.';
          this.loadClaims();
        },
        error: (err) => {
          this.errorMessage = err.message || 'Failed to delete claim.';
        }
      });
    }
  }

  getStatusBadgeClass(status?: string): string {
    switch (status) {
      case 'Settled': return 'badge-success';
      case 'Approved': return 'badge-purple';
      case 'Rejected': return 'badge-danger';
      case 'Registered': return 'badge-info';
      default: return 'badge-neutral';
    }
  }

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }
}
