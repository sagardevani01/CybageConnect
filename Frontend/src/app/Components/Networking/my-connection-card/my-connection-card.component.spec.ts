import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyConnectionCardComponent } from './my-connection-card.component';

describe('MyConnectionCardComponent', () => {
  let component: MyConnectionCardComponent;
  let fixture: ComponentFixture<MyConnectionCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MyConnectionCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MyConnectionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
