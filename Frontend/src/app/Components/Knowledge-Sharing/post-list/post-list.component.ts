import { Component, OnInit, ViewChild } from '@angular/core';
import { Post } from '../../../Models/post';
import { PostService } from '../../../Services/post.service';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../Services/auth.service';
import Swal from 'sweetalert2';
import { ProfileServiceService } from '../../../Services/profile-service.service';
import { User } from '../../../Models/user';
import { NetworkService } from '../../../Services/network.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrl: './post-list.component.css',
})
export class PostListComponent implements OnInit {
  //filtering data
  user:User;
  blog: boolean = false;
  article: boolean = false;
  insights: boolean = false;
  filteredPosts: Post[] = [];
  posts: Post[] = []; //Array Of Posts
  loading: boolean = false; //false for no posts
  fileData: string = ''; //image url from server
  defaultImage: 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Windows_10_Default_Profile_Picture.svg/2048px-Windows_10_Default_Profile_Picture.svg.png';
  //collecting data from form
  formData: any = {
    category: 0,
    imageUrl: '',
    content: '',
  };

  constructor(
    private postService: PostService,
    private authService: AuthService,
    private profileService: ProfileServiceService,
    private networkService: NetworkService
  ) {}

  ngOnInit(): void {
    this.fetchPosts(); // fetching all posts on init
    this.userProfile();
    this.getConnections();
  }

  userProfile() {
    this.profileService
      .getUserProfile(this.authService.decodeToken())
      .subscribe((res) => {
        this.user = res;
      });
  }

  //filtering according to checkbox
  filterPost() {
    const selectedCategories = [];

    if (this.blog) selectedCategories.push('Blog');
    if (this.article) selectedCategories.push('Article');
    if (this.insights) selectedCategories.push('ProjectInsight');

    if (selectedCategories.length === 0) {
      this.filteredPosts = this.posts;
    } else {
      this.filteredPosts = this.posts.filter((post) =>
        selectedCategories.includes(post.categoryName)
      );
    }
    console.log(selectedCategories);
    console.log(this.filteredPosts);
  }

  //Fetching all post
  fetchPosts(): void {
    this.loading = true;
    this.postService.getPosts().subscribe({
      next: (posts: Post[]) => {
        this.posts = posts;
        this.filterPost();
        this.loading = false;
      },
      error: (error: any) => {
        console.error('Error fetching posts:', error);
        this.loading = false;
      },
    });
  }

  //for adding post modal
  @ViewChild('myModal') myModal: any; // Reference to the modal element

  //open the post modal
  openModal() {
    this.myModal.nativeElement.style.display = 'block';
  }

  //close the post modal
  closeModal(form) {
    this.myModal.nativeElement.style.display = 'none';
    // this.formData = {}; //clear modal when it's close
    form.resetForm();
  }

  //update modal category according to button
  updateValue(value) {
    this.formData.category = value;
    this.openModal();
  }

  //checking for add or delete operation
  isEdit = false;
  editPost(data: number) {
    this.isEdit = true;
    this.openModal();
    const current = this.posts.find((p) => p.id == data);
    this.formData.id = current.id;
    this.formData.content = current.content;
    this.formData.imageUrl = current.imageUrl;
    this.formData.category = current.categoryId;
  }

  //for collecting post image
  async onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (
      !(file.type.startsWith('image/png') || file.type.startsWith('image/jpeg'))
    ) {
      this.fileData = '';
      return;
    }
    let image = await this.postService.fileUpload(file);
    this.fileData = image;
  }

  //add or delete post on modal submitting
  onSubmit(form: NgForm) {
    if (form.invalid) {
      form.form.markAllAsTouched();
      return;
    }
    console.log('Form data:', this.formData, this.fileData);
    const post: Post = {
      id: this.formData.id,
      content: this.formData.content,
      categoryId: this.formData.category,
      imageUrl: this.fileData,
      userId: this.authService.decodeToken(),
      creationDate: new Date().toISOString(),
    };

    if (!this.isEdit) {
      this.postService.createPost(post).subscribe({
        next: (res) => {
          this.fetchPosts();
          this.closeModal(form);
        },
        error: (error: any) => {
          console.error('Error fetching posts:', error);
        },
      });
    } else {
      console.log(post);
      this.postService.updatePost(post).subscribe({
        next: (res) => {
          this.fetchPosts();
          this.closeModal(form);
        },
        error: (error: any) => {
          console.error('Error fetching posts:', error);
        },
      });
    }
  }

  //for confirmation of delete
  async deletePost(data: number) {
    const res = await Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    });
    //delete the post
    if (res.isConfirmed) {
      this.postService.deletePost(data).subscribe({
        next: (res) => {
          console.log(res);
          this.fetchPosts();
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  connections : number
  getConnections() {
    this.networkService.getConnectedUsers(this.user.id).subscribe({
      next: (res) => (this.connections = res.length),
      error: (err) => console.log(err),
    });
}
}

