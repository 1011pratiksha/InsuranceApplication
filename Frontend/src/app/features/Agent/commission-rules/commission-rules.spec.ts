import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommissionRules } from './commission-rules';

describe('CommissionRules', () => {
  let component: CommissionRules;
  let fixture: ComponentFixture<CommissionRules>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommissionRules]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommissionRules);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
