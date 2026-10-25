
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import {
  UnderwritingCase,
  UnderwritingService
} from '../../../services/underwriting/underwriting';

@Component({
  selector: 'app-underwriting-queue',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './underwriting-queue.html',
  styleUrl: './underwriting-queue.scss'
})
export class UnderwritingQueue implements OnInit {
  cases: UnderwritingCase[] = [];
  searchText = '';
  selectedStatus = 'All';
  loading = false;
  errorMessage = '';

  constructor(
    private readonly api: UnderwritingService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.loadCases();
  }

  loadCases(): void {
    this.loading = true;
    this.errorMessage = '';

    this.api.getCases().subscribe({
      next: (data: UnderwritingCase[]) => {
        this.cases = Array.isArray(data) ? data : [];
        this.loading = false;
      },
      error: (error: unknown) => {
        this.loading = false;

        const response = error as {
          error?: { msg?: string; message?: string };
        };

        this.errorMessage =
          response?.error?.msg ??
          response?.error?.message ??
          'Unable to load cases. Check that the backend is running.';
      }
    });
  }

  get filteredCases(): UnderwritingCase[] {
    const term = this.searchText.trim().toLowerCase();

    return this.cases.filter((item: UnderwritingCase) => {
      const matchesSearch =
        !term ||
        String(item.underwriting_id).includes(term) ||
        String(item.quote_id).includes(term) ||
        String(item.customer_id).includes(term) ||
        String(item.product_id).includes(term);

      const matchesStatus =
        this.selectedStatus === 'All' ||
        (item.status ?? '').toLowerCase() ===
          this.selectedStatus.toLowerCase();

      return matchesSearch && matchesStatus;
    });
  }

  openCase(id: number): void {
    this.router.navigate(['/admin/uw-case', id]);
  }
}
