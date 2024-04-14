import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../../Services/login';
import { createLanguageService } from 'typescript';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  constructor(private router: Router ,private LoginService: LoginService) {}

  navigateToRegister() {
    this.router.navigate(['/Register']);
  }
  login(username: string, password: string) {
    console.log(username);
    const isAuthenticated = this.LoginService.login(username, password);
    if (isAuthenticated) {
      // Redirect or perform other actions upon successful login
      console.log('Login successful');
    } else {
      // Handle failed login attempt
      console.log('Invalid username or password');
    }
  }
}


