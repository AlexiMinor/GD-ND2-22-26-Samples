import { FormControl } from "@angular/forms";

export class RegisterModel {
  username: FormControl<string> = new FormControl('', { nonNullable: true });
  password: FormControl<string> = new FormControl('', { nonNullable: true });
  passwordConfirmation: FormControl<string> = new FormControl('', { nonNullable: true });
}
