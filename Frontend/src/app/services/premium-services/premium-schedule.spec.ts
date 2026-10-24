import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { PremiumScheduleService } from './premium-schedule';

describe('PremiumScheduleService', () => {
  let service: PremiumScheduleService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        PremiumScheduleService,
        provideHttpClient()
      ]
    });

    service = TestBed.inject(PremiumScheduleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});