import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../Services/auth.service';
import { LocalStorageService } from '../../../Services/local-storage.service';
import { ToastrService } from 'ngx-toastr';
import { Login } from '../../../Models/login';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css',
})
export class LoginPageComponent implements OnInit {
  errorMessage: string = '';
  login: Login = {
    Username: '',
    Password: '',
  };

  constructor(
    private router: Router,
    private authService: AuthService,
    private localStorageService: LocalStorageService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/Home']);
      Swal.fire({
        title: 'Are you sure?',
        text: 'You want to Log Out!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Log Out',
      }).then((result) => {
        if (result.isConfirmed) {
          this.authService.logout();
          this.router.navigate(['/Login']);
        }
      });
    }
  }

  navigateToRegister() {
    this.router.navigate(['/Register']);
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.errorMessage = '';

      this.authService.logIn(this.login).subscribe({
        next: (res) => {
          this.localStorageService.setToken(res.token);
          this.toastr.success('You have logged in successfully');
          this.router.navigate(['/Home']);
        },
        error: (err) => {
          this.errorMessage = err.error;
        },
      });
    }
  }
}
