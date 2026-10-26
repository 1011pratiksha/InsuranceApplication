
import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ReportsService {

private http = inject(HttpClient);

private url = 'https://localhost:7008/api/v1/reports';


  // CUSTOMER REPORT
 GetCustomerReports(filter?: any) {
  return this.http.get<any>(`${this.url}/customers`, {
    params: filter
  });
}


  // POLICY REPORT
  GetPolicyReports(filter?: any) {
  return this.http.get<any>(`${this.url}/policies`, {
    params: filter
  });
}


  // PREMIUM REPORT
GetPremiumReports(filter?: any) {
  return this.http.get<any>(`${this.url}/premiums`, {
    params: filter
  });
}


  // CLAIMS REPORT
 GetClaimsReports(filter?: any) {
  return this.http.get<any>(`${this.url}/claims`, {
    params: filter
  });
}


  // UNDERWRITING REPORT
 GetUnderwritingReports(filter?: any) {
  return this.http.get<any>(`${this.url}/underwriting`, {
    params: filter
  });
}


  // AGENT REPORT
  GetAgentReports(filter?: any) {
  return this.http.get<any>(`${this.url}/agents`, {
    params: filter
  });
}


  // FINANCIAL REPORT
  GetFinancialReports(filter?: any) {
  return this.http.get<any>(`${this.url}/financial`, {
    params: filter
  });
}





  // GET ALL REPORT SCHEDULES
  GetReportSchedules() {
    return this.http.get<any>(`${this.url}/schedule`);
  }


  // CREATE REPORT SCHEDULE
  CreateReportSchedule(data: any) {
    return this.http.post<any>(`${this.url}/schedule`, data);
  }


  // UPDATE REPORT SCHEDULE
  UpdateReportSchedule(id: number, data: any) {
    return this.http.put<any>(`${this.url}/schedule/${id}`, data);
  }


  // DELETE REPORT SCHEDULE
  DeleteReportSchedule(id: number) {
    return this.http.delete<any>(`${this.url}/schedule/${id}`);
  }

}
