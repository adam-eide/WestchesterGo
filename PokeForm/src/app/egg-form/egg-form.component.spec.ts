import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EggFormComponent } from './egg-form.component';

describe('EggFormComponent', () => {
  let component: EggFormComponent;
  let fixture: ComponentFixture<EggFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EggFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EggFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
