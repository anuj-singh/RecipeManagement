import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {
  @Input() sidenavStatus: boolean = false;
  list: any = [
    {
      routerLink: 'main-dashboard',
      label: 'Dashboard',
      icon: 'fas fa-home-lg-alt',
    },
    {
      routerLink: '/recipes-management',
      label: 'Recipes Management',
      icon: 'fa-regular fa-note-sticky',
    },
    {
      routerLink: 'user-management',
      label: 'Users Management',
      icon: 'fa-solid fa-user',
    },
  ];

  constructor(private router: Router, private dataService: DataService) {}
  ngOnInit(): void {}

  signout() {
    this.dataService.signOut();
  }
}
