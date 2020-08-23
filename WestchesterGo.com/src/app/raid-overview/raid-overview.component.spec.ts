import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaidOverviewComponent } from './raid-overview.component';

describe('RaidOverviewComponent', () => {
  let component: RaidOverviewComponent;
  let fixture: ComponentFixture<RaidOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaidOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaidOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
