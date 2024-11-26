import {Component, inject} from '@angular/core';
import {EventCardComponent} from './ui/event-card/event-card.component';
import {EventService} from './data/services/event.service';
import {IEvent} from './data/interfaces/event.interface';
import {AddEventModalComponent} from './ui/add-event-modal/add-event-modal.component';
import {NgForOf, NgIf} from '@angular/common';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [EventCardComponent, AddEventModalComponent, NgIf, NgForOf],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'frontend';
  eventService = inject(EventService);
  toastr = inject(ToastrService);
  events: IEvent[] = [];
  showAddEventModal = false;

  constructor() {
    this.eventService.getAllEvents().subscribe(val => {
      this.events = val
    });
  }

  addEvent($event: IEvent) {
    this.eventService.createEvent($event).subscribe(
      (createdEvent) => {
        this.events.push(createdEvent);
        this.toastr.success('Event added successfully!', 'Success');
        this.showAddEventModal = false;
      },
      (error) => {
        console.error('Failed to create event:', error);
        this.toastr.error('Failed to add event. Please try again.', 'Error');
      }
    );
  }

  trackByEventId(index: number, e: IEvent): number | undefined {
    return e.id;
  }
}
