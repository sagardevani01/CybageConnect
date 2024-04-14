import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { Post } from '../../../../Models/post';
import { LikeService } from '../../../../Services/like.service';
import { Like } from '../../../../Models/like';
import { CommentService } from '../../../../Services/comment.service';
import { Comment } from '../../../../Models/comment';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrl: './post.component.css'
})
export class PostComponent {
  //get data from parent
  @Input()
  postDetail:Post;
  liked: boolean;
  likes:number;

  //send data to parent
  @Output()
  deletePostId = new EventEmitter<number>();

  @Output()
  editPostId = new EventEmitter<number>();


  constructor(private likeService: LikeService,private CommentService: CommentService) { }

  ngOnInit(): void {
    // Fetch initial like status for the post
    this.fetchLikeStatus();
    this.getLike();
    this.fetchComments();

  }  

  //fetching all likes
  fetchLikeStatus(): void {
    this.likeService.getLikesForPost(this.postDetail.id).subscribe({
      next:(response) => {
        console.log(response);
        this.likes = response;
      },
      error:(error) => {
        console.error('Error fetching like status:', error);
      }
    });
  }


  //fetch status of like for current user
  getLike(): void {
    this.likeService.getLike(this.postDetail.id,1).subscribe({
      next:(response) => {
        console.log(response);
        this.liked = response;
      },
      error:(error) => {
        console.error('Error fetching like status:', error);
      }
    });
  }

  //like and unlike
  toggleLike(): void {
    if (this.liked) {
      // Unlike the post
      this.likeService.unlikePost(this.postDetail.id,1).subscribe({
        next:(response) => {
          console.log(response);
          this.liked = false;
          this.fetchLikeStatus();

        },
        error:(error) => {
          console.error('Error unliking post:', error);
        }
    });
    } else {
      const like: Like = {
        id: 0, 
        userId: 1,
        postId: this.postDetail.id,
        creationDate: new Date().toISOString(),
      };
      console.log(like);
      //Like the post
      this.likeService.likePost(like).subscribe({
        next:(response) => {
          console.log(response);
          this.liked = true;
          this.fetchLikeStatus();
        },
        error:(error) =>{
          console.log('Error liking post',error);
        }
      });
    }
  }

  //send data to parent 
  deletePost(){
    this.deletePostId.emit(this.postDetail.id);
  }

  //send data to parent 
  editPost(){
    this.editPostId.emit(this.postDetail.id);
  }

  //comment section
  comments:Comment[] = [];
  @ViewChild('comment') commentSec : any;
  //show comment
  showComment(){
    this.commentSec.nativeElement.style.display = 'block';
  }

  closeComment(){
    this.commentSec.nativeElement.style.display = 'none';
  }

  fetchComments(){
    this.CommentService.getComments(this.postDetail.id).subscribe({
      next:(response) => {
        console.log(response);
        this.comments = response;
      },
      error:(error) => {
        console.error('Error fetching Comment status:', error);
      }
    });
  }

  isEdit = false;
  content:string = '';
  id:number=0;


  editComment(id){
    this.isEdit = true;
    const current = this.comments.find(c=>c.id==id.innerText);
    this.content = current.content;
    this.id=current.id;
  }

  addComment(){
    const comment = {
      id:this.id,
      userId:1,
      postId:this.postDetail.id,
      content:this.content,
      creationDate:new Date().toISOString(),
    }
    if(!this.isEdit){
      this.CommentService.addComment(comment).subscribe({
        next:(res)=>{
          console.log(res);
          this.content = '',
          this.fetchComments();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    else{
      this.CommentService.updateComment(comment).subscribe({
        next:(res)=>{
          console.log(res);
          this.content = '',
          this.fetchComments();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    
  }

  deleteComment(id){
    this.CommentService.deleteComment(id.innerText).subscribe({
      next:(res)=>{
        console.log(res);
        this.fetchComments();
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }
}
