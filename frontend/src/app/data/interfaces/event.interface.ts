import {IComment} from './comment.interface';

export interface IEvent {
  id: number;
  name: string;
  location: string;
  startDate: Date;
  image: string;
  description: string;
  comments?: IComment[] | null;
}
