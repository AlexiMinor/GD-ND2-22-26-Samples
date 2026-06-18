import { Component, Input } from '@angular/core';
import { Article } from '../../models/article';

@Component({
  selector: 'app-article-title-edit-form-component',
  imports: [],
  templateUrl: './article-title-edit-form-component.html',
  styleUrl: './article-title-edit-form-component.scss',
})
export class ArticleTitleEditFormComponent {
  @Input() article?: Article;
}
