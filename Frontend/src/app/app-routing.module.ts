import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './Components/Authentication/landing-page/landing-page.component';
import { LoginPageComponent } from './Components/Authentication/login-page/login-page.component';
import { RegistrationPageComponent } from './Components/Authentication/registration-page/registration-page.component';
import { NetworkingComponent } from './Components/Networking/networking.component';
import { AuthGuard } from './Services/Guards/auth.guard';
import { PostListComponent } from './Components/Knowledge-Sharing/post-list/post-list.component';
import { ProfilePageComponent } from './Components/profile-page/profile-page.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'Login', component: LoginPageComponent },
  {
    path: 'Register',
    component: RegistrationPageComponent,
  },
  { path: 'Home', component: PostListComponent, canActivate: [AuthGuard] },
  { path: 'Network', component: NetworkingComponent, canActivate: [AuthGuard] },
  {
    path: 'MyProfile',
    component: ProfilePageComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
