import {
  Component,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { Post } from '../../../../Models/post';
import { LikeService } from '../../../../Services/like.service';
import { Like } from '../../../../Models/like';
import { FormControl, NgForm, Validators } from '@angular/forms';
import { CommentService } from '../../../../Services/comment.service';
import { Comment } from '../../../../Models/comment';
import { AuthService } from '../../../../Services/auth.service';
import { FormGroup, FormBuilder, Validator } from '@angular/forms';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrl: './post.component.css',
})
export class PostComponent {
  //get post data from parent
  @Input()
  postDetail: Post; //individual post
  liked: boolean; //status of like
  likes: number; //number of likes
  defaultImage =
    'https://flow-user-images.s3.us-west-1.amazonaws.com/prompt/Lb_wMe1KcCU_MpW8l-v0v/1699463160522';
  defaultProfile =
    'https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Windows_10_Default_Profile_Picture.svg/2048px-Windows_10_Default_Profile_Picture.svg.png';

  //send delete post id to parent
  @Output()
  deletePostId = new EventEmitter<number>();

  //send edit post id to parent
  @Output()
  editPostId = new EventEmitter<number>();

  //send data to parent
  deletePost() {
    this.deletePostId.emit(this.postDetail.id);
  }

  //send data to parent
  editPost() {
    this.editPostId.emit(this.postDetail.id);
  }

  constructor(
    private likeService: LikeService,
    private CommentService: CommentService,
    private authService: AuthService
  ) {}

  //read more and read less
  showFullContent: boolean = false;

  toggleContent() {
    this.showFullContent = !this.showFullContent;
  }

  //showing action button for login user
  showButton: boolean = false;
  showTheButton(authService) {
    if (Number(authService.decodeToken()) == this.postDetail.userId) {
      this.showButton = true;
    }
    return;
  }

  ngOnInit(): void {
    // Fetch initial like status for the post
    this.fetchLikes();
    this.getLikeStatus();
    this.fetchComments();
    this.showTheButton(this.authService);
  }

  //fetching all likes
  fetchLikes(): void {
    this.likeService.getLikesForPost(this.postDetail.id).subscribe({
      next: (response) => {
        console.log(response);
        this.likes = response;
      },
      error: (error) => {
        console.error('Error fetching like status:', error);
      },
    });
  }

  //fetch status of like for current user
  getLikeStatus(): void {
    this.likeService
      .getLike(this.postDetail.id, this.authService.decodeToken())
      .subscribe({
        next: (response) => {
          console.log(response);
          this.liked = response;
        },
        error: (error) => {
          console.error('Error fetching like status:', error);
        },
      });
  }

  //like and unlike
  toggleLike(): void {
    if (this.liked) {
      // Unlike the post
      this.likeService
        .unlikePost(this.postDetail.id, this.authService.decodeToken())
        .subscribe({
          next: (response) => {
            console.log(response);
            this.liked = false;
            this.fetchLikes();
          },
          error: (error) => {
            console.error('Error unliking post:', error);
          },
        });
    } else {
      //liking the post
      const like: Like = {
        id: 0,
        userId: this.authService.decodeToken(),
        postId: this.postDetail.id,
        creationDate: new Date().toISOString(),
      };
      this.likeService.likePost(like).subscribe({
        next: (response) => {
          console.log(response);
          this.liked = true;
          this.fetchLikes();
        },
        error: (error) => {
          console.log('Error liking post', error);
        },
      });
    }
  }

  //comment section
  comments: Comment[] = [];
  isEdit = false;
  content: any = ''; //for uploading comment
  id: number = 0;
  @ViewChild('comment') commentSec: any;
  buttonClicked: boolean = false; //fot comment validation

  //show comment
  showComment() {
    this.commentSec.nativeElement.style.display = 'grid';
  }

  closeComment() {
    this.commentSec.nativeElement.style.display = 'none';
    this.buttonClicked = false;
  }

  fetchComments() {
    this.CommentService.getComments(this.postDetail.id).subscribe({
      next: (response) => {
        console.log(response);
        this.comments = response;
      },
      error: (error) => {
        console.error('Error fetching Comment status:', error);
      },
    });
  }

  //to fill fields according to data
  editComment(id: number) {
    this.isEdit = true;
    const current = this.comments.find((c) => c.id == id);
    this.content = current.content;
    this.id = current.id;
  }

  //add or edit the comments
  addComment() {
    this.buttonClicked = true;
    console.log(this.content);
    if (!this.content || this.content.trim() == '') {
      console.log('inside the invalid');
      return;
    }
    const comment = {
      id: this.id,
      userId: this.authService.decodeToken(),
      postId: this.postDetail.id,
      content: this.content,
      creationDate: new Date().toISOString(),
    };
    if (!this.isEdit) {
      this.CommentService.addComment(comment).subscribe({
        next: (res) => {
          console.log(res);
          this.content = '';
          this.buttonClicked = false;
          this.fetchComments();
        },
        error: (err) => {
          console.log(err);
        },
      });
    } else {
      this.CommentService.updateComment(comment).subscribe({
        next: (res) => {
          console.log(res);
          (this.content = ''), (this.buttonClicked = false);
          this.fetchComments();
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  //delete comments
  async deleteComment(id: number) {
    const res = await Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    });
    if (res.isConfirmed) {
      this.CommentService.deleteComment(id).subscribe({
        next: (res) => {
          console.log(res);
          this.fetchComments();
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
}
