
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Policyservices } from '../../../services/policyservices';

@Component({
  selector: 'app-customer-endorsementng',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customer-endorsementng.html',
  styleUrl: './customer-endorsementng.scss'
})
export class CustomerEndorsement implements OnInit {

  requests: any[] = [];
  request: any = null;
  message = '';
  loading = false;

  form = new FormGroup({
    request_id: new FormControl('', Validators.required),
    updated_value: new FormControl('', Validators.required)
  });

  constructor(private service: Policyservices) {}

  ngOnInit(): void {
    this.service.allreq().subscribe({
      next: (res: any) => {
        this.requests = res?.data ?? [];
      },
      error: () => {
        this.message = 'Unable to load service requests.';
      }
    });
  }

  searchRequest(): void {
    const id = Number(this.form.controls.request_id.value);

    this.request = null;
    this.message = '';

    if (!id) {
      return;
    }

    this.service.getreqbyid(id).subscribe({
      next: (res: any) => {
        this.request = res?.data ?? null;

        if (!this.request) {
          this.message = 'Service request not found.';
        }
      },
      error: () => {
        this.message = 'Unable to find this service request.';
      }
    });
  }

  submit(): void {
    if (this.form.invalid || !this.request || this.loading) {
      this.message = 'Select a request and enter the updated value.';
      return;
    }

    const data = {
      request_id: this.request.request_id,
      endorsement_type: this.request.request_type,
      previous_value: this.request.details || null,
      updated_value: this.form.controls.updated_value.value?.trim()
    };

    if (!data.updated_value) {
      this.message = 'Please enter the updated value.';
      return;
    }

    this.loading = true;
    this.message = '';

    this.service.createEndorsement(data).subscribe({
      next: (res: any) => {
        if (res?.success === false) {
          this.message = res.message || 'Unable to submit endorsement.';
          this.loading = false;
          return;
        }

        this.message = res?.message || 'Endorsement submitted successfully.';
        this.form.controls.updated_value.reset();
        this.loading = false;
      },
      error: (err: any) => {
  console.log('Endorsement error:', err);
  console.log('Backend response:', err.error);

  this.message =
    err?.error?.message ||
    err?.error?.title ||
    `Submission failed. HTTP Status: ${err.status}`;

  this.loading = false;
}
    });
  }
}
