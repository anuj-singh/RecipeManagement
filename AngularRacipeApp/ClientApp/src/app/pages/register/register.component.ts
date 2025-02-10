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
  
constructor(private router: Router, private dataService: DataService ){
  
}

ngOnInit(): void {
}

registerForm=new FormGroup({
  username:new FormControl('',[
      Validators.required,
      Validators.minLength(2),
      Validators.pattern("[a-zA-Z].*")]),
  email:new FormControl('',[Validators.required]),
  password:new FormControl('',[
    Validators.required,
    Validators.minLength(6),
    Validators.maxLength(15)
  ]),
})

registerSubmitted(){
 console.log(this.registerForm.value);
 this.dataService.httpPostRequest(this.router.navigate(['/main-dashboard']),this.registerForm.value).subscribe((response)=>{
 console.log(response);
})
 //this.dataService.httpPostRequest('',this.registerForm.value).subscribe((response)=>{
 // console.log(response);
//})
//this.router.navigate(['/main-dashboard']);
}

get UserName():FormControl{
  return this.registerForm.get("username")as FormControl;
}
get Email():FormControl{
  return this.registerForm.get("email")as FormControl;
}
get Password():FormControl{
  return this.registerForm.get("password")as FormControl;
}

navigateToLogin() {
  this.router.navigate(['/login']);
}

}
