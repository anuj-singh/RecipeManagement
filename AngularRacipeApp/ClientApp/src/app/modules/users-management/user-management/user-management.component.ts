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
  searchedUser: any = '';
  userMangementList: any[] = [];

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    if (this.userDetails) {
      this.loggedInUser = JSON.parse(this.userDetails);
    }
    this.getAllUsers();
  }

  // get all users functions
  getAllUsers() {
    this.dataService
      .httpGetRequest('User/GetAllUsers')
      .subscribe((res: any) => {
        this.userMangementList = res;
      });
  }

  // Banned unbanned user functions
  bannedUnbannedUser(event: any) {
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

  // Search user functions by username
  onSearchUser() {
    const searchUserObj = {
      userName: this.searchedUser,
      email: '',
    };

    if (searchUserObj.userName.length > 2) {
      this.dataService
        .httpPostRequest('User/GetUserDetailsForSearch', searchUserObj)
        .subscribe((res: any) => {
          this.userMangementList = [];
          this.userMangementList = res;
        });
    }
  }

  // Delete user functions
  onDeleteUser(user: any) {
    if (confirm('Are you sure wants to delete this user?')) {
      this.dataService
        .httpDeleteRequest(`User/DeleteUser?id=${user.userId}`)
        .subscribe((res: any) => {
          alert(res.message);
          this.getAllUsers();
        });
    }
  }

  refreshPage() {
    this.searchedUser = '';
    this.getAllUsers();
  }
}
