import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../services/authentication/auth-service';

@Component({
  selector: 'app-forgot-password',
  imports: [CommonModule,ReactiveFormsModule,RouterLink],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.scss'
})
export class ForgotPassword {
  message='';
  error='';
  loading=false;

  constructor(private service:AuthService,private cd:ChangeDetectorRef)
  {
  }

  forgotForm=new FormGroup({
    username:new FormControl('',Validators.required)
  });

  ForgotPassword(data:any)
  {
    if(this.forgotForm.invalid)
    {
      this.forgotForm.markAllAsTouched();
      return;
    }

    this.message='';
    this.error='';
    this.loading=true;

    this.service.ForgotPassword(data).subscribe({
      next:res=>{
        this.loading=false;
        this.message=res.msg;
        this.cd.detectChanges();
      },
      error:err=>{
        this.loading=false;
        this.error=err.status===429
          ? 'Too many attempts. Please wait one minute.'
          : err.error?.msg || 'Unable to send reset email. Please try again.';

        this.cd.detectChanges();
      }
    });
  }
}