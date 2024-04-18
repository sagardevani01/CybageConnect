import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../../../Models/user';
import { NetworkService } from '../../../Services/network.service';
import { Network } from '../../../Models/Network';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-send-connection-card',
  templateUrl: './send-connection-card.component.html',
  styleUrl: './send-connection-card.component.css',
})
export class SendConnectionCardComponent implements OnInit {
  @Input()
  user: User;
  defaultImage: string =
    'https://images.unsplash.com/photo-1494790108377-be9c29b29330?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D';
  @Output() sendConnectionRequestSuccess: EventEmitter<any> =
    new EventEmitter();
  constructor(
    private networkService: NetworkService,
    private toastr: ToastrService
  ) {}

  network: Network = {
    userId: 0,
    friendId: 0,
  };

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: any = jwtDecode(token);
      this.network.userId = parseInt(decodedToken.nameid);
    }
  }

  showSuccessToast(message: string) {
    this.toastr.success(message);
  }
  showErrorToast(message: string) {
    this.toastr.error(message);
  }

  sendConnectionRequest(receiverId: number): void {
    this.network.friendId = receiverId;
    this.networkService
      .sendConnectionRequest(this.network)
      .subscribe((response: boolean) => {
        if (response) {
          this.sendConnectionRequestSuccess.emit(true);
          this.showSuccessToast('Connection Request sent successfully');
        } else {
          this.showErrorToast('Connection Request failed');
        }
      });
  }
}
