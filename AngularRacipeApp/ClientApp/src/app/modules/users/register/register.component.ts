import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  constructor(
    private router: Router,
    private dataService: DataService
  ) { }

  ngOnInit(): void {
  }



  registerForm = new FormGroup({
    userName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.pattern("[a-zA-Z].*")
    ]
    ),
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(15)
    ]
    ),
  })

  registerSubmitted() {

    let registerUserObj = {
      loginDto: {
        userName: this.registerForm.controls['userName'].value,
        email: this.registerForm.controls['email'].value,
        password: this.registerForm.controls['password'].value
      },
      securityQuestion: "nickname",
      securityAnswer: "dharitri"
    }
    //console.log(JSON.stringify(registerUserObj, null , 2));
    this.dataService.httpPostRequest('Auth/Register', registerUserObj)
      .subscribe((res) => {
        console.log(res);
        this.router.navigate(['/users/login'])
      });
  }

  get UserName(): FormControl {
    return this.registerForm.get("userName") as FormControl;
  }
  get Email(): FormControl {
    return this.registerForm.get("email") as FormControl;
  }
  get Password(): FormControl {
    return this.registerForm.get("password") as FormControl;
  }

  navigateToLogin() {
    this.router.navigate(['/users/']);
  }



}


