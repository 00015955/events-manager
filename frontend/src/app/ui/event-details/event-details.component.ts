import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, RouterModule,} from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {IEvent} from '../../data/interfaces/event.interface';
import {IComment} from '../../data/interfaces/comment.interface';
import {EventService} from '../../data/services/event.service';

@Component({
  selector: 'app-event-details',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './event-details.component.html',
})
export class EventDetailsComponent implements OnInit {
  eventId!: number;
  event?: IEvent;
  comments: IComment[] = [];

  // For adding/editing comments
  showCommentModal = false;
  isEditing = false;
  currentComment: IComment = this.initializeComment();

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService,
  ) {}

  ngOnInit(): void {
    // Get the event ID from the route parameters
    this.route.paramMap.subscribe((params) => {
      const idParam = params.get('id');
      if (idParam) {
        this.eventId = +idParam;
        this.loadEvent();
        this.loadComments();
      }
    });
  }

  // Fetch event details by ID
  loadEvent(): void {
    this.eventService.getEvent(this.eventId).subscribe({
      next: (event) => {
        this.event = event;
      },
      error: (err) => {
        console.error('Error fetching event:', err);
      },
    });
  }

  // Fetch all comments and filter by eventId
  loadComments(): void {
    this.eventService.getAllComments().subscribe({
      next: (comments) => {
        this.comments = comments.filter((comment) => comment.eventId === this.eventId);
      },
      error: (err) => {
        console.error('Error fetching comments:', err);
      },
    });
  }

  // Initialize a new comment object
  initializeComment(): IComment {
    return {
      id: 0,
      title: '',
      content: '',
      createdOn: new Date(),
      eventId: this.eventId, // Set eventId to current eventId
    };
  }

  // Open modal to add a new comment
  openAddCommentModal(): void {
    this.isEditing = false;
    this.currentComment = this.initializeComment();
    this.showCommentModal = true;
  }

  // Open modal to edit an existing comment
  openEditCommentModal(comment: IComment): void {
    this.isEditing = true;
    this.currentComment = { ...comment };
    this.showCommentModal = true;
  }

  // Close the comment modal
  closeCommentModal(): void {
    this.showCommentModal = false;
  }

  // Submit the comment (add or update)
  submitComment(): void {
    if (this.isEditing) {
      // Update existing comment
      this.eventService
        .updateComment(this.currentComment.id, this.currentComment)
        .subscribe({
          next: (updatedComment) => {
            // Update the comment in the comments array
            const index = this.comments.findIndex((c) => c.id === updatedComment.id);
            if (index !== -1) {
              this.comments[index] = updatedComment;
            }
            console.log('Comment updated successfully!', 'Success');
            this.closeCommentModal();
          },
          error: (err) => {
            console.error('Failed to update comment:', err);
          },
        });
    } else {
      // Add new comment
      this.currentComment.eventId = this.eventId; // Ensure eventId is set
      this.eventService.createComment(this.eventId, this.currentComment).subscribe({
        next: (newComment) => {
          this.comments.push(newComment);
          console.log('Comment added successfully!', 'Success');
          this.closeCommentModal();
        },
        error: (err) => {
          console.error('Failed to add comment:', err);
        },
      });
    }
  }

  // Delete a comment
  deleteComment(commentId: number): void {
    if (confirm('Are you sure you want to delete this comment?')) {
      this.eventService.deleteComment(commentId).subscribe({
        next: () => {
          this.comments = this.comments.filter((c) => c.id !== commentId);
          console.log('Comment deleted successfully!', 'Success');
        },
        error: (err) => {
          console.error('Failed to delete comment:', err);
        },
      });
    }
  }
}

