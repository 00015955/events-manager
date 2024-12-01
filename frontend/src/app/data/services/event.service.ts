import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IEvent} from '../interfaces/event.interface';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  http: HttpClient = inject(HttpClient);
  baseApiUrl = 'http://localhost:5182/api';

  getAllEvents(){
    return this.http.get<IEvent[]>(`${this.baseApiUrl}/event`)
  }
  getEvent(id: number){
    return this.http.get<IEvent>(`${this.baseApiUrl}/event/${id}`)
  }
  createEvent(event: IEvent){
    return this.http.post<IEvent>(`${this.baseApiUrl}/event`, event)
  }
  updateEvent(id: number, event: IEvent){
    return this.http.put(`${this.baseApiUrl}/event/${id}`, event)
  }
  deleteEvent(id: number){
    return this.http.delete(`${this.baseApiUrl}/event/${id}`)
  }
}
