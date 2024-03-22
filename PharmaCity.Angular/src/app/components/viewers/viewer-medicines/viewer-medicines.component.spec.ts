import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewerMedicinesComponent } from './viewer-medicines.component';

describe('ViewerMedicinesComponent', () => {
  let component: ViewerMedicinesComponent;
  let fixture: ComponentFixture<ViewerMedicinesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewerMedicinesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewerMedicinesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
