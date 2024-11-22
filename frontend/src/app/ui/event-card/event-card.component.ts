import { Component, Input } from '@angular/core';
import {NgOptimizedImage} from '@angular/common';
import {IEvent} from '../../data/interfaces/event.interface';

@Component({
  selector: 'app-event-card',
  standalone: true,
  imports: [
    NgOptimizedImage
  ],
  templateUrl: './event-card.component.html'
})
export class EventCardComponent {
  @Input() event!: IEvent;
}
