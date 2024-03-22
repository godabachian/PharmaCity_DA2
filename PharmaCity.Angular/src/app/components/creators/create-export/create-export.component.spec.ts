import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateExportComponent } from './create-export.component';

describe('CreateExportComponent', () => {
  let component: CreateExportComponent;
  let fixture: ComponentFixture<CreateExportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateExportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateExportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
