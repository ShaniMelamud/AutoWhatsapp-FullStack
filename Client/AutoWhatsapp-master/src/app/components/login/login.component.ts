import { AuthService } from './../../services/auth.service';
import { CredentialModel } from './../../models/credential.model';
import { Component, OnInit } from '@angular/core';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    FormGroupDirective,
    NgForm,
    Validators,
} from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    public credentials = new CredentialModel();

    constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }

    ngOnInit(): void {
        // this.loginForm = this.fb.group({
        //     email: ['', Validators.required, Validators.email],
        //     password: ['', Validators.required],
        // });
    }
    public async login() {
        const success = await this.auth.login(this.credentials);
        if (success) {
            this.router.navigateByUrl("");
         
        }
        else{
            alert("User name or password is wrong.")
        }
    }
}
