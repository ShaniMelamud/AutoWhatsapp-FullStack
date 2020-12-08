import { MatInputModule } from '@angular/material/input';
import { JwtInterceptorService } from './services/jwt-interceptor.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StartupComponent } from './components/startup/startup.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { QRComponent } from './components/qr/qr.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AutomaticResponseComponent } from './components/automatic-response/automatic-response.component';
import { MailingMessagesComponent } from './components/mailing-messages/mailing-messages.component';
import { RobotConstructionComponent } from './components/robot-construction/robot-construction.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { ReportsProductionComponent } from './components/reports-production/reports-production.component';
import { ProfileComponent } from './components/profile/profile.component';
import { MaterialModule } from './shared/material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SuccessComponent } from './components/success/success.component';
import { MatIconModule } from '@angular/material/icon';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LogoutComponent } from './components/logout/logout.component';
import { MatFormFieldModule } from '@angular/material/form-field';


@NgModule({
    declarations: [
        AppComponent,
        StartupComponent,
        LoginComponent,
        SignupComponent,
        QRComponent,
        NavbarComponent,
        DashboardComponent,
        AutomaticResponseComponent,
        MailingMessagesComponent,
        RobotConstructionComponent,
        ContactsComponent,
        ReportsProductionComponent,
        ProfileComponent,
        SuccessComponent,
        PageNotFoundComponent,
        LogoutComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule
    ],
    providers: [{
        provide: HTTP_INTERCEPTORS,
        useClass: JwtInterceptorService,
        multi: true
    }],
    bootstrap: [AppComponent]
})
export class AppModule { }
