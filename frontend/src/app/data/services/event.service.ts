//Student ID: 00015955
import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {IEvent} from '../interfaces/event.interface';
import {IComment} from '../interfaces/comment.interface';
import {Observable} from 'rxjs';
import {environment} from '../../../environment';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  http: HttpClient = inject(HttpClient);
  private baseApiUrl = `${environment.apiBaseUrl}/api`;

  getAllEvents(){
    return this.http.get<IEvent[]>(`${this.baseApiUrl}/event`)
  }
  getEvent(id: number){
    return this.http.get<IEvent>(`${this.baseApiUrl}/event/${id}`)
  }
  createEvent(formData: FormData){
    return this.http.post<IEvent>(`${this.baseApiUrl}/event`, formData)
  }
  updateEvent(id: number, formData: FormData): Observable<any> {
    return this.http.put(`${this.baseApiUrl}/event/${id}`, formData)
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
