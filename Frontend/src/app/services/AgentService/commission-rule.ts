import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommissionRule {

  private apiUrl = 'https://localhost:7014/api/v1/commission-rules';

  constructor(private http: HttpClient) {}

  // Get all commission rules
  getAllCommissionRules() {
    return this.http.get<any[]>(this.apiUrl, {
      withCredentials: true
    });
  }

  // Get commission rule by product ID
  getCommissionRuleByProductId(productId: number) {
    return this.http.get<any>(
      `${this.apiUrl}/product/${productId}`,
      { withCredentials: true }
    );
  }

  // Create commission rule
  createCommissionRule(data: any) {
    return this.http.post(this.apiUrl, data, {
      withCredentials: true
    });
  }

  // Update commission rule
  updateCommissionRule(ruleId: number, data: any) {
    return this.http.put(`${this.apiUrl}/${ruleId}`, data, {
      withCredentials: true
    });
  }


  getAllProducts() {
  return this.http.get<any[]>(
    `${this.apiUrl}/products`,
    { withCredentials: true }
  );
}
}