import { Component, Input, OnInit } from '@angular/core';
import { User } from '../../../Models/user';

@Component({
  selector: 'app-my-connection-card',
  templateUrl: './my-connection-card.component.html',
  styleUrl: './my-connection-card.component.css',
})
export class MyConnectionCardComponent {
  @Input()
  user: User;
}
