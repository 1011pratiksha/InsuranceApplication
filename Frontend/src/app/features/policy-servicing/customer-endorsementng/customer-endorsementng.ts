import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Policyservices } from '../../../services/policyservices';

@Component({
  selector: 'app-customer-endorsementng',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customer-endorsementng.html',
  styleUrl: './customer-endorsementng.scss',
})
export class CustomerEndorsementng {
  message = '';
   loading = false;
  isError = false;

  endorsementForm = new FormGroup({
    request_id: new FormControl<number | null>(null, Validators.required),
    endorsement_type: new FormControl('', Validators.required),
    previous_value: new FormControl(''),
    updated_value: new FormControl('', Validators.required)
  });

  constructor(private service: Policyservices ) {}

  submitEndorsement() {
    this.message = '';
    this.isError = false;
    if (this.endorsementForm.invalid) {
      this.message = 'Please fill all required fields.';
      this.isError = true;
      return;
    }

    this.service.createEndorsement(this.endorsementForm.value).subscribe({
      next: (res: any) => {
        this.message = res.message || 'Endorsement submitted successfully.';
        this.endorsementForm.reset();
      },
      error: () => {
        this.message = 'Error submitting endorsement.';
      }
    });
  }
}
