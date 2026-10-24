
import { Component } from '@angular/core';
import { Policyservices } from '../../../services/policyservices';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-adminservicerequest',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './adminservicerequest.html',
  styleUrl: './adminservicerequest.scss',
})
export class Adminservicerequest {

  constructor(private service: Policyservices) {
    this.allreqs();
  }

  loading = false;
  errorMessage = '';
  totalreqs: any[] = [];

  allreqs(): void {
    this.loading = true;
    this.errorMessage = '';

    this.service.allreq().subscribe({
      next: (res: any) => {
        this.totalreqs = res?.data ?? res?.Data ?? [];
        this.loading = false;
      },
      error: (err: any) => {
        console.error('Failed to load service requests:', err);

        this.errorMessage =
          'Unable to load service requests. Please check the backend API.';

        this.loading = false;
      }
    });
  }

  updateStatus(data: any, status: string): void {
    const confirmed = confirm(
      `Are you sure you want to ${status.toLowerCase()} this request?`
    );

    if (!confirmed) {
      return;
    }

    this.service.updatereq(data.request_id, status).subscribe({
      next: () => {
        alert(`Request ${status.toLowerCase()} successfully.`);
        this.allreqs();
      },
      error: (err: any) => {
        console.error('Status update failed:', err);

        alert(
          'Failed to update status. ' +
          (err.error?.message || err.message || 'Please try again.')
        );
      }
    });
  }
}
