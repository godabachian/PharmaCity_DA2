import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStockrequestComponent } from './add-stockrequest.component';

describe('AddStockrequestComponent', () => {
  let component: AddStockrequestComponent;
  let fixture: ComponentFixture<AddStockrequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddStockrequestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddStockrequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
