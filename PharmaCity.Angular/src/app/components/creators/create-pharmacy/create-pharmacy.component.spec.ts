import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePharmacyComponent } from './create-pharmacy.component';

describe('CreatePharmacyComponent', () => {
  let component: CreatePharmacyComponent;
  let fixture: ComponentFixture<CreatePharmacyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatePharmacyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatePharmacyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
