import { Routes } from '@angular/router';
import { Login } from './features/authentication/login/login';
import { TwoFactor } from './features/authentication/two-factor/two-factor';
import { ForgotPassword } from './features/authentication/forgot-password/forgot-password';
import { ResetPassword } from './features/authentication/reset-password/reset-password';
import { LoginSuccess } from './features/authentication/login-success/login-success';

export const routes: Routes = [
    {path:'',redirectTo:'login',pathMatch:'full'},
    {path:'login',component:Login},
    {path:'two-factor',component:TwoFactor},
    {path:'forgot-password',component:ForgotPassword},
    {path:'reset-password',component:ResetPassword},
    {path:'login-success',component:LoginSuccess},
    {path:'**',redirectTo:'login'}
];