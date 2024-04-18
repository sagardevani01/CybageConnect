import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from '../Models/post';
import axios from 'axios';
@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiUrl = 'http://localhost:5029/api/Post';
  constructor(private http: HttpClient) { }

  
  getPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(this.apiUrl);
  }

  // Create a new post
  createPost(post: Post): Observable<Post> {
    return this.http.post<Post>(this.apiUrl, post);
  }

  // Update an existing post
  updatePost(post: Post): Observable<Post> {
    return this.http.put<Post>(`${this.apiUrl}/${post.id}`, post);
  }

  // Delete a post
  deletePost(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }


  async fileUpload(file){
    let formData = new FormData();
    formData.append('file', file);
    const res = await axios.post(`http://localhost:5029/api/Utility/upload`, formData, {
      headers:{
        'Content-Type':"multipart/form-data"
      }
    })
  return res.data.url;    
  }
}
