import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  title: string = 'Recipes Management';
  user: string = 'admin';
  displayStyleUser = "none"; 
  displayStyleUserUpdate = "none"; 
  userModelOpened: boolean= false;
  userUpdateModelOpened: boolean= false;

  sideMenu: boolean = true;
  userUpdateForm!: FormGroup;
  @Output() sideNavToggled = new EventEmitter();
  

  constructor(
    private router: Router,
    private fb: FormBuilder
  ) {}
  ngOnInit(): void {
    this.userUpdateForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  sideNavToggle() {
    this.sideMenu = !this.sideMenu;
    this.sideNavToggled.emit(this.sideMenu);
  }
  
  togglePopup() { 
    if(!this.userModelOpened === true){
      this.displayStyleUser = "block";
    } else {
      this.displayStyleUser = "none"; 
    }
    this.userModelOpened = !this.userModelOpened;
  }  
  openUpdateProfile(){
    this.displayStyleUser = "none"; 
    if(!this.userUpdateModelOpened === true){
      this.displayStyleUserUpdate = "block";
    } else {
      this.userUpdateForm.reset();
      this.displayStyleUserUpdate = "none"; 
    }
    this.userUpdateModelOpened = !this.userUpdateModelOpened;
  }
  signout(){
    this.togglePopup();
    localStorage.removeItem("tokenKey");
    this.router.navigate(['/login']);
  }
  onSubmit(){
    if (this.userUpdateForm.valid) {
      this.displayStyleUserUpdate = "none"; 
      console.log('Form Submitted!', this.userUpdateForm.value);
      this.userUpdateForm.reset();
      // You can add your form submission logic here
    } else {
      console.log('Form is invalid');
    }
  }

}

