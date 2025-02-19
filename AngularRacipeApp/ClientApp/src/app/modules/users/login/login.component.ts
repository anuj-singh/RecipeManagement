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
      let user: any = {
        id: 1,
        username: 'User',
        bio: 'Just a regular user enjoying life.',
        pic: 'https://th.bing.com/th/id/OIP.EFchTjQLTexrr4eFgDAruwHaHa?rs=1&pid=ImgDetMain',
        token: 'user8584894jdfkifjfjgkdkdjo56840fskckifabc123xyz',
        userType: 'user',
      };

      let admin: any = {
        id: 2,
        username: 'Admin',
        bio: 'Just a regular admin user enjoying life.',
        pic: 'https://th.bing.com/th/id/OIP.EFchTjQLTexrr4eFgDAruwHaHa?rs=1&pid=ImgDetMain',
        token: 'admindkefooejfi45838274038vjdjfogfj487595',
        userType: 'admin',
      };

      if (this.loginForm.controls['email'].value == 'user@gmail.com') {
        sessionStorage.setItem('tokenKey', JSON.stringify(user));
        this.router.navigate(['/main-dashboard']);
      } else {
        sessionStorage.setItem('tokenKey', JSON.stringify(admin));
        this.router.navigate(['/main-dashboard']);
      }
      // console.log(this.loginForm.value);
      // this.dataService
      //   .httpPostRequest('', this.loginForm.value)
      //   .subscribe((res) => {
      //     console.log(res);
      //   });
    } else {
      alert('Username or password is invalid');
    }
  }

  navigateToRegister() {
    this.router.navigate(['/users/register']);
  }

  navigateToForgotpassword() {
    this.router.navigate(['/users/forgot-password']);
  }
}
