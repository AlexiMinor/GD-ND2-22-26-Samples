import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { Observable } from 'rxjs';
import queryString from 'query-string';

@Injectable({
  providedIn: 'root',
})
export class ApiService {

  constructor(private http: HttpClient) {}

  private getUrl(url: string) : string {
    return environment.apiBaseUrl + url;
  }

  get<T>(url: string, data: object = {}) : Observable<T | null> {
    if (Object.keys(data).length > 0){
      url = `${url}?${queryString.stringify(data)}`
    }
    return this.http.get<T>(this.getUrl(url));
  }

  post<T>(url: string, data: object = {}, options = {}) : Observable<T | null> {
    return this.http.post<T>(this.getUrl(url), data, options);
  }


  request(method: string, url: string, data: object = {}, options = {}) : Observable<any> {
    const request = new HttpRequest(method, this.getUrl(url), {body: data, ...options});
    return this.http.request(request);
  }

  //potentially add methods
  patch<T>(url: string, data: object = {}, options = {}) : Observable<T | null> {
    return this.http.patch<T>(this.getUrl(url), data, options);
  }
}
