//Student ID: 00015955
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ToastComponent} from '../toast/toast.component';
import {ToastService} from '../../data/services/toasts.service';

@Component({
  selector: 'app-toasts-container',
  standalone: true,
  imports: [CommonModule, ToastComponent],
  template: `
    <div class="toasts-container">
      <app-toast
        *ngFor="let toast of toastService.toasts"
        [message]="toast.message"
        [type]="toast.type"
      ></app-toast>
    </div>
  `,
  styles: [`
    .toasts-container {
      position: fixed;
      top: 1rem;
      right: 1rem;
      width: 300px;
      z-index: 9999;
    }
  `],
})
export class ToastsContainerComponent {
  constructor(public toastService: ToastService) {}
}

