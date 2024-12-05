//Student ID: 00015955
import { Routes } from '@angular/router';
import {EventDetailsComponent} from './ui/event-details/event-details.component';
import {EventListComponent} from './ui/event-list/event-list.component';

export const routes: Routes = [
  { path: '', component: EventListComponent },
  { path: 'event/:id', component: EventDetailsComponent },
  { path: '**', redirectTo: '' }
];
