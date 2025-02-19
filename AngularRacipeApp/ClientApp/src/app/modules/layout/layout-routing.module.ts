import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: LayoutComponent },
      {
        path: 'main-dashboard',
        loadChildren: () =>
          import('../main-dashboard/main-dashboard.module').then(
            (m) => m.MainDashboardModule
          ),
      },
      {
        path: 'recipes-management',
        loadChildren: () =>
          import('../recipes-management/recipes-management.module').then(
            (m) => m.RecipesManagementModule
          ),
      },
      {
        path: 'user-management',
        loadChildren: () =>
          import('../users-management/users-management.module').then(
            (m) => m.UsersManagementModule
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LayoutRoutingModule {}
