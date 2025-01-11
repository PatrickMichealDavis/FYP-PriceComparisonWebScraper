import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckScraperComponent } from './check-scraper.component';

describe('CheckScraperComponent', () => {
  let component: CheckScraperComponent;
  let fixture: ComponentFixture<CheckScraperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CheckScraperComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CheckScraperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
