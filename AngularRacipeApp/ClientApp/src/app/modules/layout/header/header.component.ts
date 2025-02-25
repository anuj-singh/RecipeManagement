import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  userDetails: any = sessionStorage.getItem('tokenKey');
  loggedInUser: any;
  title: string = 'Recipes Management';
  displayStyleUser = 'none';
  displayStyleUserUpdate = 'none';
  userModelOpened: boolean = false;
  userUpdateModelOpened: boolean = false;
  userLogo = "https://th.bing.com/th/id/OIP.EFchTjQLTexrr4eFgDAruwHaHa?rs=1&pid=ImgDetMain";

  sideMenu: boolean = true;
  userUpdateForm!: FormGroup;
  @Output() sideNavToggled = new EventEmitter();

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private dataService: DataService
  ) {}
  ngOnInit(): void {
    if (this.userDetails) {
      this.loggedInUser = JSON.parse(this.userDetails);
      
    }

    this.userUpdateForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      bio: [''],
    });
  }

  sideNavToggle() {
    this.sideMenu = !this.sideMenu;
    this.sideNavToggled.emit(this.sideMenu);
  }
  openUpdateProfile() {
    this.displayStyleUser = 'none';
    if (!this.userUpdateModelOpened === true) {
      this.populateUserData();
      this.displayStyleUserUpdate = 'block';
    } else {
      this.userUpdateForm.reset();
      this.displayStyleUserUpdate = 'none';
    }
    this.userUpdateModelOpened = !this.userUpdateModelOpened;
  }
  populateUserData(){
    this.userUpdateForm.patchValue({
      username: this.loggedInUser.userName,
      email: this.loggedInUser.email,
      bio: this.loggedInUser.bio,
    });
  }
  signout() {
    this.dataService.signOut();
    this.toggleDropdown();
  }

  onSubmit() {
    if (this.userUpdateForm.valid) {
      this.displayStyleUserUpdate = 'none';
      const payload = {
        userId: this.loggedInUser.userId,
        userName: this.userUpdateForm.value['username'],
        email: this.userUpdateForm.value['email'],
        bio: this.userUpdateForm.value['bio'],
        statusId:this.loggedInUser.userId,
        imageUrl:'',
      }
      console.log(payload);
      this.dataService.httpUpdateRequest(`User/UpdateUser?id=${this.loggedInUser.userId}`, payload).subscribe((value:any)=> {
          this.loggedInUser.userName = value.userName;
          this.loggedInUser.mail = value.mail;
          console.log('pre session', this.loggedInUser);
          sessionStorage.setItem('tokenKey', JSON.stringify(this.loggedInUser));
      });
    } else {
      console.log('Form is invalid');
    }
  }
  toggleDropdown() {
    if (!this.userModelOpened === true) {
      this.displayStyleUser = 'block';
    } else {
      this.displayStyleUser = 'none';
    }
    this.userModelOpened = !this.userModelOpened;
  }
}
