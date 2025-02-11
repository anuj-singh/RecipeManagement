import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  title: string = "Recipes Management"
  user: string = "admin"
  displayStyleUser = "none"; 
  displayStyleUserUpdate = "none"; 
  userModelOpened: boolean= false;
  userUpdateModelOpened: boolean= false;

  constructor(){

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
    this.togglePopup();
    if(!this.userUpdateModelOpened === true){
      this.displayStyleUserUpdate = "block";
    } else {
      this.displayStyleUserUpdate = "none"; 
    }
    this.userUpdateModelOpened = !this.userUpdateModelOpened;
  }
}
