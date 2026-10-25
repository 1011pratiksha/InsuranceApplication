
import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { PolicyIssuanceService } from '../../../../services/policy-issurance/policy-issuance.service';

@Component({
  selector: 'app-policy-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './policy-details.component.html',
  styleUrl: './policy-details.component.css'
})
export class PolicyDetailsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private service = inject(PolicyIssuanceService);

  policy: any = null;
  loading = false;
  downloading = false;
  errorMessage = '';

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (!Number.isInteger(id) || id <= 0) {
      this.errorMessage = 'Invalid policy ID.';
      return;
    }

    this.loadPolicy(id);
  }

  loadPolicy(id: number): void {
    this.loading = true;

    this.service.getPolicyById(id).subscribe({
      next: (response: any) => {
        this.policy = response?.data ?? response;
        this.loading = false;
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message ?? 'Unable to load policy details.';
        this.loading = false;
      }
    });
  }

  downloadPdf(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (this.downloading || !id) {
      return;
    }

    this.downloading = true;
    this.errorMessage = '';

    this.service.downloadDocument(id).subscribe({
      next: (blob: Blob) => {
        if (!blob || blob.size === 0) {
          this.errorMessage = 'The policy document is empty.';
          this.downloading = false;
          return;
        }

        const url = URL.createObjectURL(blob);
        const link = document.createElement('a');

        link.href = url;
        link.download =
          `${this.policy?.policyNumber ?? 'policy'}.pdf`;

        link.click();
        URL.revokeObjectURL(url);

        this.downloading = false;
      },
      error: (err: any) => {
        this.errorMessage =
          err?.error?.message ?? 'Unable to download policy PDF.';
        this.downloading = false;
      }
    });
  }

  backToPolicies(): void {
    this.router.navigate(['/customer/my-policies']);
  }

  payPremium(): void {
    this.router.navigate(['/premium-payment']);
  }

  requestChange(): void {
    this.router.navigate(['/policy-servicing']);
  }

  raiseClaim(): void {
    this.router.navigate(['/claim-settlement']);
  }
}
