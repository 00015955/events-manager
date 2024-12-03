import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule} from '@angular/forms';
import { IEvent } from '../../data/interfaces/event.interface';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-add-event-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-event-modal.component.html',
})
export class AddEventModalComponent {
  @Output() close = new EventEmitter<void>();
  @Output() eventAdded = new EventEmitter<IEvent>();

  constructor() {}

  newEvent: IEvent = {
    id: 0,
    startDate: new Date(),
    name: '',
    description: '',
    image: '',
    location: '',
  };

  onSubmit() {
    this.eventAdded.emit(this.newEvent);
    this.closeModal();
  }

  closeModal() {
    this.close.emit();
  }
}

