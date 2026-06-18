import { Injectable } from '@angular/core';
import { interval } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TestService {

  getData(): void {
    // interval(1000).subscribe(value => console.log(value))
  }
}
