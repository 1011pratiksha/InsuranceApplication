import { TestBed } from '@angular/core/testing';

import { CommissionRule } from './commission-rule';

describe('CommissionRule', () => {
  let service: CommissionRule;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CommissionRule);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
