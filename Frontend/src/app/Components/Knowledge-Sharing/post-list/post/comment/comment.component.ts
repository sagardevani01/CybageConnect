import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Comment } from '../../../../../Models/comment';
import { Input } from '@angular/core';
import { AuthService } from '../../../../../Services/auth.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.css'
})
export class CommentComponent implements OnInit {
  @Input()
  commentDetail:Comment;

  @Output()
  editCommentId = new EventEmitter<number>();

  @Output()
  deleteCommentId = new EventEmitter<number>();

  constructor(private authService:AuthService){}
  showActionButton:boolean = false;
  showTheActionButton(authService){
      if(Number(authService.decodeToken()) == this.commentDetail.userId){
        this.showActionButton = true;
      } 
  }
  ngOnInit():void{
    this.showTheActionButton(this.authService);
  }

  editComment(){
    this.editCommentId.emit(this.commentDetail.id);
  }

  deleteComment(){
    this.deleteCommentId.emit(this.commentDetail.id);
  }
}
