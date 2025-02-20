import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css'],
})
export class UserManagementComponent implements OnInit {
  userDetails: any = sessionStorage.getItem('tokenKey');
  loggedInUser: any;

  userMangementList: any[] = [];

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    if (this.userDetails) {
      this.loggedInUser = JSON.parse(this.userDetails);
    }
    this.getAllUsers();
  }

  getAllUsers() {
    this.dataService
      .httpGetRequest('User/GetAllUsers')
      .subscribe((res: any) => {
        this.userMangementList = res;
      });
  }

  onChangeUser(event: any) {
    if (event.statusId === 3) {
      this.dataService
        .httpPostRequest('Admin/UnBanSingleUser', event.userId)
        .subscribe((res: any) => {
          alert(res.message);
          this.getAllUsers();
        });
    } else {
      this.dataService
        .httpPostRequest('Admin/BanSingleUser', event.userId)
        .subscribe((res: any) => {
          this.getAllUsers();
          alert(res.message);
        });
    }
  }
}
