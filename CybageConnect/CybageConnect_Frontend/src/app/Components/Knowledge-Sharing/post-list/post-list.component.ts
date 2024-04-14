import { Component, OnInit, ViewChild } from '@angular/core';
import { Post } from '../../../Models/post';
import { PostService } from '../../../Services/post.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.css'
})
export class PostListComponent implements OnInit{
   //Displaying all post
   posts: Post[] = [];
   loading: boolean = false;
 
   constructor(private postService: PostService) { }
 
   ngOnInit(): void {
     this.fetchPosts();
   }
 
   //Display all post
   fetchPosts(): void {
     this.loading = true;
     this.postService.getPosts().subscribe({
     next: (posts: Post[]) => {
       console.log(posts);
       this.posts = posts;
       this.loading = false;
     },
     error: (error: any) => {
       console.error('Error fetching posts:', error);
       this.loading = false;
     }
   });
   }

   //for adding post modal
  @ViewChild('myModal') myModal: any; // Reference to the modal element

  openModal() {
    this.myModal.nativeElement.style.display = 'block';
  }

  closeModal() {
    this.myModal.nativeElement.style.display = 'none';
  }
   
//collecting data from form
 formData: any = {
  cat : 1,
 }
 
  onSubmit(form: NgForm) {
    console.log('Form data:', this.formData);
    const post:Post = {
      id:this.formData.id,
      content :this.formData.content,
      categoryId : this.formData.cat,
      imageUrl : this.formData.file,
      userId:1,
      creationDate:new Date().toISOString()
    }
     

    //create and edit post
    if(!this.isEdit){
      this.postService.createPost(post).subscribe({
        next: (res) => {
          console.log(res);
          this.fetchPosts();
          this.closeModal();
        },
        error: (error: any) => {
          console.error('Error fetching posts:', error);
        }
      });
    }
    else{
      console.log(post);
       this.postService.updatePost(post).subscribe({
        next:(res)=>{
          console.log(res);
          this.fetchPosts();
          this.closeModal();
        },
        error: (error: any) => {
          console.error('Error fetching posts:', error);
        }
       })
    }
  }

  //for collecting post data
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    console.log(file);
    // You can now access the file object and perform further actions, such as uploading it to a server
  }

  deletePost(data:number){
    this.postService.deletePost(data).subscribe({
      next:(res) => {
        console.log(res);
        this.fetchPosts();
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }

  isEdit = false;
  editPost(data:number){
    this.isEdit = true;
    this.openModal();
    const current = this.posts.find(p=>p.id == data);
    this.formData.id = current.id;
    this.formData.content = current.content;
    this.formData.imageUrl = current.imageUrl;
    this.formData.cat = current.categoryId;
  }
 
}


