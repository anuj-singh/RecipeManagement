import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'src/app/shared/service/data.service';

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrls: ['./recover-password.component.css']
})
export class RecoverPasswordComponent implements OnInit {
  resetForm!: FormGroup ;
 
  message: string | null = null;

  constructor(private fb: FormBuilder, private dataService: DataService, private router: Router) {}

  ngOnInit(): void {
    this.resetForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });
  }
  
  passwordMatchValidator(formGroup: FormGroup) {
    const { newPassword, confirmPassword } = formGroup.value;
    return newPassword === confirmPassword ? null : { mismatch: true };
  }
  
  onSubmit() {
    if (this.resetForm.valid) {
      // Handle form submission
      console.log('Form Submitted', this.resetForm.value);
      this.router.navigate(['/users']);
      this.dataService
     .httpPostRequest('', this.resetForm.value)
    .subscribe((res) => {
        console.log(res);
       });
    }
  }
}
  