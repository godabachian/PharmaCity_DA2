import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SymptomsInputComponent } from './symptoms-input.component';

describe('SymptomsInputComponent', () => {
  let component: SymptomsInputComponent;
  let fixture: ComponentFixture<SymptomsInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SymptomsInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SymptomsInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
