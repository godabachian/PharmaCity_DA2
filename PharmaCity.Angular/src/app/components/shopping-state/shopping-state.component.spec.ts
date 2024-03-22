import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingStateComponent } from './shopping-state.component';

describe('ShoppingStateComponent', () => {
  let component: ShoppingStateComponent;
  let fixture: ComponentFixture<ShoppingStateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingStateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShoppingStateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
