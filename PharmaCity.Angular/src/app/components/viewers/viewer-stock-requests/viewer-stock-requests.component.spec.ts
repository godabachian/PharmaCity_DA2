import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewerStockRequestsComponent } from './viewer-stock-requests.component';

describe('ViewerStockRequestsComponent', () => {
  let component: ViewerStockRequestsComponent;
  let fixture: ComponentFixture<ViewerStockRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewerStockRequestsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewerStockRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
