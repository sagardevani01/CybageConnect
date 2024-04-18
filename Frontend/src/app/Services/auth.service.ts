import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import axios from 'axios';
import { FormGroup } from '@angular/forms';
import { Login } from '../Models/login';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5029/api'; // Replace with your API URl

  constructor(private http: HttpClient) {}

  logIn(login: Login): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Auth/login`, login);
  }
  registerUser(user: User): Observable<any> {
    return this.http.post<User>(`${this.apiUrl}/Auth/register`, user);
  }
  decodeToken() {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      return decodedToken.nameid;
    } else {
      return null;
    }
  }
  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout(): void {
    localStorage.removeItem('token');
  }
  async uploadProfilePhoto(formData: FormData) {
    const result = await axios.post(`${this.apiUrl}/Utility/upload`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return result.data.url;
  }
  // Helper method to mark all form controls as touched
}
