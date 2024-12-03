import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {ToastsContainerComponent} from './ui/toasts-container/toasts-container.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToastsContainerComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {}
