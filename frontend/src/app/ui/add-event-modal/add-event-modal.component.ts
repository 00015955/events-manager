//Student ID: 00015955
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule} from '@angular/forms';
import { IEvent } from '../../data/interfaces/event.interface';
import {CommonModule} from '@angular/common';
import {EventService} from '../../data/services/event.service';
import {ToastService} from '../../data/services/toasts.service';

@Component({
  selector: 'app-add-event-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-event-modal.component.html',
})
export class AddEventModalComponent {
  @Output() close = new EventEmitter<void>();
  @Output() eventAdded = new EventEmitter<IEvent>();

  constructor(private eventService: EventService, private toastService: ToastService) {}

  newEvent: IEvent = {
    id: 0,
    startDate: new Date(),
    name: '',
    description: '',
    image: '',
    location: '',
  };

  selectedFile: File | null = null;

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.selectedFile = input.files[0];
    }
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('Name', this.newEvent.name);
    formData.append('Location', this.newEvent.location);
    formData.append('Description', this.newEvent.description);
    formData.append('StartDate', this.newEvent.startDate.toISOString());
    if (this.selectedFile) {
      formData.append('ImageFile', this.selectedFile);
    }

    this.eventService.createEvent(formData).subscribe({
      next: (createdEvent) => {
        console.log('Created Event:', createdEvent);
        this.eventAdded.emit(createdEvent);
        this.closeModal();
      },
      error: (error) => {
        console.error('Failed to create event:', error);
        this.toastService.show('Failed to create event. Please try again.', 'error');
      },
    });
  }

  closeModal() {
    this.close.emit();
  }
}

