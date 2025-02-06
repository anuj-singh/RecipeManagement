import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  private HOME_URL: any = '';

  constructor(private http: HttpClient) {}

  httpGetRequest(url: any) {
    return this.http.get(`${this.HOME_URL}${url}`);
  }

  httpPostRequest(url: any, payload: any) {
    return this.http.post(`${this.HOME_URL}${url}`, payload);
  }

  httpUpdateRequest(url: any, payload: any) {
    return this.http.put(`${this.HOME_URL}${url}`, payload);
  }

  httpDeleteRequest(url: any, id: any) {
    return this.http.delete(`${this.HOME_URL}${url}${id}`);
  }
}
