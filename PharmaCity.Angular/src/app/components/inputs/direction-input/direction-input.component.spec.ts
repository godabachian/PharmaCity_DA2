import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DirectionInputComponent } from './direction-input.component';

describe('DirectionInputComponent', () => {
  let component: DirectionInputComponent;
  let fixture: ComponentFixture<DirectionInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DirectionInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DirectionInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
