import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Article } from '../../models/article';
import { Observable } from 'rxjs';
import { GetRatedArticlesRequest } from '../../models/get-rated-articles-request';

@Injectable({
  providedIn: 'root',
})
export class ArticlesService {
  constructor(private apiService: ApiService) {}

  getArticles(data: GetRatedArticlesRequest): Observable<Article[] | null> {
    return this.apiService.get<Article[]>('Articles', data);
  }

  getArticlesCount(minRate?: number): Observable<number | null> {
    return this.apiService.get<number>('Articles/Count', {minRate});
  }

  getArticleById(id: number): Observable<Article | null> {
    return this.apiService.get<Article>(`Articles/${id}`);
  }
}
