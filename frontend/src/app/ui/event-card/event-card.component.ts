//Student ID: 00015955
import {Component, EventEmitter, Input, Output} from '@angular/core';
import {IEvent} from '../../data/interfaces/event.interface';
import {EventService} from '../../data/services/event.service';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {ToastService} from '../../data/services/toasts.service';
import {environment} from '../../../environment';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './event-card.component.html'
})
export class EventCardComponent {
  @Input() event!: IEvent;
  @Output() eventDeleted = new EventEmitter<number>();
  @Output() eventUpdated = new EventEmitter<IEvent>();
  showDeleteModal = false;
  showModal = false;
  editableEvent: IEvent = { ...this.event };
  selectedFile: File | null = null;

  constructor(private eventService: EventService, private toastService: ToastService) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
    }
  }

  openDeleteModal() {
    this.showDeleteModal = true;
  }

  closeDeleteModal() {
    this.showDeleteModal = false;
  }

  openEditModal() {
    this.editableEvent = { ...this.event };
    this.showModal = true;
  }

  closeEditModal() {
    this.showModal = false;
  }

  updateEvent() {
    const formData = new FormData();
    formData.append('Name', this.editableEvent.name);
    formData.append('Location', this.editableEvent.location);
    formData.append('Description', this.editableEvent.description);
    formData.append('StartDate', new Date(this.editableEvent.startDate).toISOString());

    if (this.selectedFile) {
      formData.append('ImageFile', this.selectedFile);
    }

    this.eventService.updateEvent(this.editableEvent.id, formData).subscribe({
      next: (updatedEvent) => {
        // Update the local event data with the response
        this.event = { ...updatedEvent };
        this.eventUpdated.emit(this.event);
        this.toastService.show('Event updated successfully!', 'success');
        this.closeEditModal();
      },
      error: (err) => {
        console.error('Failed to update event:', err);
        this.toastService.show('Failed to update event. Please try again.', 'error');
      },
    });
  }

  deleteEvent() {
    this.eventService.deleteEvent(this.event.id).subscribe({
      next: () => {
        this.toastService.show('Event deleted successfully!', 'success');
        this.eventDeleted.emit(this.event.id); // Notify the parent about the deletion
        this.closeDeleteModal();
      },
      error: (error) => {
        console.error('Failed to delete event:', error);
        this.toastService.show('Failed to delete event. Please try again.', 'error');
        this.closeDeleteModal();
      }
    });
  }
}



