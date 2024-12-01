import {Component, inject} from '@angular/core';
import {EventCardComponent} from './ui/event-card/event-card.component';
import {EventService} from './data/services/event.service';
import {IEvent} from './data/interfaces/event.interface';
import {AddEventModalComponent} from './ui/add-event-modal/add-event-modal.component';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, EventCardComponent, AddEventModalComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  eventService = inject(EventService);
  events: IEvent[] = [];
  showAddEventModal = false;

  constructor() {
    this.eventService.getAllEvents().subscribe({
      next: (val) => {
        this.events = val;
      },
      error: () => {
        console.error('Failed to load events');
      }
    })
  }

  addEvent($event: IEvent) {
    this.eventService.createEvent($event).subscribe({
      next: (createdEvent) => {
        this.events.push(createdEvent);
        console.log('Event added successfully!', 'Success');
        this.showAddEventModal = false;
      },
      error: (error) => {
        console.error('Failed to create event:', error);
      }
    });
  }

  handleEventDeleted(eventId: number) {
    this.events = this.events.filter(event => event.id !== eventId);
  }

  trackByEventId(index: number, e: IEvent): number | undefined {
    return e.id;
  }
}
