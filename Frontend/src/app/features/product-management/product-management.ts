
import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

type Category = {
  category_id: number;
  category_name: string;
  description?: string;
  status: string;
};

type Product = {
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
};

@Component({
  selector: 'app-product-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-management.html',
  styleUrl: './product-management.css'
})
export class ProductManagement implements OnInit {
  private http = inject(HttpClient);
  private base = 'https://localhost:7225/api/v1';

  categories: Category[] = [];
  products: Product[] = [];
  selectedProduct: Product | null = null;

  coverages: any[] = [];
  benefits: any[] = [];
  riders: any[] = [];
  frequencies: any[] = [];
  commission: any = null;

  activeSection = 'products';
  message = '';
  error = '';
  loading = false;
  showCategoryForm = false;
  showProductForm = false;

  // Number of active products shown on the dashboard
  get activeProductCount(): number {
    return this.products.filter(
      p => p.status?.toLowerCase() === 'active'
    ).length;
  }

  categoryForm: any = {
    category_name: '',
    description: '',
    status: 'Active'
  };

  productForm: any = {
    category_id: null,
    product_code: '',
    product_name: '',
    product_type: 'Life',
    description: '',
    minimum_age: 18,
    maximum_age: 65,
    minimum_sum_insured: 100000,
    maximum_sum_insured: 10000000,
    status: 'Inactive'
  };

  coverageForm: any = {
    coverage_name: '',
    minimum_coverage: 0,
    maximum_coverage: 0,
    waiting_period_days: 0,
    deductible_amount: 0
  };

  benefitForm: any = {
    benefit_name: '',
    benefit_description: '',
    benefit_limit: null
  };

  riderForm: any = {
    rider_name: '',
    rider_description: '',
    rider_premium: 0,
    status: 'Active'
  };

  frequencyForm: any = {
    frequency_name: 'Monthly',
    installment_count: 12,
    status: 'Active'
  };

  commissionForm: any = {
    first_year_commission: 0,
    renewal_commission: 0,
    branch_commission: 0
  };

  ngOnInit(): void {
    this.loadCategories();
    this.loadProducts();
    this.loadFrequencies();
  }

  private clearMessages(): void {
    this.message = '';
    this.error = '';
  }

  private handleError(err: any): void {
    this.loading = false;
    this.error =
      err?.error?.msg ||
      err?.error?.message ||
      'Request failed. Check the backend API and browser console.';
  }

  loadCategories(): void {
    this.http.get<Category[]>(`${this.base}/product-categories`)
      .subscribe({
        next: data => this.categories = data,
        error: err => this.handleError(err)
      });
  }

  loadProducts(): void {
    this.loading = true;

    this.http.get<Product[]>(`${this.base}/products`)
      .subscribe({
        next: data => {
          this.products = data;
          this.loading = false;
        },
        error: err => this.handleError(err)
      });
  }

  categoryName(categoryId: number): string {
    return this.categories.find(
      c => c.category_id === categoryId
    )?.category_name ?? 'Unknown category';
  }

  saveCategory(): void {
    this.clearMessages();

    if (!this.categoryForm.category_name.trim()) {
      this.error = 'Enter a category name.';
      return;
    }

    this.http.post(
      `${this.base}/product-categories`,
      this.categoryForm
    ).subscribe({
      next: () => {
        this.message = 'Category created successfully.';

        this.categoryForm = {
          category_name: '',
          description: '',
          status: 'Active'
        };

        this.showCategoryForm = false;
        this.loadCategories();
      },
      error: err => this.handleError(err)
    });
  }

  saveProduct(): void {
    this.clearMessages();

    if (!this.productForm.category_id) {
      this.error = 'Please select a product category.';
      return;
    }

    if (
      !this.productForm.product_code.trim() ||
      !this.productForm.product_name.trim()
    ) {
      this.error = 'Product code and product name are required.';
      return;
    }

    if (
      this.productForm.minimum_age < 0 ||
      this.productForm.maximum_age < this.productForm.minimum_age
    ) {
      this.error = 'Please enter valid age limits.';
      return;
    }

    if (
      this.productForm.minimum_sum_insured <= 0 ||
      this.productForm.maximum_sum_insured <
        this.productForm.minimum_sum_insured
    ) {
      this.error = 'Please enter valid sum-insured limits.';
      return;
    }

    const payload = {
      ...this.productForm,
      category_id: Number(this.productForm.category_id)
    };

    this.http.post(`${this.base}/products`, payload)
      .subscribe({
        next: () => {
          this.message =
            'Product created. Configure its details before publishing.';

          this.showProductForm = false;

          this.productForm = {
            category_id: null,
            product_code: '',
            product_name: '',
            product_type: 'Life',
            description: '',
            minimum_age: 18,
            maximum_age: 65,
            minimum_sum_insured: 100000,
            maximum_sum_insured: 10000000,
            status: 'Inactive'
          };

          this.loadProducts();
        },
        error: err => this.handleError(err)
      });
  }

  openProduct(product: Product): void {
    this.selectedProduct = product;
    this.activeSection = 'coverages';
    this.clearMessages();
    this.loadProductConfiguration();
  }

  backToProducts(): void {
    this.selectedProduct = null;
    this.activeSection = 'products';
    this.clearMessages();
  }

