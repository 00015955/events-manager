//Student ID: 00015955
import { Component, Input } from '@angular/core';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-toast',
  template: `
    <div class="toast" [ngClass]="type">
      <p>{{ message }}</p>
    </div>
  `,
  styles: [`
    .toast {
      padding: 1rem;
      border-radius: 4px;
      margin-bottom: 1rem;
      color: #fff;
      animation: fadeInOut 3s forwards;
    }

    .success {
      background-color: #28a745;
    }

    .error {
      background-color: #dc3545;
    }

    @keyframes fadeInOut {
      0% {
        opacity: 0;
      }
      10% {
        opacity: 1;
        transform: translateY(0);
      }
      50% {
        opacity: 1;
      }
      100% {
        opacity: 0;
      }
    }
  `],
  imports: [
    NgClass
  ],
  standalone: true
})
export class ToastComponent {
  @Input() message!: string;
  @Input() type: 'success' | 'error' = 'success';
}

