import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css'],
})
export class UserManagementComponent implements OnInit {
  userObj: any = sessionStorage.getItem('tokenKey');
  loggedInUser: any = '';
  userMangementList = [
    {
      id: 1,
      username: 'petter',
      profile: 'web developer',
      email: 'petter@gmail.com',
      isActivated: true,
    },
    {
      id: 2,
      username: 'sam',
      profile: 'SAP developer',
      email: 'sam@gmail.com',
      isActivated: false,
    },
    {
      id: 3,
      username: 'john',
      profile: 'web developer',
      email: 'john@gmail.com',
      isActivated: false,
    },
    {
      id: 4,
      username: 'joe',
      profile: 'Frontend developer',
      email: 'joe@gmail.com',
      isActivated: true,
    },
    {
      id: 5,
      username: 'Dev',
      profile: 'Frontend developer',
      email: 'dev@gmail.com',
      isActivated: true,
    },
    {
      id: 6,
      username: 'jamil',
      profile: 'Frontend developer',
      email: 'jamil@gmail.com',
      isActivated: true,
    },
    {
      id: 7,
      username: 'johny',
      profile: 'Fullstack developer',
      email: 'johny@gmail.com',
      isActivated: false,
    },
  ];
  constructor() {}

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.

    if (this.userObj) {
      this.loggedInUser = JSON.parse(this.userObj);
    }
  }
}
