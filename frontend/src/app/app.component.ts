import {Component, inject} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {EventCardComponent} from './ui/event-card/event-card.component';
import {EventService} from './data/services/event.service';
import {JsonPipe} from '@angular/common';
import {IEvent} from './data/interfaces/event.interface';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, EventCardComponent, JsonPipe],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'frontend';
  eventService = inject(EventService);
  events: IEvent[] = [];

  constructor() {
    this.eventService.getAllEvents().subscribe(val => {
      this.events = val
    });
  }
}
