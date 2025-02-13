import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-update-profile-modal',
  templateUrl: './update-profile-modal.component.html',
  styleUrls: ['./update-profile-modal.component.css']
})
export class UpdateProfileModalComponent implements OnInit {
  

  constructor() { }
  @ViewChild('myModal') myModal!: ElementRef;
  ngOnInit(): void {
  }

  openModal() {
    if(this.myModal) this.myModal.nativeElement.style.display = 'block';
  }

  closeModal() {
    if(this.myModal) this.myModal.nativeElement.style.display = 'none';
  }
}
