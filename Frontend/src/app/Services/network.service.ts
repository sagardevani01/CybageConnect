import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { User } from '../Models/user';
import { Network } from '../Models/Network';

@Injectable({
  providedIn: 'root',
})
export class NetworkService {
  private apiUrl = 'http://localhost:5029/api/Network';

  constructor(private http: HttpClient) {}
  getConnectedUsers(id: number): Observable<any> {
    return this.http.get<User>(`${this.apiUrl}/connections/${id}`);
  }
  getUnconnectedUsers(id: number): Observable<any> {
    return this.http.get<User>(`${this.apiUrl}/unconnected-users/${id}`);
  }
  getRequestedUsers(id: number): Observable<any> {
    return this.http.get<User>(`${this.apiUrl}/connection-requests/${id}`);
  }
  sendConnectionRequest(network: Network): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/send-request`, network);
  }

  acceptConnectionRequest(network: Network): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/accept-request`, network);
  }
  declineConnectionRequest(network: Network): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/decline-request`, network);
  }
}
