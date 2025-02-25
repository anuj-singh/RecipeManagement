import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  roleList: any[] = [];
  constructor(private router: Router, private dataService: DataService) {}

  ngOnInit(): void {
    this.getAllRoles();
  }

  registerForm = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.pattern('[a-zA-Z].*'),
    ]),
    email: new FormControl('', [Validators.required]),
    roleId: new FormControl('', [Validators.required]),
    securityQuestion: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.pattern('[a-zA-Z].*'),
    ]),
    securityAnswer: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.pattern('[a-zA-Z].*'),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(15),
    ]),
  });

  registerSubmitted() {
    const registerObj = {
      roleId: this.registerForm['controls'].roleId.value,
      loginDto: {
        userName: this.registerForm['controls'].username.value,
        email: this.registerForm['controls'].email.value,
        password: this.registerForm['controls'].password.value,
      },
      securityQuestion:  this.registerForm['controls'].securityQuestion.value,
      securityAnswer:  this.registerForm['controls'].securityAnswer.value,
    };

    this.dataService
      .httpPostRequest('Auth/Register', registerObj)
      .subscribe((res: any) => {
        alert(res.message);
        this.router.navigate(['/users']);
      });
  }

  get UserName(): FormControl {
    return this.registerForm.get('username') as FormControl;
  }
  get Email(): FormControl {
    return this.registerForm.get('email') as FormControl;
  }

  get roleId(): FormControl {
    return this.registerForm.get('roleId') as FormControl;
  }

  get securityQuestion(): FormControl {
    return this.registerForm.get('securityQuestion') as FormControl;
  }

  get securityAnswer(): FormControl {
    return this.registerForm.get('securityAnswer') as FormControl;
  }

  get Password(): FormControl {
    return this.registerForm.get('password') as FormControl;
  }

  getAllRoles() {
    this.dataService
      .httpGetRequest('User/GetAllRoles')
      .subscribe((res: any) => {
        this.roleList = res;
      });
  }
  navigateToLogin() {
    this.router.navigate(['/users/']);
  }
}
