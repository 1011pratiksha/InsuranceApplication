import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Customer,
  CustomerDetail,
  CreateCustomerRequest,
  UpdateCustomerRequest,
  CustomerAddress,
  CreateAddressRequest,
  Nominee,
  CreateNomineeRequest,
  CustomerDocument,
  UploadDocumentRequest,
  CustomerSearchResponse
} from './customer-registration.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerRegistrationService {

  private http = inject(HttpClient);

  private readonly baseUrl = '/api/v1';

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(`${this.baseUrl}/customers`);
  }

  getCustomerById(id: number): Observable<CustomerDetail> {
    return this.http.get<CustomerDetail>(`${this.baseUrl}/customers/${id}`);
  }

  createCustomer(request: CreateCustomerRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/customers`, request);
  }

  updateCustomer(id: number, request: UpdateCustomerRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/customers/${id}`, request);
  }

  deleteCustomer(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/customers/${id}`);
  }

  searchCustomers(searchText: string): Observable<CustomerSearchResponse> {
    return this.http.get<CustomerSearchResponse>(
      `${this.baseUrl}/customers/search?searchText=${encodeURIComponent(searchText)}`
    );
  }

  addAddress(customerId: number, request: CreateAddressRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/customers/${customerId}/addresses`, request);
  }

  getAddressesByCustomerId(customerId: number): Observable<CustomerAddress[]> {
    return this.http.get<CustomerAddress[]>(`${this.baseUrl}/customers/${customerId}/addresses`);
  }

  updateAddress(id: number, request: CreateAddressRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/addresses/${id}`, request);
  }

  deleteAddress(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/addresses/${id}`);
  }

  addNominee(customerId: number, request: CreateNomineeRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/customers/${customerId}/nominees`, request);
  }

  getNomineesByCustomerId(customerId: number): Observable<Nominee[]> {
    return this.http.get<Nominee[]>(`${this.baseUrl}/customers/${customerId}/nominees`);
  }

  updateNominee(id: number, request: CreateNomineeRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/nominees/${id}`, request);
  }

  deleteNominee(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/nominees/${id}`);
  }

  uploadDocument(customerId: number, request: UploadDocumentRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/customers/${customerId}/documents`, request);
  }

  getDocumentsByCustomerId(customerId: number): Observable<CustomerDocument[]> {
    return this.http.get<CustomerDocument[]>(`${this.baseUrl}/customers/${customerId}/documents`);
  }

  verifyDocument(id: number, status: string = 'Verified'): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/documents/${id}/verify?status=${encodeURIComponent(status)}`, {});
  }

  deleteDocument(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/documents/${id}`);
  }
}
