import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/authentication/auth-service';

@Component({
  selector: 'app-login',
  imports: [CommonModule,ReactiveFormsModule,RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  message='';
  loading=false;

  constructor(private service:AuthService,private router:Router,private cd:ChangeDetectorRef)
  {
  }

  loginForm=new FormGroup({
    username:new FormControl('',Validators.required),
    password:new FormControl('',Validators.required)
  });

  Login(data:any)
  {
    if(this.loginForm.invalid)
    {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.message='';
    this.loading=true;
    this.service.loginData=null;

    this.service.Login(data).subscribe({
      next:res=>{
        this.loading=false;
        this.loginForm.reset();
        this.router.navigate(['/two-factor']);
      },
      error:err=>{
        this.loading=false;

        if(err.error?.requiresTwoFactor)
        {
          this.service.loginData=data;
          this.loginForm.reset();
          this.router.navigate(['/two-factor']);
          return;
        }

        this.message=err.status===429
          ? 'Too many attempts. Please wait one minute.'
          : err.error?.message || err.error?.msg || 'Unable to login. Please try again.';

        this.cd.detectChanges();
      }
    });
  }
}