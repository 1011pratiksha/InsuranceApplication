import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

interface Product {
  product_id: number;
  category_id: number;
  product_code: string;
  product_name: string;
  product_type: string;
  description?: string;
  minimum_age: number;
  maximum_age: number;
  minimum_sum_insured: number;
  maximum_sum_insured: number;
  status: string;
}

interface Category {
  category_id: number;
  category_name: string;
  status: string;
}

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer.html',
  styleUrl: './customer.css'
})
export class Customer implements OnInit {
  private http = inject(HttpClient);
  private base = 'https://localhost:7225/api/v1';

  products: Product[] = [];
  categories: Category[] = [];
  selectedCategory = 'All';
  searchText = '';
  loading = false;
  error = '';

  selectedProduct: Product | null = null;
  coverages: any[] = [];
  benefits: any[] = [];
  riders: any[] = [];

  ngOnInit(): void {
    this.loadCategories();
    this.loadProducts();
  }

  loadCategories(): void {
    this.http.get<Category[]>(`${this.base}/product-categories`)
      .subscribe({
        next: data => this.categories = data.filter(
          c => c.status?.toLowerCase() === 'active'
        ),
        error: () => this.error = 'Unable to load insurance categories.'
      });
  }

  loadProducts(): void {
    this.loading = true;
    this.error = '';

    this.http.get<Product[]>(`${this.base}/products`).subscribe({
      next: data => {
        this.products = data.filter(
          p => p.status?.toLowerCase() === 'active'
        );
        this.loading = false;
      },
      error: () => {
        this.error = 'Unable to load products. Please try again later.';
        this.loading = false;
      }
    });
  }

  get filteredProducts(): Product[] {
    return this.products.filter(p => {
      const matchesSearch =
        `${p.product_name} ${p.product_code} ${p.product_type}`
          .toLowerCase()
          .includes(this.searchText.toLowerCase());

      const category = this.categories.find(
        c => c.category_id === p.category_id
      );

      const matchesCategory =
        this.selectedCategory === 'All' ||
        category?.category_name === this.selectedCategory;

      return matchesSearch && matchesCategory;
    });
  }

  categoryName(id: number): string {
    return this.categories.find(c => c.category_id === id)
      ?.category_name ?? 'Insurance';
  }

  chooseCategory(name: string): void {
    this.selectedCategory = name;
    this.selectedProduct = null;
  }

  viewProduct(product: Product): void {
    this.selectedProduct = product;
    const id = product.product_id;

    this.coverages = [];
    this.benefits = [];
    this.riders = [];

    this.http.get<any[]>(`${this.base}/products/${id}/coverages`)
      .subscribe({
        next: data => this.coverages = data,
        error: () => this.coverages = []
      });

    this.http.get<any[]>(`${this.base}/products/${id}/benefits`)
      .subscribe({
        next: data => this.benefits = data,
        error: () => this.benefits = []
      });

    this.http.get<any[]>(`${this.base}/products/${id}/riders`)
      .subscribe({
        next: data => this.riders = data,
        error: () => this.riders = []
      });
  }

  closeDetails(): void {
    this.selectedProduct = null;
  }
}