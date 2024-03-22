import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddInvitationComponent } from './add-invitation.component';

describe('AddInvitationComponent', () => {
  let component: AddInvitationComponent;
  let fixture: ComponentFixture<AddInvitationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddInvitationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddInvitationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
