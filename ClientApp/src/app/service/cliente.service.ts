import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client } from '../models/Client';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  baseURL = `${environment.mainUrlAPI}client`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Client[]> {
    return this.http.get<Client[]>(this.baseURL);
  }

  getOne(client: Client): Observable<Client> {
    return this.http.get<Client>(`${this.baseURL}`);
  }

  post(client: Client) {
    return this.http.post(this.baseURL, client);
  }

  put(client: Client) {
    return this.http.put(this.baseURL, client);
  }
  // delete(client: Client) {
  //   return this.http.delete(this.baseURL, client);
  // }

}
