import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
    providedIn:'root'
})
export class AuthService {
    http=inject(HttpClient);
    loginData:any=null;

    url="/api/v1/auth/login";
    Login(data:any)
    {
        return this.http.post<any>(this.url,data,{withCredentials:true});
    }

    url2="/api/v1/auth/logout";
    Logout()
    {
        return this.http.post<any>(this.url2,{},{withCredentials:true});
    }

    url3="/api/v1/auth/refresh-token";
    RefreshToken()
    {
        return this.http.post<any>(this.url3,{},{withCredentials:true});
    }

    url4="/api/v1/auth/change-password";
    ChangePassword(data:any)
    {
        return this.http.post<any>(this.url4,data,{withCredentials:true});
    }

    url5="/api/v1/auth/forgot-password";
    ForgotPassword(data:any)
    {
        return this.http.post<any>(this.url5,data,{withCredentials:true});
    }

    url6="/api/v1/auth/reset-password";
    ResetPassword(data:any)
    {
        return this.http.post<any>(this.url6,data,{withCredentials:true});
    }

    url7="/api/v1/auth/2fa/setup";
    SetupTwoFactor()
    {
        return this.http.post<any>(this.url7,{},{withCredentials:true});
    }

    url8="/api/v1/auth/2fa/enable";
    EnableTwoFactor(data:any)
    {
        return this.http.post<any>(this.url8,data,{withCredentials:true});
    }
}