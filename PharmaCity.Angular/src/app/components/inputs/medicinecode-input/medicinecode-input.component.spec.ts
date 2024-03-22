import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicinecodeInputComponent } from './medicinecode-input.component';

describe('MedicinecodeInputComponent', () => {
  let component: MedicinecodeInputComponent;
  let fixture: ComponentFixture<MedicinecodeInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicinecodeInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicinecodeInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
