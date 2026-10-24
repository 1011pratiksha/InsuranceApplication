import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Policyservices {

  constructor(private http:HttpClient)
  {

  }
  
  url1="https://localhost:44374/api/v1/service-requests/create-requests"
  
  createreq(data:any){
   return this.http.post<any>(this.url1,data);
  }

  url2="https://localhost:44374/api/v1/service-requests/get-requests"

  allreq()
  {
    return this.http.get<any>(this.url2);
  }

  url3="https://localhost:44374/api/v1/service-requests/update-status"
  updatereq(id:any,status:any)
  {
    return this.http.put<any>(`${this.url3}/${id}`,{status});
  }

   endorsementUrl = 'https://localhost:44374/api/v1/endorsements';

    createEndorsement(data: any) {
    return this.http.post<any>(this.endorsementUrl, data);
  }

  getEndorsement(id: number) {
    return this.http.get<any>(`${this.endorsementUrl}/${id}`);
  }

  approveEndorsement(id: number) {
    return this.http.put<any>(
      `${this.endorsementUrl}/approve/${id}`,
      {}
    );
  }

  rejectEndorsement(id: number) {
    return this.http.put<any>(
      `${this.endorsementUrl}/reject/${id}`,
      {}
    );
  }
}
