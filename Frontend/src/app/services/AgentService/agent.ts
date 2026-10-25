import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Agent {

  private apiUrl = 'https://localhost:7014/api/v1/agents';

  constructor(private http: HttpClient) {}

  getAllAgents() {
    return this.http.get<any[]>(this.apiUrl, {
      withCredentials: true
    });
  }

  getAgentById(id: number) {
    return this.http.get(`${this.apiUrl}/${id}`, {
      withCredentials: true
    });
  }

  createAgent(data: any) {
    return this.http.post(this.apiUrl, data, {
      withCredentials: true
    });
  }

  updateAgent(id: number, data: any) {
    return this.http.put(`${this.apiUrl}/${id}`, data, {
      withCredentials: true
    });
  }

  getAllUsers() {
  return this.http.get<any[]>(`${this.apiUrl}/users`, {
    withCredentials: true
  });
}
}