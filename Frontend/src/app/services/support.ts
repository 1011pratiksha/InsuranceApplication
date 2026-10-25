import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SupportService {
  private apiUrl = 'https://localhost:44304/api/Support';

  constructor(private http: HttpClient) {}

  getTickets(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getTicketById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  createTicket(ticket: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, ticket);
  }

  updateTicket(id: number, ticket: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, ticket);
  }

  deleteTicket(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
