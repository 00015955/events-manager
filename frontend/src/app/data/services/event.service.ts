import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IEvent} from '../interfaces/event.interface';
import {IComment} from '../interfaces/comment.interface';
import {Observable} from 'rxjs';

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

  getAllComments(): Observable<IComment[]> {
    return this.http.get<IComment[]>(`${this.baseApiUrl}/comment`);
  }

  getComment(commentId: number): Observable<IComment> {
    return this.http.get<IComment>(`${this.baseApiUrl}/comment/${commentId}`);
  }

  createComment(eventId: number, comment: IComment): Observable<IComment> {
    return this.http.post<IComment>(`${this.baseApiUrl}/comment/${eventId}`, comment);
  }

  updateComment(commentId: number, comment: IComment): Observable<IComment> {
    return this.http.put<IComment>(`${this.baseApiUrl}/comment/${commentId}`, comment);
  }

  deleteComment(commentId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseApiUrl}/comment/${commentId}`);
  }
}
