import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewGroupPostComponent } from './new-group-post.component';

describe('NewGroupPostComponent', () => {
  let component: NewGroupPostComponent;
  let fixture: ComponentFixture<NewGroupPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewGroupPostComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewGroupPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
