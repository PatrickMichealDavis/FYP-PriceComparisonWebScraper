import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoggingTableComponent } from './logging-table.component';

describe('LoggingTableComponent', () => {
  let component: LoggingTableComponent;
  let fixture: ComponentFixture<LoggingTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoggingTableComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoggingTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
