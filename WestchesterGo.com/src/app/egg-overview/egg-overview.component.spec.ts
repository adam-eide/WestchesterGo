import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EggOverviewComponent } from './egg-overview.component';

describe('EggOverviewComponent', () => {
  let component: EggOverviewComponent;
  let fixture: ComponentFixture<EggOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EggOverviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EggOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
