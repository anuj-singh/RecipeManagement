import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  title: string = 'Recipes Management';
  user: string = 'admin';
  displayStyle = 'none';

  sideMenu: boolean = true;
  @Output() sideNavToggled = new EventEmitter();

  constructor() {}
  ngOnInit(): void {}

  sideNavToggle() {
    this.sideMenu = !this.sideMenu;
    this.sideNavToggled.emit(this.sideMenu);
  }

  openPopup() {
    this.displayStyle = 'block';
  }
  closePopup() {
    this.displayStyle = 'none';
  }
}
