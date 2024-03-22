import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PresentationInputComponent } from './presentation-input.component';

describe('PresentationInputComponent', () => {
  let component: PresentationInputComponent;
  let fixture: ComponentFixture<PresentationInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PresentationInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PresentationInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
