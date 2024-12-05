//Student ID: 00015955
import { Injectable } from '@angular/core';

export interface Toast {
  message: string;
  type: 'success' | 'error';
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  toasts: Toast[] = [];

  show(message: string, type: 'success' | 'error' = 'success') {
    const toast: Toast = { message, type };
    this.toasts.push(toast);
    setTimeout(() => this.remove(toast), 3000);
  }

  remove(toast: Toast) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }
}