  loadProductConfiguration(): void {
    if (!this.selectedProduct) {
      return;
    }

    const id = this.selectedProduct.product_id;

    this.http.get<any[]>(
      `${this.base}/products/${id}/coverages`
    ).subscribe({
      next: data => this.coverages = data,
      error: err => this.handleError(err)
    });

    this.http.get<any[]>(
      `${this.base}/products/${id}/benefits`
    ).subscribe({
      next: data => this.benefits = data,
      error: err => this.handleError(err)
    });

    this.http.get<any[]>(
      `${this.base}/products/${id}/riders`
    ).subscribe({
      next: data => this.riders = data,
      error: err => this.handleError(err)
    });

    this.http.get<any>(
      `${this.base}/products/${id}/commission`
    ).subscribe({
      next: data => {
        this.commission = data;

        if (data) {
          this.commissionForm = {
            first_year_commission: data.first_year_commission ?? 0,
            renewal_commission: data.renewal_commission ?? 0,
            branch_commission: data.branch_commission ?? 0
          };
        }
      },
      error: () => this.commission = null
    });
  }

  saveCoverage(): void {
    if (!this.selectedProduct) {
      return;
    }

    this.clearMessages();

    this.http.post(
      `${this.base}/products/${this.selectedProduct.product_id}/coverages`,
      this.coverageForm
    ).subscribe({
      next: () => {
        this.message = 'Coverage added.';

        this.coverageForm = {
          coverage_name: '',
          minimum_coverage: 0,
          maximum_coverage: 0,
          waiting_period_days: 0,
          deductible_amount: 0
        };

        this.loadProductConfiguration();
      },
      error: err => this.handleError(err)
    });
  }

  saveBenefit(): void {
    if (!this.selectedProduct) {
      return;
    }

    this.clearMessages();

    this.http.post(
      `${this.base}/products/${this.selectedProduct.product_id}/benefits`,
      this.benefitForm
    ).subscribe({
      next: () => {
        this.message = 'Benefit added.';

        this.benefitForm = {
          benefit_name: '',
          benefit_description: '',
          benefit_limit: null
        };

        this.loadProductConfiguration();
      },
      error: err => this.handleError(err)
    });
  }

  saveRider(): void {
    if (!this.selectedProduct) {
      return;
    }

    this.clearMessages();

    this.http.post(
      `${this.base}/products/${this.selectedProduct.product_id}/riders`,
      this.riderForm
    ).subscribe({
      next: () => {
        this.message = 'Rider added.';

        this.riderForm = {
          rider_name: '',
          rider_description: '',
          rider_premium: 0,
          status: 'Active'
        };

        this.loadProductConfiguration();
      },
      error: err => this.handleError(err)
    });
  }

  loadFrequencies(): void {
    this.http.get<any[]>(
      `${this.base}/premium-frequencies`
    ).subscribe({
      next: data => this.frequencies = data,
      error: err => this.handleError(err)
    });
  }

  saveFrequency(): void {
    this.clearMessages();

    this.http.post(
      `${this.base}/premium-frequencies`,
      this.frequencyForm
    ).subscribe({
      next: () => {
        this.message = 'Premium frequency added.';
        this.loadFrequencies();
      },
      error: err => this.handleError(err)
    });
  }

  saveCommission(): void {
    if (!this.selectedProduct) {
      return;
    }

    this.clearMessages();

    const id = this.selectedProduct.product_id;

    const request = this.commission
      ? this.http.put(
          `${this.base}/product-commissions/${this.commission.commission_id}`,
          this.commissionForm
        )
      : this.http.post(
          `${this.base}/products/${id}/commission`,
          this.commissionForm
        );

    request.subscribe({
      next: () => {
        this.message = 'Commission saved.';
        this.loadProductConfiguration();
      },
      error: err => this.handleError(err)
    });
  }

  publishProduct(product: Product): void {
    this.clearMessages();

    const payload = {
      ...product,
      status: 'Active'
    };

    this.http.put(
      `${this.base}/products/${product.product_id}`,
      payload
    ).subscribe({
      next: () => {
        this.message = 'Product activated.';
        this.loadProducts();

        if (
          this.selectedProduct?.product_id === product.product_id
        ) {
          this.selectedProduct = {
            ...product,
            status: 'Active'
          };
        }
      },
      error: err => this.handleError(err)
    });
  }

  deactivateProduct(product: Product): void {
    this.clearMessages();

    this.http.delete(
      `${this.base}/products/${product.product_id}`
    ).subscribe({
      next: () => {
        this.message = 'Product deactivated.';
        this.loadProducts();

        if (
          this.selectedProduct?.product_id === product.product_id
        ) {
          this.selectedProduct = {
            ...product,
            status: 'Inactive'
          };
        }
      },
      error: err => this.handleError(err)
    });
  }

  deactivateCategory(category: Category): void {
    this.clearMessages();

    this.http.delete(
      `${this.base}/product-categories/${category.category_id}`
    ).subscribe({
      next: () => {
        this.message = 'Category deactivated.';
        this.loadCategories();
      },
      error: err => this.handleError(err)
    });
  }

  deleteCoverage(id: number): void {
    if (!confirm('Delete this coverage?')) {
      return;
    }

    this.http.delete(`${this.base}/coverages/${id}`)
      .subscribe({
        next: () => {
          this.message = 'Coverage deleted.';
          this.loadProductConfiguration();
        },
        error: err => this.handleError(err)
      });
  }

  deleteBenefit(id: number): void {
    if (!confirm('Delete this benefit?')) {
      return;
    }

    this.http.delete(`${this.base}/benefits/${id}`)
      .subscribe({
        next: () => {
          this.message = 'Benefit deleted.';
          this.loadProductConfiguration();
        },
        error: err => this.handleError(err)
      });
  }

  deleteRider(id: number): void {
    if (!confirm('Delete this rider?')) {
      return;
    }

    this.http.delete(`${this.base}/riders/${id}`)
      .subscribe({
        next: () => {
          this.message = 'Rider deleted.';
          this.loadProductConfiguration();
        },
        error: err => this.handleError(err)
      });
  }

  setSection(section: string): void {
    this.activeSection = section;
    this.clearMessages();
  }
}
