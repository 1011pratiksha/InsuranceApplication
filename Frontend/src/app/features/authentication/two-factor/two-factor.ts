import { ChangeDetectorRef, Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/authentication/auth-service';

@Component({
  selector: 'app-two-factor',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './two-factor.html',
  styleUrl: './two-factor.scss'
})
export class TwoFactor implements OnDestroy {
  message='';
  qrCode='';
  manualKey='';
  setupMode=false;
  loading=false;

  constructor(private service:AuthService,private router:Router,private cd:ChangeDetectorRef)
  {
    if(!this.service.loginData)
    {
      this.SetupTwoFactor();
    }
  }

  codeForm=new FormGroup({
    code:new FormControl('',[
      Validators.required,
      Validators.pattern(/^[0-9]{6}$/)
    ])
  });

  SetupTwoFactor()
  {
    this.loading=true;

    this.service.SetupTwoFactor().subscribe({
      next:res=>{
        this.setupMode=true;
        this.qrCode=res.qrCode;
        this.manualKey=res.manualEntryKey;
        this.loading=false;
        this.cd.detectChanges();
      },
      error:err=>{
        this.loading=false;

        if(err.status===401)
        {
          this.router.navigate(['/login']);
          return;
        }

        this.message=err.error?.msg || 'Unable to set up two-factor authentication.';
        this.cd.detectChanges();
      }
    });
  }

  VerifyCode(data:any)
  {
    if(this.codeForm.invalid)
    {
      this.codeForm.markAllAsTouched();
      return;
    }

    this.message='';

    if(this.setupMode)
    {
      this.loading=true;

      this.service.EnableTwoFactor(data).subscribe({
        next:res=>{
          this.loading=false;
          this.codeForm.reset();
          this.router.navigate(['/login-success']);
        },
        error:err=>{
          this.loading=false;
          this.message=err.status===429
            ? 'Too many attempts. Please wait one minute.'
            : err.error?.msg || 'Unable to enable 2FA. Enter a fresh code.';

          this.cd.detectChanges();
        }
      });
    }
    else
    {
      if(!this.service.loginData)
      {
        this.router.navigate(['/login']);
        return;
      }

      var loginData={
        username:this.service.loginData.username,
        password:this.service.loginData.password,
        code:data.code
      };

      this.loading=true;

      this.service.Login(loginData).subscribe({
        next:res=>{
          this.loading=false;
          this.service.loginData=null;
          this.codeForm.reset();
          this.router.navigate(['/login-success']);
        },
        error:err=>{
          this.loading=false;
          this.message=err.status===429
            ? 'Too many attempts. Please wait one minute.'
            : err.error?.message || err.error?.msg || 'Verification failed. Please try again.';

          this.cd.detectChanges();
        }
      });
    }
  }

  BackToLogin()
  {
    this.service.loginData=null;
    this.router.navigate(['/login']);
  }

  ngOnDestroy()
  {
    this.service.loginData=null;
  }
}