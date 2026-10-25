import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-kyc-admin-dashboard',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './kyc-dashboard.component.html',
  styleUrl: './kyc-dashboard.component.css'
})
export class KycAdminDashboardComponent {
}