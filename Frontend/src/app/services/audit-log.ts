import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuditLog {
  private apiUrl = 'http://localhost:5050/api/AuditLogs';

  constructor(private http: HttpClient) {}

  getAllLogs(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
