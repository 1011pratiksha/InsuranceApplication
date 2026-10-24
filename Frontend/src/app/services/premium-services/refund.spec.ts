import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { RefundService } from './refund';

describe('RefundService', () => {
  let service: RefundService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        RefundService,
        provideHttpClient()
      ]
    });

    service = TestBed.inject(RefundService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});