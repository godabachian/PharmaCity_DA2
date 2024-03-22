import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewerInvitationsComponent } from './viewer-invitations.component';

describe('ViewerInvitationsComponent', () => {
  let component: ViewerInvitationsComponent;
  let fixture: ComponentFixture<ViewerInvitationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewerInvitationsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewerInvitationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
