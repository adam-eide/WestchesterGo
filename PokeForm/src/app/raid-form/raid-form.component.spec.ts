import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaidFormComponent } from './raid-form.component';

describe('RaidFormComponent', () => {
  let component: RaidFormComponent;
  let fixture: ComponentFixture<RaidFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaidFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaidFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
