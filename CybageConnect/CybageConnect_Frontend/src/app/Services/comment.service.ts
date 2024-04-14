import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Comment } from '../Models/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private apiUrl = 'http://localhost:5029/api/Comment'; 
  constructor(private http:HttpClient) { }

  //get comments for post 
  getComments(postId:number) : Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/${postId}`);
  }

  addComment(comment:Comment):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}`,comment);
  }

  updateComment(comment:Comment):Observable<any>{
    return this.http.put<any>(`${this.apiUrl}`,comment)
  }

  deleteComment(commentId:number):Observable<any>{
    return this.http.delete<any>(`${this.apiUrl}/${commentId}`);
  }
}
