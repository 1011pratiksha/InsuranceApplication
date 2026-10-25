
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Product {
  product_id: number;
  category_id: number;
  product_code: string;
  product_name: string;
  product_type: string;
  description?: string | null;
  minimum_age: number;
  maximum_age: number;
  minimum_sum_insured: number;
  maximum_sum_insured: number;
  status: string;
}

export type ProductRequest = Omit<Product, 'product_id'>;

@Injectable({
  providedIn: 'root'
})
export class ProductManagementService {
  private http = inject(HttpClient);

  private baseUrl = 'https://localhost:7225/api/v1/products';

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }

  createProduct(product: ProductRequest): Observable<Product> {
    return this.http.post<Product>(this.baseUrl, product);
  }

  updateProduct(
    id: number,
    product: ProductRequest
  ): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/${id}`, product);
  }
}
