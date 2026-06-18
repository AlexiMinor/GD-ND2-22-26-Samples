import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleTitleEditFormComponent } from './article-title-edit-form-component';

describe('ArticleTitleEditFormComponent', () => {
  let component: ArticleTitleEditFormComponent;
  let fixture: ComponentFixture<ArticleTitleEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArticleTitleEditFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ArticleTitleEditFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
