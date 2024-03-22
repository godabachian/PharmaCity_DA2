import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmacyInputComponent } from './pharmacy-input.component';

describe('PharmacyInputComponent', () => {
  let component: PharmacyInputComponent;
  let fixture: ComponentFixture<PharmacyInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PharmacyInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PharmacyInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
