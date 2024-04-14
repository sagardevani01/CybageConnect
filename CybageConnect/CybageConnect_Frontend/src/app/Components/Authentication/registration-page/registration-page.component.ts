import { Component } from '@angular/core';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrl: './registration-page.component.css'
})
export class RegistrationPageComponent {
  details = {
    FirstName: '',
    LastName: '',
    Email: '',
    UserName: '',
    Phone: '',
    Department: '',
    Designation: '',
    Password: '',
    ConfirmPassword: '',
    Location: '',
    ProfilePhoto: ''
  };

  submitForm(form: any):void{
    if(form.valid){
      console.log(this.details);
    }
  }

  currentPage: number = 1;

  showPage(page: number){
    this.currentPage = page;
  }

  previousPage(){
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  nextPage(){
    if (this.currentPage < 3) {
      this.currentPage++;
    }
  }

}
