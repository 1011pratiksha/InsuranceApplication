import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Policyservices } from '../../../services/policyservices';

@Component({
  selector: 'app-policyserviceingss',
  imports: [ReactiveFormsModule],
  templateUrl: './policyserviceingss.html',
  styleUrl: './policyserviceingss.scss',
})
export class Policyserviceingss {

  constructor(private service:Policyservices){

  }

    requestForm = new FormGroup({
      policy_id: new FormControl('', Validators.required),
    customer_id: new FormControl(1, Validators.required),
    request_type: new FormControl('', Validators.required),
    details: new FormControl('', Validators.required),
    remarks: new FormControl('')
    });

    submitreq()
    {
      this.service.createreq(this.requestForm.value).subscribe({
        next:res=>{
          alert('Service request submitted successfully!');
          this.requestForm.reset({
          policy_id: '',
          customer_id: 1,
          request_type: '',
          details: '',
          remarks: ''
        });
        },
        error:err=>{
          console.log('Status:', err.status);
  console.log('Error details:', err.error);
  console.log('Message:', err.message);
  alert(
    'Status: ' + err.status +
    '\nError: ' + (err.error?.message || err.message)
  );
        
        }
      })
    }
}
