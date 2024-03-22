import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewerPharmaciesComponent } from './viewer-pharmacies.component';

describe('ViewerPharmaciesComponent', () => {
  let component: ViewerPharmaciesComponent;
  let fixture: ComponentFixture<ViewerPharmaciesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewerPharmaciesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewerPharmaciesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
