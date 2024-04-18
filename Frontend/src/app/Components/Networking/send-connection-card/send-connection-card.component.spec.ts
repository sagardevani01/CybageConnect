import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendConnectionCardComponent } from './send-connection-card.component';

describe('SendConnectionCardComponent', () => {
  let component: SendConnectionCardComponent;
  let fixture: ComponentFixture<SendConnectionCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SendConnectionCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SendConnectionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
