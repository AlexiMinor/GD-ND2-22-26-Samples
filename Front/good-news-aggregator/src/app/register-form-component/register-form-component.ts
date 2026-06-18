import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-form-component',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './register-form-component.html',
  styleUrl: './register-form-component.scss',
})

export class RegisterFormComponent {
  registerFormData = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
    passwordConfirmation: new FormControl('', Validators.required),
  });
}
