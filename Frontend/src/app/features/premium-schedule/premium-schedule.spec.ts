import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PremiumSchedule } from './premium-schedule';

describe('PremiumSchedule', () => {
  let component: PremiumSchedule;
  let fixture: ComponentFixture<PremiumSchedule>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PremiumSchedule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PremiumSchedule);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
