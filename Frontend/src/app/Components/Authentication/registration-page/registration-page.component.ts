import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../../Services/auth.service';
import { User } from '../../../Models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css'],
})
export class RegistrationPageComponent {
  registrationForm: FormGroup;
  emailError: string = '';
  phoneError: string = '';
  usernameError: string = '';
  profileImage: string = '';

  constructor(
    private formBuilder: FormBuilder,
    public authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registrationForm = this.formBuilder.group(
      {
        firstname: [
          '',
          [Validators.required, Validators.pattern('[a-zA-Z ]*')],
        ],
        lastname: ['', [Validators.required, Validators.pattern('[a-zA-Z ]*')]],
        email: [
          '',
          [
            Validators.required,
            Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
          ],
        ],
        phone: [
          '',
          [
            Validators.required,
            Validators.pattern(/^\d{10}$/),
            Validators.maxLength(10),
          ],
        ],
        username: [
          '',
          [
            Validators.required,
            Validators.pattern('^[a-zA-Z0-9_-]{6,16}$'),
            Validators.minLength(6),
            Validators.maxLength(16),
          ],
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.pattern(
              /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
            ),
            Validators.minLength(8),
          ],
        ],
        cpassword: ['', Validators.required],
        designation: ['', Validators.required],
        department: ['', Validators.required],
        location: ['', Validators.required],
        profile: ['', Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(group: FormGroup): { [key: string]: any } | null {
    const password = group.get('password');
    const cpassword = group.get('cpassword');
    return password && cpassword && password.value !== cpassword.value
      ? { passwordMismatch: true }
      : null;
  }

  submitForm(): void {
    if (this.registrationForm.valid) {
      const registrationData = this.registrationForm.value;
      const user: User = {
        firstName: registrationData.firstname,
        lastName: registrationData.lastname,
        email: registrationData.email,
        phone: registrationData.phone,
        username: registrationData.username,
        password: registrationData.password,
        designation: registrationData.designation,
        departament: registrationData.department,
        profilePicture: this.profileImage,
        location: registrationData.location,
      };

      this.authService.registerUser(user).subscribe({
        next: (res) => {
          console.log(res.message);
          this.router.navigate(['/Login']);
        },
        error: (err) => {
          console.log(err.error);
          const errList = err.error;
          if (errList.emailError) {
            this.emailError = errList.emailError;
          }
          if (errList.phoneError) {
            this.phoneError = errList.phoneError;
          }
          if (errList.usernameError) {
            this.usernameError = errList.usernameError;
          }
        },
      });
    } else {
      // Mark all form controls as touched to trigger validation messages
      this.markFormGroupTouched(this.registrationForm);
      console.error('Form is invalid');
    }
  }

  markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach((control) => {
      if (control instanceof FormGroup) {
        //control is itself another FormGroup
        this.markFormGroupTouched(control);
      } else {
        //For the controls we call markAsToched().
        control.markAsTouched();
      }
    });
  }

  async onFileSelected(event: any) {
    const file: File = event.target.files[0];
    console.log(file);
    const formData: FormData = new FormData();
    console.log(formData);
    formData.append('file', file, file.name);
    var profileUrl = await this.authService.uploadProfilePhoto(formData);
    this.profileImage = profileUrl;
  }

  resetEmailError(): void {
    this.emailError = '';
  }

  resetPhoneError(): void {
    this.phoneError = '';
  }

  resetUsernameError(): void {
    this.usernameError = '';
  }
}
