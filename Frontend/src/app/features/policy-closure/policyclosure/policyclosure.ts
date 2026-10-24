import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Policyservices } from '../../../services/policyservices';

@Component({
  selector: 'app-policyclosure',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './policyclosure.html',
  styleUrl: './policyclosure.scss',
})
export class Policyclosure {
message = '';

  closureForm = new FormGroup({
    policy_id: new FormControl<number | null>(null, Validators.required),
    cancellation_reason: new FormControl('', Validators.required)
  });

  constructor(private service: Policyservices) {}

  submitClosure() {
    if (this.closureForm.invalid) {
      this.message = 'Please enter the policy ID and cancellation reason.';
      return;
    }
    const policyId = this.closureForm.controls.policy_id.value!;
    const reason = this.closureForm.controls.cancellation_reason.value!;

     this.service.cancelPolicy(policyId, reason).subscribe({
      next: (res: any) => {
        this.message = res.message || 'Closure request submitted successfully.';
        this.closureForm.reset();
      },
      error: (err: any) => {
        this.message =
          err?.error?.message || 'Unable to submit closure request.';
      }
    });
  }
  

  
}
