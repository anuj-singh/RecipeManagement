import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipesManagementComponent } from './recipes-management.component';

describe('RecipesManagementComponent', () => {
  let component: RecipesManagementComponent;
  let fixture: ComponentFixture<RecipesManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecipesManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecipesManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
