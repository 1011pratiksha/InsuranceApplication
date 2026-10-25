
import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import {
  Product,
  ProductManagementService
} from '../../../services/product-management/product-management';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './admin.html',
  styleUrl: './admin.css'
})
export class Admin implements OnInit {
  private service = inject(ProductManagementService);

  products: Product[] = [];
  error = '';
  loading = false;

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.error = '';

    this.service.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading products:', err);
        this.error = 'Unable to load products. Check that the backend is running.';
        this.loading = false;
      }
    });
  }

  get activeCount(): number {
    return this.products.filter(
      p => p.status.toLowerCase() === 'active'
    ).length;
  }

  get inactiveCount(): number {
    return this.products.filter(
      p => p.status.toLowerCase() !== 'active'
    ).length;
  }
}
