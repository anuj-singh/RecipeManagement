import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RecipesManagementRoutingModule } from './recipes-management-routing.module';
import { RecipesManagementComponent } from './recipes-management/recipes-management.component';


@NgModule({
  declarations: [
    RecipesManagementComponent
  ],
  imports: [
    CommonModule,
    RecipesManagementRoutingModule
  ]
})
export class RecipesManagementModule { }
