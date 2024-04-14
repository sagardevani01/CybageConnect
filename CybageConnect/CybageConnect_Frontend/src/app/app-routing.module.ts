import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './Components/Authentication/landing-page/landing-page.component';
import { LoginPageComponent } from './Components/Authentication/login-page/login-page.component';
import { RegistrationPageComponent } from './Components/Authentication/registration-page/registration-page.component';
import { PostListComponent } from './Components/Knowledge-Sharing/post-list/post-list.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'Login', component: LoginPageComponent },
  { path: 'Register', component: RegistrationPageComponent },
  {path:'Post',component:PostListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
