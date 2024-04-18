import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../Services/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
})
export class LandingPageComponent implements OnInit {
  constructor(private AuthService: AuthService, private router: Router) {}
  ngOnInit(): void {
    if (this.AuthService.isLoggedIn()) {
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
          this.AuthService.logout();
          this.router.navigate(['/']);
        }
      });
    }
  }
}
