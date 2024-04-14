import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
import { Like } from '../Models/like';

@Injectable({
  providedIn: 'root'
})
export class LikeService {

  private apiUrl = 'http://localhost:5029/api/Likes'; 

  constructor(private http: HttpClient) { }

  //All Likes for post
  getLikesForPost(postId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${postId}`);
  }

  //GetStatus for particular user liked the post
  getLike(postId:number,userId:number): Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/${postId}/${userId}`);
  }

  // Like a post by its ID
  likePost(like:Like): Observable<any> {
    return this.http.post<Like>(`${this.apiUrl}/Like`,like);
  }

  // Unlike a post by its ID
  unlikePost(postId:number,userId:number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${postId}/${userId}`);
  }
}
