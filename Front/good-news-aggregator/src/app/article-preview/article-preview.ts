import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Article } from '../../models/article';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { SafeHtmlPipe } from '../../pipes/safe-html-pipe';

@Component({
  selector: 'app-article-preview',
  imports: [CommonModule, RouterLink, SafeHtmlPipe, MatCardModule, MatButtonModule],
  templateUrl: './article-preview.html',
  styleUrl: './article-preview.scss',
})

export class ArticlePreviewComponent {
 @Input() article?: Article;

 @Output() selectedArticleEvent = new EventEmitter<Article>();

  editTitle(): void {
    if (this.article) {
      this.selectedArticleEvent.emit(this.article);
    }
  }

  readMore(): void {
    // Implementation for reading more about the article can be added here
  }
}
