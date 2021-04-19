import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultTimeComponent } from './result-time.component';

describe('ResultTimeComponent', () => {
  let component: ResultTimeComponent;
  let fixture: ComponentFixture<ResultTimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResultTimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
