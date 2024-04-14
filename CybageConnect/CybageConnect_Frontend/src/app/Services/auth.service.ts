import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from '../Models/login';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


   private apiUrl = 'https://example.com/api/data'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  logIn(login :Login): Observable<Login> {
    return this.http.post<Login>(this.apiUrl,login);
  }
  signUp(user:User): Observable<User> {
    return this.http.post<User>(this.apiUrl,user);
  }
}
