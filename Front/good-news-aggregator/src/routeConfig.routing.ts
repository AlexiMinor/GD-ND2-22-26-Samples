import { Routes, RouterModule } from '@angular/router';
import { ArticleDetailsComponent } from './app/article-details-component/article-details-component';
import { ArticleComponent } from './app/articles/article.component';
import { LoginFormComponent } from './app/login-form-component/login-form-component';

const routeConfig: Routes = [
  { path: '', component: ArticleComponent, title: 'Articles' },
  { path: 'articles', redirectTo: '', pathMatch: 'full' },
  { path: 'articles/:id', component: ArticleDetailsComponent, title: 'Article Details' },
  { path: 'login', component: LoginFormComponent, title: 'Login' },
];

export default routeConfig;
