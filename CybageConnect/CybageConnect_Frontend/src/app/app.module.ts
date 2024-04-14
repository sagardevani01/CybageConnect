import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { LoginPageComponent } from './Components/Authentication/login-page/login-page.component';
import { RegistrationPageComponent } from './Components/Authentication/registration-page/registration-page.component';
import { LandingPageComponent } from './Components/Authentication/landing-page/landing-page.component';
import { HeaderComponent } from './Components/Authentication/SharedComponents/header/header.component';
import { FooterComponent } from './Components/Authentication/SharedComponents/footer/footer.component';
import { PostListComponent } from './Components/Knowledge-Sharing/post-list/post-list.component';
import { PostComponent } from './Components/Knowledge-Sharing/post-list/post/post.component';
import { CommentComponent } from './Components/Knowledge-Sharing/post-list/post/comment/comment.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    RegistrationPageComponent,
    LandingPageComponent,
    HeaderComponent,
    FooterComponent,
    PostListComponent,
    PostComponent,
    CommentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
