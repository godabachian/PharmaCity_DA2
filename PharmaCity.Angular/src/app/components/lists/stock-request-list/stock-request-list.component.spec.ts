import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockRequestListComponent } from './stock-request-list.component';

describe('StockRequestListComponent', () => {
  let component: StockRequestListComponent;
  let fixture: ComponentFixture<StockRequestListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockRequestListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StockRequestListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
