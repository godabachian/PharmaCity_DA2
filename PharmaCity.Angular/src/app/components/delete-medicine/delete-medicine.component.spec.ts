import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteMedicineComponent } from './delete-medicine.component';

describe('DeleteMedicineComponent', () => {
  let component: DeleteMedicineComponent;
  let fixture: ComponentFixture<DeleteMedicineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteMedicineComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteMedicineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
