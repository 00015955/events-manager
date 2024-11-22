import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IEvent} from '../interfaces/event.interface';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  http: HttpClient = inject(HttpClient);
  baseApiUrl = 'http://localhost:5182/api';

  getEvents(){
    return this.http.get<IEvent[]>(`${this.baseApiUrl}/event`)
  }
}
