import { Component, inject, OnInit, Signal } from '@angular/core';
import { Article } from '../../models/article';
import {ActivatedRoute, RouterLink} from "@angular/router";
import { ArticlesService } from '../../services/articles/articles-service';
import { Location } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { SafeHtmlPipe } from '../../pipes/safe-html-pipe';

@Component({
  selector: 'app-article-details-component',
  imports: [RouterLink, SafeHtmlPipe],
  templateUrl: './article-details-component.html',
  styleUrl: './article-details-component.scss',
})
export class ArticleDetailsComponent {
  id?: number;
  article: Signal<Article | null | undefined>;

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticlesService,
    private location: Location,
  ) {
    this.id = Number(this.route.snapshot.params['id']);
    console.log('ArticleDetailsComponent initialized with id:', this.id);

    this.article = toSignal(this.articleService.getArticleById(this.id));
  }

  goBack(): void {
    this.location.back();
  }
}
