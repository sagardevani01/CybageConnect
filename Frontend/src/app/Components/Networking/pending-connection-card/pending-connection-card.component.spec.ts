import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingConnectionCardComponent } from './pending-connection-card.component';

describe('PendingConnectionCardComponent', () => {
  let component: PendingConnectionCardComponent;
  let fixture: ComponentFixture<PendingConnectionCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PendingConnectionCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PendingConnectionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
