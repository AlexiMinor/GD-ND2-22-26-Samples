import { Component, signal } from '@angular/core';
import { LoginModel } from '../../models/login-model';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule, MatLabel } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { FeedbackMode, PasswordRulesComponent } from '@ngx-zen/mat-password-meter/rules';
import { CustomRulesFn } from '@ngx-zen/mat-password-meter';

@Component({
  selector: 'app-login-form-component',
  imports: [
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    PasswordRulesComponent,
  ],
  standalone: true,
  templateUrl: './login-form-component.html',
  styleUrl: './login-form-component.scss',
})
export class LoginFormComponent {
  loginModel: LoginModel;
  hide = signal(true);
  readonly currentStrength = signal(0);
  readonly isPasswordValid = signal(false);

  darkMode = signal(false);
  feedback = signal<FeedbackMode>('full');
  hideStrength = signal(false);

  //validation parameters
  minLength = signal(8);
  optLowercase = signal(true);
  optUppercase = signal(true);
  optNumber = signal(true);
  optSpecialChar = signal(true);

  constructor() {
    this.loginModel = new LoginModel();
  }

  readonly customRules: CustomRulesFn = (password) => [
    { label: 'Must not contain username', passed: !this.loginModel.password.toLowerCase().includes('user') },
  ];

  onStrengthChange(value: number): void {
    this.currentStrength.set(value);
  }

  onIsValid(valid: boolean): void {
    this.isPasswordValid.set(valid);
  }

  login(): void {
    console.log('Login attempted with:', this.loginModel);
  }
}
