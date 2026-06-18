import { Component, OnDestroy, signal } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { TestService } from '../services/test/test-service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MediaMatcher } from '@angular/cdk/layout';

@Component({
  //decorator
  selector: 'app-root',
  imports: [
    RouterOutlet,
    RouterLink,
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss'],
  standalone: true,
})
export class App implements OnDestroy {
  title: string = 'Good news aggregator';
  // counter: WritableSignal<number> = signal(0);
  // firstName = signal('John');
  // lastName = signal('Doe');
  // fullName = computed(() => `${this.firstName()} ${this.lastName()}`);

  protected readonly isMobile = signal(true);

  private readonly _mobileQuery: MediaQueryList;
  private readonly _mobileQueryListener: () => void;

  constructor(private testService: TestService, private mediaMatcher: MediaMatcher) {
    this.testService.getData();

    this._mobileQuery = mediaMatcher.matchMedia('(max-width: 600px)');
    this.isMobile.set(this._mobileQuery.matches);
    this._mobileQueryListener = () => this.isMobile.set(this._mobileQuery.matches);
    this._mobileQuery.addEventListener('change', this._mobileQueryListener);
  }

  ngOnDestroy(): void {
    this._mobileQuery.removeEventListener('change', this._mobileQueryListener);
  }

  // constructor() {
    // effect(() => {
    //   console.log(`The current count is: ${this.counter()}`);
    // });
  // }

  // increment() {
    // this.counter.set(20); //whole change of value
    // this.counter.update((currentValue) => currentValue + 1); //change based on current value
}
