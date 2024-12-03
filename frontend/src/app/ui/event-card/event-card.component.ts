import {Component, EventEmitter, Input, Output} from '@angular/core';
import {IEvent} from '../../data/interfaces/event.interface';
import {EventService} from '../../data/services/event.service';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';
import {ToastService} from '../../data/services/toasts.service';

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
  editableEvent!: IEvent;

  constructor(private eventService: EventService, private toastService: ToastService) {}

  openDeleteModal() {
    this.showDeleteModal = true;
  }

  closeDeleteModal() {
    this.showDeleteModal = false;
  }

  openEditModal() {
    this.editableEvent = { ...this.event }; // Create a shallow copy for editing
    this.showModal = true;
  }

  closeEditModal() {
    this.showModal = false;
  }

  updateEvent() {
    this.eventService.updateEvent(this.editableEvent.id, this.editableEvent).subscribe({
      next: () => {
        this.event.name = this.editableEvent.name;
        this.event.location = this.editableEvent.location;
        this.event.description = this.editableEvent.description;
        this.event.image = this.editableEvent.image;

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



