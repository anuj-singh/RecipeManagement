import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  
  
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(private router: Router, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  onSubmitLogin() {
   console.log(this.loginForm.value);
   this.router.navigate(['/main-dashboard']);
  }

  navigateToRegister() {
    this.router.navigate(['/register']);
  }
}
