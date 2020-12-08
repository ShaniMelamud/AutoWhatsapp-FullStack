import { LoginGuardService } from './services/login-guard.service';
import { LogoutComponent } from './components/logout/logout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AutomaticResponseComponent } from './components/automatic-response/automatic-response.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { MailingMessagesComponent } from './components/mailing-messages/mailing-messages.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProfileComponent } from './components/profile/profile.component';
import { QRComponent } from './components/qr/qr.component';
import { ReportsProductionComponent } from './components/reports-production/reports-production.component';
import { RobotConstructionComponent } from './components/robot-construction/robot-construction.component';
import { SignupComponent } from './components/signup/signup.component';
import { StartupComponent } from './components/startup/startup.component';
import { SuccessComponent } from './components/success/success.component';

const routes: Routes = [
    { path: 'startup', component: StartupComponent },
    { path: 'login', component: LoginComponent },
    { path: 'login/signup', component: SignupComponent },
    { path: 'qr-code', component: QRComponent },
    {
        path: 'dashboard',
        component: DashboardComponent,
        children: [
            { path: 'automatic-response', canActivate: [LoginGuardService], component: AutomaticResponseComponent },
            { path: 'logout', canActivate: [LoginGuardService], component: LogoutComponent },
            { path: 'mailing-messages', canActivate: [LoginGuardService], component: MailingMessagesComponent },
            { path: 'robot-construction', canActivate: [LoginGuardService], component: RobotConstructionComponent },
            { path: 'chat-bot', canActivate: [LoginGuardService], component: SuccessComponent },
            { path: 'contacts', canActivate: [LoginGuardService], component: ContactsComponent },
            { path: 'reports-production', canActivate: [LoginGuardService], component: ReportsProductionComponent },
            { path: 'profile', canActivate: [LoginGuardService], component: ProfileComponent },
            { path: '', redirectTo: 'automatic-response', pathMatch: 'full' },
        ],
    },
    { path: '', redirectTo: 'startup', pathMatch: 'full' },
    { path: "**", component: PageNotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }
