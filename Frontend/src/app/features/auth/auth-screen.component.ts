import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { UserRole } from '../../core/models/sidebar-menu.model';

@Component({
  selector: 'app-auth-screen',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <div class="auth-container">
      <div class="auth-card">
        <div class="brand-badge">
          <span class="material-symbols-rounded">shield</span>
          <span>InsurancePAS</span>
        </div>

        <h2 class="auth-title">Welcome Back</h2>
        <p class="auth-subtitle">Select your role to access the portal or test role navigation.</p>

        <div class="role-grid">
          <div
            class="role-card"
            [class.selected]="selectedRole === 'customer'"
            (click)="selectedRole = 'customer'">
            <div class="role-icon customer">
              <span class="material-symbols-rounded">person</span>
            </div>
            <div class="role-title">Customer</div>
            <div class="role-desc">View policies, pay premiums & file claims</div>
          </div>

          <div
            class="role-card"
            [class.selected]="selectedRole === 'agent'"
            (click)="selectedRole = 'agent'">
            <div class="role-icon agent">
              <span class="material-symbols-rounded">support_agent</span>
            </div>
            <div class="role-title">Agent</div>
            <div class="role-desc">Manage clients, generate quotes & commissions</div>
          </div>

          <div
            class="role-card"
            [class.selected]="selectedRole === 'admin'"
            (click)="selectedRole = 'admin'">
            <div class="role-icon admin">
              <span class="material-symbols-rounded">admin_panel_settings</span>
            </div>
            <div class="role-title">Admin</div>
            <div class="role-desc">Underwriting, issuance, KYC & system masters</div>
          </div>
        </div>

        <button class="submit-btn" (click)="onContinue()">
          <span>Enter as {{ getRoleName() }}</span>
          <span class="material-symbols-rounded">arrow_forward</span>
        </button>

        <div class="back-link">
          <a routerLink="/customer/dashboard">Skip to Application</a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .auth-container {
      min-height: 100vh;
      display: flex;
      align-items: center;
      justify-content: center;
      background: radial-gradient(circle at 50% 20%, #1e1b4b 0%, #0b0f19 80%);
      padding: 24px;
      font-family: 'Inter', sans-serif;
    }
    .auth-card {
      width: 100%;
      max-width: 480px;
      background: rgba(15, 23, 42, 0.85);
      border: 1px solid rgba(148, 163, 184, 0.12);
      border-radius: 20px;
      padding: 40px;
      box-shadow: 0 20px 40px rgba(0, 0, 0, 0.4);
      backdrop-filter: blur(16px);
      text-align: center;
    }
    .brand-badge {
      display: inline-flex;
      align-items: center;
      gap: 8px;
      background: rgba(99, 102, 241, 0.15);
      color: #818cf8;
      border: 1px solid rgba(99, 102, 241, 0.3);
      padding: 6px 14px;
      border-radius: 20px;
      font-size: 13px;
      font-weight: 700;
      letter-spacing: 0.5px;
      margin-bottom: 20px;
    }
    .auth-title {
      color: #f8fafc;
      font-size: 24px;
      font-weight: 700;
      margin: 0 0 8px;
    }
    .auth-subtitle {
      color: #94a3b8;
      font-size: 13.5px;
      margin: 0 0 28px;
    }
    .role-grid {
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      gap: 12px;
      margin-bottom: 28px;
    }
    .role-card {
      background: rgba(30, 41, 59, 0.5);
      border: 2px solid rgba(148, 163, 184, 0.1);
      border-radius: 12px;
      padding: 16px 10px;
      cursor: pointer;
      transition: all 0.2s;
      display: flex;
      flex-direction: column;
      align-items: center;
      text-align: center;
    }
    .role-card:hover {
      border-color: rgba(99, 102, 241, 0.4);
      transform: translateY(-2px);
    }
    .role-card.selected {
      border-color: #6366f1;
      background: rgba(99, 102, 241, 0.15);
      box-shadow: 0 0 16px rgba(99, 102, 241, 0.25);
    }
    .role-icon {
      width: 40px;
      height: 40px;
      border-radius: 10px;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-bottom: 10px;
    }
    .role-icon.customer { background: rgba(99, 102, 241, 0.2); color: #818cf8; }
    .role-icon.agent { background: rgba(16, 185, 129, 0.2); color: #34d399; }
    .role-icon.admin { background: rgba(245, 158, 11, 0.2); color: #fbbf24; }
    .role-title {
      color: #f1f5f9;
      font-size: 13px;
      font-weight: 600;
      margin-bottom: 4px;
    }
    .role-desc {
      color: #64748b;
      font-size: 10px;
      line-height: 1.3;
    }
    .submit-btn {
      width: 100%;
      padding: 12px;
      border-radius: 10px;
      background: linear-gradient(135deg, #6366f1 0%, #4f46e5 100%);
      color: white;
      border: none;
      font-size: 14px;
      font-weight: 600;
      cursor: pointer;
      display: flex;
      align-items: center;
      justify-content: center;
      gap: 8px;
      transition: all 0.2s;
      box-shadow: 0 4px 14px rgba(99, 102, 241, 0.4);
    }
    .submit-btn:hover {
      transform: translateY(-1px);
      box-shadow: 0 6px 20px rgba(99, 102, 241, 0.5);
    }
    .back-link {
      margin-top: 18px;
    }
    .back-link a {
      color: #818cf8;
      text-decoration: none;
      font-size: 12.5px;
    }
  `]
})
export class AuthScreenComponent {
  private auth = inject(AuthService);
  private router = inject(Router);

  selectedRole: UserRole = 'customer';

  getRoleName(): string {
    switch (this.selectedRole) {
      case 'agent': return 'Agent';
      case 'admin': return 'Admin';
      default: return 'Customer';
    }
  }

  onContinue(): void {
    this.auth.setRole(this.selectedRole);
    this.router.navigate([`/${this.selectedRole}/dashboard`]);
  }
}
