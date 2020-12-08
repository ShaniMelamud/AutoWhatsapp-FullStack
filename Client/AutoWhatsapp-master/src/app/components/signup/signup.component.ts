import { AuthService } from './../../services/auth.service';
import { BusinessModel } from './../../models/business-model';
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
import { store } from 'src/app/redux/store';
export class MyErrorStateMatcher implements ErrorStateMatcher {

    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        const isSubmitted = form && form.submitted;
        return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
    }
}
@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
    public signupForm: FormGroup;
    public business = new BusinessModel();
    public passwordToConfirm: string;
    public passwordVerify: boolean = true;

    constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }


    ngOnInit(): void {
        // this.signupForm = this.fb.group({
        //     businessName: ['', Validators.required],
        //     businessLine: ['', Validators.required],
        //     phoneNumber: ['', Validators.required],
        //     bussinessOwnerName: ['', Validators.required],
        //     email: ['', Validators.required, Validators.email],
        //     password: ['', Validators.required],
        //     confirmPassword: ['', Validators.required],
        // });
    }
    // get businessName() { return this.signupForm.get('businessName'); }
    // get businessLine() { return this.signupForm.get('businessLine'); }
    // get phoneNumber() { return this.signupForm.get('phoneNumber'); }
    // get bussinessOwnerName() { return this.signupForm.get('bussinessOwnerName'); }
    // get email() { return this.signupForm.get('email'); }
    // get password() { return this.signupForm.get('password'); }
    // get confirmPassword() { return this.signupForm.get('confirmPassword'); }

    public async register() {
        if (this.ConfirmPasswordCheck()) {

            this.passwordVerify = true;
            this.business.role = 'user';
            const success = await this.auth.register(this.business);
            if (success) {
                this.router.navigateByUrl("/dashboard");
            }
        }
        else {
            this.passwordVerify = false;
        }
    }
    public ConfirmPasswordCheck(): boolean {

        return this.business.password === this.passwordToConfirm ? true : false;

    }
    
}
