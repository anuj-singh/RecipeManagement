import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private dataService: DataService
  ) {}

  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  onSubmitLogin() {
    if (
      (this.loginForm.value['email'] == 'admin@gmail.com' &&
        this.loginForm.value['password'] == 'admin123') ||
      (this.loginForm.value['email'] == 'user@gmail.com' &&
        this.loginForm.value['password'] == 'user123')
    ) {
      this.router.navigate(['/main-dashboard']);
      console.log(this.loginForm.value);
      this.dataService
        .httpPostRequest('', this.loginForm.value)
        .subscribe((res) => {
          console.log(res);
        });
    } else {
      alert('Username or password is invalid');
    }
  }

  navigateToRegister() {
    this.router.navigate(['/users/register']);
  }

  navigateToForgotpassword(){
    this.router.navigate(['/users/forgot-password']);
  }

  
}
