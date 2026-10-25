import { ChangeDetectorRef, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../services/authentication/auth-service';

@Component({
  selector: 'app-login-success',
  imports: [CommonModule,RouterLink],
  templateUrl: './login-success.html',
  styleUrl: './login-success.scss'
})
export class LoginSuccess {
  message='';
  loading=false;

  constructor(private service:AuthService,private router:Router,private cd:ChangeDetectorRef)
  {
  }

  Logout()
  {
    this.message='';
    this.loading=true;

    this.service.Logout().subscribe({
      next:res=>{
        this.loading=false;
        this.service.loginData=null;
        this.router.navigate(['/login']);
      },
      error:err=>{
        this.loading=false;
        this.message=err.status===401
          ? 'Your session has expired. Please log in again.'
          : err.error?.msg || 'Unable to logout. Please try again.';

        this.cd.detectChanges();
      }
    });
  }
}