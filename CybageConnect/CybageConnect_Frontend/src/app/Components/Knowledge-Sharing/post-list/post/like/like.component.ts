import { Component, OnInit } from '@angular/core';
import { LikeService } from '../../../../../Services/like.service';
import { Like } from '../../../../../Models/like';

@Component({
  selector: 'app-like',
  templateUrl: './like.component.html',
  styleUrl : './like.component.css'
})
export class LikeComponent implements OnInit {
  postId: number =2;
  liked: boolean;
  likes:number;
  like:Like = {
    userId :1,
    postId : 2,
    creationDate: "08/07/2032",
    id :1
  }

  constructor(private likeService: LikeService) { }

  ngOnInit(): void {
    // Fetch initial like status for the post
    this.fetchLikeStatus();
  }

  fetchLikeStatus(): void {
    this.likeService.getLikesForPost(this.postId).subscribe({
      next:(response) => {
        // Check if user has already liked the post
        console.log(response);
        this.likes = response.liked;
      },
      error:(error) => {
        console.error('Error fetching like status:', error);
      }
    });
  }

  // toggleLike(): void {
  //   if (this.liked) {
  //     // Unlike the post
  //     this.likeService.unlikePost(this.postId).subscribe({
  //       next:(liked) => {
  //         this.liked = false;
  //       },
  //       error:(error) => {
  //         console.error('Error unliking post:', error);
  //       }
  //   });
  //   } else {
  //     // Like the post
  //     this.likeService.likePost(this.like).subscribe(
  //       () => {
  //         this.liked = true;
  //       },
  //       (error) => {
  //         console.error('Error liking post:', error);
  //       }
  //     );
  //   }
  // }
}
