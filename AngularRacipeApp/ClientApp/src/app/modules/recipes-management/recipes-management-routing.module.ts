import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecipesManagementComponent } from './recipes-management/recipes-management.component';

const routes: Routes = [{ path: '', component: RecipesManagementComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RecipesManagementRoutingModule {}
