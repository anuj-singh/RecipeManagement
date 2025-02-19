import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { RecipesManagementRoutingModule } from './recipes-management-routing.module';
import { RecipesManagementComponent } from './recipes-management/recipes-management.component';


@NgModule({
  declarations: [
    RecipesManagementComponent
  ],
  imports: [
    CommonModule,
    RecipesManagementRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class RecipesManagementModule { }
