import {Component, EventEmitter, Input, Output} from '@angular/core';
import {IEvent} from '../../data/interfaces/event.interface';
import {EventService} from '../../data/services/event.service';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './event-card.component.html'
})
export class EventCardComponent {
  @Input() event!: IEvent;
  @Output() eventDeleted = new EventEmitter<number>();
  showDeleteModal = false;

  constructor(private eventService: EventService) {}

  openDeleteModal() {
    this.showDeleteModal = true;
  }

  closeDeleteModal() {
    this.showDeleteModal = false;
  }

  deleteEvent() {
    this.eventService.deleteEvent(this.event.id).subscribe({
      next: () => {
        console.log('Event deleted successfully!', 'Success');
        this.eventDeleted.emit(this.event.id); // Notify the parent about the deletion
        this.closeDeleteModal();
      },
      error: (error) => {
        console.error('Failed to delete event:', error);
        this.closeDeleteModal();
      }
    });
  }
}
