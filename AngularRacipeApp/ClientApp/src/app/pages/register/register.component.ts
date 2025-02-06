import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  confirmpassword:string='none';
constructor(){}

ngOnInit(): void {
}

registerForm=new FormGroup({
  email:new FormControl('',[Validators.required,Validators.email]),
  createpassword:new FormControl('',[
    Validators.required,
    Validators.minLength(6),
    Validators.maxLength(15)
  ]),
  confirmpassword:new FormControl(""),
})

registerSubmitted(){
  if(this.CreatePassword.value==this.ConfirmPassword.value)
  {
    console.log("Submitted");
    this.confirmpassword='none'
  }else{
    this.confirmpassword='inline'
  }
 // console.log(this.registerForm.get("email"));
}

get Email():FormControl{
  return this.registerForm.get("email")as FormControl;
}
get CreatePassword():FormControl{
  return this.registerForm.get("createpassword")as FormControl;
}
get ConfirmPassword():FormControl{
  return this.registerForm.get("confirmpassword")as FormControl;
}

}
