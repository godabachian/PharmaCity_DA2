import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicineInputComponent } from './medicine-input.component';

describe('MedicineInputComponent', () => {
  let component: MedicineInputComponent;
  let fixture: ComponentFixture<MedicineInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicineInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicineInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
