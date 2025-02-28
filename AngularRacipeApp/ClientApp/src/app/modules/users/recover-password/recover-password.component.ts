import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrls: ['./recover-password.component.css'],
})
export class RecoverPasswordComponent implements OnInit {
  resetForm!: FormGroup;

  message: string | null = null;
  token: any;
  constructor(
    private fb: FormBuilder,
    private dataService: DataService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.resetForm = this.fb.group(
      {
        newPassword: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );

    this.route.queryParams.subscribe((params) => {
      console.log(params);
      this.token = params['token'];
    });
  }

  passwordMatchValidator(formGroup: FormGroup) {
    const { newPassword, confirmPassword } = formGroup.value;
    return newPassword === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    const resetPwdObj = {
      token: this.token,
      newPassword: this.resetForm.controls['newPassword'].value,
    };

    this.dataService
      .httpPostRequest('User/reset-password', resetPwdObj)
      .subscribe((res: any) => {
        alert(res.message);
        this.router.navigate(['/users']);
      });
  }
}
