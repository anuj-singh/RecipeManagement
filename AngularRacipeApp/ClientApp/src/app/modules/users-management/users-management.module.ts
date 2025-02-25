import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersManagementRoutingModule } from './users-management-routing.module';
import { UserManagementComponent } from './user-management/user-management.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    UserManagementComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    UsersManagementRoutingModule
  ]
})
export class UsersManagementModule { }
