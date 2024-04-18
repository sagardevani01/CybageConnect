import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { User } from '../../../../Models/user';
import { NetworkService } from '../../../../Services/network.service';
import { AuthService } from '../../../../Services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  pendingUsers: User[] = [];
  userId?: number;
  loggedin: boolean = false;
  @Output() updateLogin: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private networkService: NetworkService,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.decodeToken();
    this.getPendingUsers();
  }

  getPendingUsers(): void {
    this.networkService
      .getRequestedUsers(this.userId)
      .subscribe((users: User[]) => {
        this.pendingUsers = users;
      });
  }

  logout(): void {
    this.updateLogin.emit(false);
    this.toastr.error('You have logged out ');
    this.router.navigate(['/']);
    this.authService.logout();
  }
}
