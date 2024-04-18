import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';

import { NetworkService } from '../../Services/network.service';
import { User } from '../../Models/user';

import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-networking',
  templateUrl: './networking.component.html',
  styleUrl: './networking.component.css',
})
export class NetworkingComponent implements OnInit, OnChanges {
  filteredPendingUsers: User[] = [];
  filteredUnconnectedUsers: User[] = [];
  filteredConnectedUsers: User[] = [];
  pendingUsers: User[] = [];
  connectedUsers: User[] = [];
  unconnectedUsers: User[] = [];
  userId?: number;
  searchQuery: string = '';

  constructor(
    private networkService: NetworkService,
    private authService: AuthService
  ) {}
  ngOnChanges(searchQuery): void {
    this.onInputSearch(searchQuery);
  }
  ngOnInit(): void {
    this.userId = this.authService.decodeToken();
    this.getPendingUsers();
    this.getConnectedUsers();
    this.getUnconnectedUsers();
  }

  getPendingUsers(): void {
    this.networkService
      .getRequestedUsers(this.userId)
      .subscribe((users: User[]) => {
        this.pendingUsers = users;
      });
  }

  getConnectedUsers(): void {
    this.networkService
      .getConnectedUsers(this.userId)
      .subscribe((users: User[]) => {
        this.connectedUsers = users;
      });
  }

  getUnconnectedUsers(): void {
    this.networkService
      .getUnconnectedUsers(this.userId)
      .subscribe((users: User[]) => {
        this.unconnectedUsers = users;
      });
  }
  onInputSearch(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const query = inputElement.value;

    if (query != '') {
      this.filteredConnectedUsers = this.connectedUsers.filter((user) =>
        user.firstName.toLowerCase().startsWith(query.toLowerCase())
      );

      this.filteredUnconnectedUsers = this.unconnectedUsers.filter((user) =>
        user.firstName.toLowerCase().startsWith(query.toLowerCase())
      );
      this.filteredPendingUsers = this.pendingUsers.filter((user) =>
        user.firstName.toLowerCase().startsWith(query.toLowerCase())
      );
    } else {
      this.filteredConnectedUsers = [];
      this.filteredUnconnectedUsers = [];
      this.filteredPendingUsers = [];
    }
  }

  receiveMessage(message: boolean) {
    this.getUnconnectedUsers();
  }
  acceptorreject(message: boolean) {
    this.getPendingUsers();
    this.getConnectedUsers();
    this.getUnconnectedUsers();
  }
}
