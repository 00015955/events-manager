//Student ID: 00015955
import {Component, inject, OnInit} from '@angular/core';
import {EventService} from '../../data/services/event.service';
import {IEvent} from '../../data/interfaces/event.interface';
import {AddEventModalComponent} from '../add-event-modal/add-event-modal.component';
import {EventCardComponent} from '../event-card/event-card.component';
import {NgForOf, NgIf} from '@angular/common';
import {ToastService} from '../../data/services/toasts.service';

@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [
    AddEventModalComponent,
    EventCardComponent,
    NgForOf,
    NgIf
  ],
  templateUrl: './event-list.component.html'
})
export class EventListComponent implements OnInit {
  eventService = inject(EventService);
  events: IEvent[] = [];
  showAddEventModal = false;

  constructor(private toastService: ToastService) {
    this.eventService.getAllEvents().subscribe({
      next: (val) => {
        this.events = val;
      },
      error: () => {
        console.error('Failed to load events');
      }
    })
  }

  addEvent(event: IEvent) {
    this.events.push(event);
    this.toastService.show('Event added successfully!', 'success');
    this.showAddEventModal = false;
  }

  handleEventDeleted(eventId: number) {
    this.events = this.events.filter(event => event.id !== eventId);
  }

  trackByEventId(index: number, e: IEvent): number | undefined {
    return e.id;
  }

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe({
      next: (events) => (this.events = events),
      error: (err) => console.error('Error fetching events:', err),
    });
  }
}
