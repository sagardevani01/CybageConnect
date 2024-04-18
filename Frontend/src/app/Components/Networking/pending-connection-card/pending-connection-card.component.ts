import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../../../Models/user';
import { Network } from '../../../Models/Network';
import { NetworkService } from '../../../Services/network.service';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-pending-connection-card',
  templateUrl: './pending-connection-card.component.html',
  styleUrls: ['./pending-connection-card.component.css'], // Corrected typo in styleUrls
})
export class PendingConnectionCardComponent implements OnInit {
  @Input() user: User;
  network: Network = {
    userId: 0,
    friendId: 0,
  };
  defaultImage: string =
    'https://images.unsplash.com/photo-1494790108377-be9c29b29330?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D';
  @Output() acceptOrRejectRequest: EventEmitter<boolean> =
    new EventEmitter<boolean>();

  constructor(
    private networkService: NetworkService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      this.network.friendId = parseInt(decodedToken.nameid);
    }
  }

  acceptConnection(receiverId: number): void {
    this.network.userId = receiverId;
    this.networkService
      .acceptConnectionRequest(this.network)
      .subscribe((response: boolean) => {
        if (response) {
          this.acceptOrRejectRequest.emit(true);
          this.showSuccessToast('Connection request accepted successfully.');
        } else {
          this.showErrorToast('Failed to accept connection request.');
        }
      });
  }

  declineConnection(receiverId: number): void {
    this.network.userId = receiverId;
    this.networkService
      .declineConnectionRequest(this.network)
      .subscribe((response: boolean) => {
        if (response) {
          this.acceptOrRejectRequest.emit(true);
          this.showSuccessToast('Connection request declined successfully.');
        } else {
          this.showErrorToast('Failed to decline connection request.');
        }
      });
  }

  showSuccessToast(message: string) {
    this.toastr.success(message);
  }
  showErrorToast(message: string) {
    this.toastr.error(message);
  }
}
