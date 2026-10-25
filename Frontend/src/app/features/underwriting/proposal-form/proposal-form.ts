
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs/operators';

import {
  UnderwritingService,
  CreateUnderwriting,
  MedicalDetails,
  FinancialDetails,
  ApiMessage
}from '../../../services/underwriting/underwriting';

@Component({
  selector: 'app-proposal-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './proposal-form.html',
  styleUrl: './proposal-form.scss'
})
export class ProposalForm {
  // Temporary IDs. Replace these with real logged-in customer,
  // selected quote and selected product IDs when authentication is integrated.
  form: CreateUnderwriting = {
    quote_id: 1,
    customer_id: 1,
    product_id: 1
  };

  medical: MedicalDetails = {
    height: 0,
    weight: 0,
    blood_pressure: '',
    diabetes: false,
    heart_disease: false,
    cancer_history: false,
    smoking: false,
    alcohol: false,
    medical_history: '',
    medical_report_path: ''
  };

  financial: FinancialDetails = {
    annual_income: 0,
    monthly_income: 0,
    employer_name: '',
    occupation: '',
    existing_insurance: 0,
    loan_liability: 0
  };

  declarationAccepted = false;
  loading = false;
  successMessage = '';
  errorMessage = '';
  underwritingId: number | null = null;

  constructor(private readonly api: UnderwritingService) {}

  submitProposal(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.loading) {
      return;
    }

    if (!this.declarationAccepted) {
      this.errorMessage =
        'Please accept the declaration before submitting.';
      return;
    }

    if (
      !Number.isInteger(this.form.quote_id) ||
      this.form.quote_id <= 0 ||
      !Number.isInteger(this.form.customer_id) ||
      this.form.customer_id <= 0 ||
      !Number.isInteger(this.form.product_id) ||
      this.form.product_id <= 0
    ) {
      this.errorMessage =
        'Quote, customer and product IDs must be valid positive integers.';
      return;
    }

    if (
      !Number.isFinite(this.medical.height) ||
      !Number.isFinite(this.medical.weight) ||
      this.medical.height <= 0 ||
      this.medical.weight <= 0
    ) {
      this.errorMessage = 'Enter a valid height and weight.';
      return;
    }

    if (
      !Number.isFinite(this.financial.annual_income) ||
      !Number.isFinite(this.financial.monthly_income) ||
      this.financial.annual_income < 0 ||
      this.financial.monthly_income < 0 ||
      !this.financial.employer_name.trim() ||
      !this.financial.occupation.trim()
    ) {
      this.errorMessage =
        'Enter valid income, employer and occupation details.';
      return;
    }

    this.medical.bmi = Number(
      (
        this.medical.weight /
        Math.pow(this.medical.height / 100, 2)
      ).toFixed(2)
    );

    this.loading = true;

    this.api.createCase(this.form)
      .pipe(finalize(() => {
        this.loading = false;
      }))
      .subscribe({
        next: (response: ApiMessage) => {
          const id = Number(response.underwriting_id);

          if (!Number.isInteger(id) || id <= 0) {
            this.errorMessage =
              'The API did not return a valid underwriting_id. Check the create-case response.';
            return;
          }

          this.underwritingId = id;
          this.medical.underwriting_id = id;
          this.financial.underwriting_id = id;

          this.saveMedicalDetails(id);
        },
        error: (error: unknown) => {
          this.errorMessage = this.getErrorMessage(
            error,
            'Unable to create the underwriting case.'
          );
        }
      });
  }

  private saveMedicalDetails(id: number): void {
    this.loading = true;

    this.api.addMedical(id, this.medical)
      .pipe(finalize(() => {
        this.loading = false;
      }))
      .subscribe({
        next: () => {
          this.saveFinancialDetails(id);
        },
        error: (error: unknown) => {
          this.errorMessage = this.getErrorMessage(
            error,
            'The case was created, but medical details could not be saved.'
          );
        }
      });
  }

  private saveFinancialDetails(id: number): void {
    this.loading = true;

    this.api.addFinancial(id, this.financial)
      .pipe(finalize(() => {
        this.loading = false;
      }))
      .subscribe({
        next: () => {
          this.successMessage =
            `Proposal submitted successfully. Case ID: ${id}`;
          this.errorMessage = '';
        },
        error: (error: unknown) => {
          this.errorMessage = this.getErrorMessage(
            error,
            'The case and medical details were saved, but financial details failed.'
          );
        }
      });
  }

  private getErrorMessage(
    error: unknown,
    fallback: string
  ): string {
    if (
      typeof error === 'object' &&
      error !== null &&
      'error' in error
    ) {
      const body = (error as {
        error?: { msg?: string; message?: string };
      }).error;

      return body?.msg || body?.message || fallback;
    }

    return fallback;
  }
}
