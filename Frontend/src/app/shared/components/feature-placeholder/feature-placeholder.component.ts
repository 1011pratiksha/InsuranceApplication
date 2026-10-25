import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyPoliciesComponent } from '../../../features/policy-issuance/customer/my-policies/my-policies.component';

@Component({
  selector: 'app-feature-placeholder',
  standalone: true,
  imports: [CommonModule, MyPoliciesComponent],
  templateUrl: './feature-placeholder.component.html',
  styleUrl: './feature-placeholder.component.scss'
})
export class FeaturePlaceholderComponent {}
