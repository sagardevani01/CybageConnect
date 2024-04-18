import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class ProfileServiceService {

  private profileUrl = 'http://localhost:5029/api/Profile/'; 

  constructor(private http: HttpClient) { }

  getUserProfile(id: number): Observable<any>{
    // const profileUrl = `${this.apiUrl}/Profile/`+id;
    return this.http.get<User>(this.profileUrl+id);
  }

  updateUserProfile(id: number, user: User): Observable<any>{
    return this.http.put<User>(this.profileUrl+id, user);
  }
}
