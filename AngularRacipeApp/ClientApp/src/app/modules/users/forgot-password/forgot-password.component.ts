import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm!: FormGroup;
  message: string | null = null;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private dataService: DataService
  ) {}
  ngOnInit(): void {
    this.forgotpasswordform();
  }
  forgotpasswordform() {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      securityAnswer: ['', [Validators.required]],
    });
  }

  get email() {
    return this.forgotPasswordForm.get('email');
  }

  get securityAnswer() {
    return this.forgotPasswordForm.get('securityAnswer');
  }

  onSubmit() {
    this.dataService
      .httpPostRequest('User/forgot-password', this.forgotPasswordForm.value)
      .subscribe((res) => {});
  }
}
