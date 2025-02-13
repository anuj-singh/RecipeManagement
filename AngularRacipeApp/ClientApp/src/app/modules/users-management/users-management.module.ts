import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersManagementRoutingModule } from './users-management-routing.module';
import { UserManagementComponent } from './user-management/user-management.component';


@NgModule({
  declarations: [
    UserManagementComponent
  ],
  imports: [
    CommonModule,
    UsersManagementRoutingModule
  ]
})
export class UsersManagementModule { }
