import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'CybageConnectFrontend';
  loggedin: boolean = false;
  constructor(private authService: AuthService) {}
  ngOnInit(): void {
    this.loggedin = this.authService.isLoggedIn();
  }
  updateLogin(message: boolean) {
    this.loggedin = message;
  }
}
