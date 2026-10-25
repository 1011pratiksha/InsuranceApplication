import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  ClaimResponseDto,
  ClaimService,
  SettlementDTO,
  SettlementResponseDTO
} from '../../../services/claim-management/claim-service';

@Component({
  selector: 'app-claim-settlement',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './claim-settlement.html',
  styleUrl: './claim-settlement.scss'
})
export class ClaimSettlement implements OnInit {
  private route = inject(ActivatedRoute);
  claimService = inject(ClaimService);

  claims: ClaimResponseDto[] = [];
  selectedClaimId: number = 0;

  isDisbursing = false;
  isLoadingSettlement = false;
  existingSettlement: SettlementResponseDTO | null = null;
  successMessage = '';
  errorMessage = '';

  // Form fields strictly mapping to SettlementDTO
  // Only ONE field: paymentMode — NEFT | RTGS | IMPS | UPI | Cheque
  formData: SettlementDTO = {
    paymentMode: 'NEFT'
  };

  readonly paymentModes: SettlementDTO['paymentMode'][] = ['NEFT', 'RTGS', 'IMPS', 'UPI', 'Cheque'];

  ngOnInit(): void {
    this.claimService.getAllClaims().subscribe({
      next: (list) => {
        this.claims = list;
        const paramClaimId = this.route.snapshot.queryParamMap.get('claimId');
        if (paramClaimId) {
          this.selectedClaimId = Number(paramClaimId);
        } else {
          const approved = list.find(c => c.claimStatus === 'Approved');
          if (approved) {
            this.selectedClaimId = approved.claimId;
          } else if (list.length > 0) {
            this.selectedClaimId = list[0].claimId;
          }
        }
        this.loadExistingSettlement();
      },
      error: (err) => {
        this.errorMessage = err.message || 'Failed to load claims.';
      }
    });
  }

  onClaimSelect(): void {
    this.successMessage = '';
    this.errorMessage = '';
    this.existingSettlement = null;
    this.loadExistingSettlement();
  }

  loadExistingSettlement(): void {
    if (!this.selectedClaimId) return;
    this.isLoadingSettlement = true;
    this.claimService.getSettlement(this.selectedClaimId).subscribe({
      next: (res) => {
        this.isLoadingSettlement = false;
        this.existingSettlement = res;
      },
      error: () => {
        this.isLoadingSettlement = false;
        this.existingSettlement = null;
      }
    });
  }

  // POST /api/claims/{id}/settlement — SettlementDTO { paymentMode }
  processSettlement(): void {
    if (!this.selectedClaimId || this.selectedClaimId <= 0) {
      this.errorMessage = 'Please select a valid Claim ID.';
      return;
    }

    if (!this.formData.paymentMode) {
      this.errorMessage = 'Payment Mode is required: NEFT, RTGS, IMPS, UPI, or Cheque.';
      return;
    }

    this.isDisbursing = true;
    this.successMessage = '';
    this.errorMessage = '';

    const payload: SettlementDTO = {
      paymentMode: this.formData.paymentMode
    };

    this.claimService.processSettlement(this.selectedClaimId, payload).subscribe({
      next: (res) => {
        this.isDisbursing = false;
        this.existingSettlement = res;
        this.successMessage = `Settlement processed! Transaction Reference: ${res.transactionReference}. Status: ${res.settlementStatus}.`;
      },
      error: (err) => {
        this.isDisbursing = false;
        this.errorMessage = err.message || 'Settlement failed. Ensure claim is Approved and has an assessment with NetPayable > 0.';
      }
    });
  }

  getRoleLink(path: string): string {
    const role = this.claimService.currentRole.toLowerCase();
    return `/${role}/${path}`;
  }

  getSelectedClaim(): ClaimResponseDto | undefined {
    return this.claims.find(c => c.claimId === Number(this.selectedClaimId));
  }
}
