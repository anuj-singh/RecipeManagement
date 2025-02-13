import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  constructor(private dataService: DataService) {}
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.getAllUsers();
  }

  getAllUsers() {
    this.dataService
      .httpGetRequest('https://jsonplaceholder.typicode.com/users')
      .subscribe({
        next: (res: any) => {
          console.log(res);
        },
      });
  }
}
