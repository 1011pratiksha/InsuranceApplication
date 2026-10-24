
import { Routes } from '@angular/router';
import { PremiumSchedule } from './features/premium-schedule/premium-schedule';
import { PayPremium } from './features/premium-schedule/pay-premium/pay-premium';
import { PaymentHistory } from './features/premium-schedule/payment-history/payment-history';
import { Receipts } from './features/premium-schedule/receipts/receipts';
import { RefundStatus } from './features/premium-schedule/refund-status/refund-status';
import { AdminPremiumSchedules } from './features/premium-schedule/admin-premium-schedules/admin-premium-schedules';
import { AdminPaymentVerification } from './features/premium-schedule/admin-payment-verification/admin-payment-verification';
import { AdminLateFeeConfig } from './features/premium-schedule/admin-late-fee-config/admin-late-fee-config';
import { AdminReminders } from './features/premium-schedule/admin-reminders/admin-reminders';
import { AdminRefunds } from './features/premium-schedule/admin-refunds/admin-refunds';

export const routes: Routes = [
  { path: '', redirectTo: 'premium-schedule', pathMatch: 'full' },

  { path: 'premium-schedule', component: PremiumSchedule },
  { path: 'pay-premium', component: PayPremium },
{ path: 'payment-history', component: PaymentHistory },
{ path: 'receipts', component: Receipts },
{ path: 'refund-status', component: RefundStatus },
{ path: 'admin/premium-schedules', component: AdminPremiumSchedules },
{ path: 'admin/payment-verification', component: AdminPaymentVerification },
{ path: 'admin/late-fee-config', component: AdminLateFeeConfig },
{ path: 'admin/reminders', component: AdminReminders },
{ path: 'admin/refunds', component: AdminRefunds },
  { path: '**', redirectTo: 'premium-schedule' }
];
