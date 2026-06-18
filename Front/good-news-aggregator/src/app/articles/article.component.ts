import { Component, effect, Signal, signal, WritableSignal } from '@angular/core';
import { Article } from '../../models/article';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ArticlePreviewComponent } from '../article-preview/article-preview';
import { ArticleTitleEditFormComponent } from '../article-title-edit-form-component/article-title-edit-form-component';
import { ArticlesService } from '../../services/articles/articles-service';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-articles',
  imports: [
    CommonModule,
    FormsModule,
    ArticlePreviewComponent,
    ArticleTitleEditFormComponent,
    MatGridListModule,
    MatPaginatorModule,
  ],
  templateUrl: './article.component.html',
  styleUrl: './article.component.scss',
})
export class ArticleComponent {
  selectedArticle?: Article;
  articles: WritableSignal<Article[] | null> = signal(null);
  isArticlesLoaded: WritableSignal<boolean> = signal(false);
  length: WritableSignal<number | null> = signal(null);
  pageSize = 15;
  pageIndex = 0;
  pageSizeOptions = [6, 15, 24];
  pageEvent: PageEvent | undefined;

  constructor(private articlesService: ArticlesService) {
    this.loadArticles();
    this.setArticlesCount();
    effect(() => {
      console.log(this.articles());
      console.log(this.isArticlesLoaded());
    });
  }

  handlePageEvent(e: PageEvent) {
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;

    this.loadArticles();
  }
  private setArticlesCount(): void {
    this.articlesService.getArticlesCount().subscribe((count) => {
      this.length.set(count);
      console.log(count);
    });
  }

  private loadArticles(): void {
    this.isArticlesLoaded.set(false);

    this.articlesService
      .getArticles({
        pageNumber: this.pageIndex + 1,
        pageSize: this.pageSize,
      })
      .subscribe((articles) => {
        this.articles.set(articles);
        this.isArticlesLoaded.set(true);
      });
  }

  onSelect(article: Article): void {
    this.selectedArticle = article;
  }

  onSelectedArticle(article: Article): void {
    this.selectedArticle = article;
  }
}
