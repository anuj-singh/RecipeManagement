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
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  sideNavToggle() {
    this.sideMenu = !this.sideMenu;
    this.sideNavToggled.emit(this.sideMenu);
  }
  openUpdateProfile() {
    this.displayStyleUser = 'none';
    if (!this.userUpdateModelOpened === true) {
      this.displayStyleUserUpdate = 'block';
    } else {
      this.userUpdateForm.reset();
      this.displayStyleUserUpdate = 'none';
    }
    this.userUpdateModelOpened = !this.userUpdateModelOpened;
  }
  signout() {
    this.dataService.signOut();
    this.toggleDropdown();
  }

  onSubmit() {
    if (this.userUpdateForm.valid) {
      this.displayStyleUserUpdate = 'none';
      console.log('Form Submitted!', this.userUpdateForm.value);
      this.userUpdateForm.reset();
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
