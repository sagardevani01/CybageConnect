import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { LoginPageComponent } from './Components/Authentication/login-page/login-page.component';
import { RegistrationPageComponent } from './Components/Authentication/registration-page/registration-page.component';
import { HeaderComponent } from './Components/Authentication/SharedComponents/header/header.component';
import { FooterComponent } from './Components/Authentication/SharedComponents/footer/footer.component';
import { PendingConnectionCardComponent } from './Components/Networking/pending-connection-card/pending-connection-card.component';
import { SendConnectionCardComponent } from './Components/Networking/send-connection-card/send-connection-card.component';
import { MyConnectionCardComponent } from './Components/Networking/my-connection-card/my-connection-card.component';
import { MatIconModule } from '@angular/material/icon';
import { NetworkingComponent } from './Components/Networking/networking.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PostListComponent } from './Components/Knowledge-Sharing/post-list/post-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PostComponent } from './Components/Knowledge-Sharing/post-list/post/post.component';
import { LandingPageComponent } from './Components/Authentication/landing-page/landing-page.component';
import { ProfilePageComponent } from './Components/profile-page/profile-page.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommentComponent } from './Components/Knowledge-Sharing/post-list/post/comment/comment.component';
import { MatCardModule } from '@angular/material/card';


@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    RegistrationPageComponent,
    LandingPageComponent,
    HeaderComponent,
    FooterComponent,
    PendingConnectionCardComponent,
    SendConnectionCardComponent,
    MyConnectionCardComponent,
    NetworkingComponent,
    PostListComponent,
    PostComponent,
    ProfilePageComponent,
    CommentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatIconModule,
    MatSnackBarModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatCardModule,
    ToastrModule.forRoot(),
  ],
  providers: [
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
