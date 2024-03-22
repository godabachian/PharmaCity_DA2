import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewerShoppingComponent } from './viewer-shopping.component';

describe('ViewerShoppingComponent', () => {
  let component: ViewerShoppingComponent;
  let fixture: ComponentFixture<ViewerShoppingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewerShoppingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewerShoppingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
