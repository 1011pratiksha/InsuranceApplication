import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CommissionRule } from '../../../services/AgentService/commission-rule';

@Component({
  selector: 'app-commission-rules',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './commission-rules.html',
  styleUrl: './commission-rules.scss'
})
export class CommissionRules implements OnInit {

  commissionRules: any[] = [];
  products: any[] = [];

  showForm = false;
  isEditMode = false;
  selectedRuleId = 0;
  isLoading = false;

  commissionRule = {
    productId: 0,
    commissionPercentage: 0,
    effectiveFrom: '',
    effectiveTo: '',
    isActive: true
  };

  constructor(private commissionRuleService: CommissionRule) {}

  ngOnInit(): void {
    this.loadCommissionRules();
    this.loadProducts();
  }

  loadCommissionRules(): void {
    this.isLoading = true;

    this.commissionRuleService.getAllCommissionRules().subscribe({
      next: (data) => {
        this.commissionRules = data;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading commission rules:', error);
        alert('Unable to load commission rules.');
        this.isLoading = false;
      }
    });
  }

  toggleForm(): void {
    if (this.showForm) {
      this.showForm = false;
      this.resetForm();
    } else {
      this.showForm = true;
    }
  }

  loadProducts(): void {
  this.commissionRuleService.getAllProducts().subscribe({
    next: (data) => {
      this.products = data;
    },
    error: (error) => {
      console.error('Error loading products:', error);
      alert('Unable to load products.');
    }
  });
}


getProductName(productId: number): string {
  const product = this.products.find(
    p => p.product_id === productId
  );

  return product
    ? `${product.product_name} (${product.product_code})`
    : `Product ID: ${productId}`;
}


  saveCommissionRule(): void {
    if (
      this.commissionRule.productId <= 0 ||
      this.commissionRule.commissionPercentage <= 0 ||
      this.commissionRule.commissionPercentage > 100 ||
      !this.commissionRule.effectiveFrom
    ) {
      alert('Please enter valid commission rule details.');
      return;
    }

    if (
      this.commissionRule.effectiveTo &&
      this.commissionRule.effectiveTo < this.commissionRule.effectiveFrom
    ) {
      alert('Effective To date cannot be before Effective From date.');
      return;
    }

    const data = {
      productId: Number(this.commissionRule.productId),
      commissionPercentage: Number(this.commissionRule.commissionPercentage),
      effectiveFrom: this.commissionRule.effectiveFrom,
      effectiveTo: this.commissionRule.effectiveTo || null,
      isActive: this.commissionRule.isActive
    };

    if (this.isEditMode) {
      this.commissionRuleService
        .updateCommissionRule(this.selectedRuleId, data)
        .subscribe({
          next: () => {
            alert('Commission rule updated successfully.');
            this.showForm = false;
            this.resetForm();
            this.loadCommissionRules();
          },
          error: (error) => {
            console.error('Error updating commission rule:', error);
            alert('Unable to update commission rule.');
          }
        });
    } else {
      this.commissionRuleService.createCommissionRule(data).subscribe({
        next: () => {
          alert('Commission rule created successfully.');
          this.showForm = false;
          this.resetForm();
          this.loadCommissionRules();
        },
        error: (error) => {
          console.error('Error creating commission rule:', error);
          alert('Unable to create commission rule.');
        }
      });
    }
  }

  editCommissionRule(rule: any): void {
    this.isEditMode = true;
    this.selectedRuleId = rule.commissionRuleId;

    this.commissionRule = {
      productId: rule.productId,
      commissionPercentage: rule.commissionPercentage,
      effectiveFrom: rule.effectiveFrom?.substring(0, 10) ?? '',
      effectiveTo: rule.effectiveTo?.substring(0, 10) ?? '',
      isActive: rule.isActive
    };

    this.showForm = true;
  }

  resetForm(): void {
    this.isEditMode = false;
    this.selectedRuleId = 0;

    this.commissionRule = {
      productId: 0,
      commissionPercentage: 0,
      effectiveFrom: '',
      effectiveTo: '',
      isActive: true
    };
  }
}