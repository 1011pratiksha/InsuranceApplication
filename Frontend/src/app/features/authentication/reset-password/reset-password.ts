import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/authentication/auth-service';

@Component({
  selector: 'app-reset-password',
  imports: [CommonModule,ReactiveFormsModule,RouterLink],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.scss'
})
export class ResetPassword {
  message='';
  loading=false;

  constructor(private service:AuthService,private router:Router,private cd:ChangeDetectorRef)
  {
  }

  resetForm=new FormGroup({
    username:new FormControl('',Validators.required),
    token:new FormControl('',Validators.required),
    newPassword:new FormControl('',Validators.required),
    confirmPassword:new FormControl('',Validators.required)
  });

  ResetPassword(data:any)
  {
    this.message='';

    if(this.resetForm.invalid)
    {
      this.resetForm.markAllAsTouched();
      this.message='Please fill in all fields.';
      return;
    }

    if(data.newPassword!==data.confirmPassword)
    {
      this.message='Passwords do not match.';
      return;
    }

    this.loading=true;

    this.service.ResetPassword(data).subscribe({
      next:res=>{
        this.loading=false;
        this.service.loginData=null;
        this.resetForm.reset();
        alert(res.msg);
        this.router.navigate(['/login']);
      },
      error:err=>{
        this.loading=false;
        this.message=err.status===429
          ? 'Too many attempts. Please wait one minute.'
          : err.error?.msg || 'Unable to reset password. Check your details and token.';

        this.cd.detectChanges();
      }
    });
  }
}