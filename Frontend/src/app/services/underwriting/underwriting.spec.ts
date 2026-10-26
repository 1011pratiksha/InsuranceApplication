import { TestBed } from '@angular/core/testing';

import { Underwriting } from './underwriting';

describe('Underwriting', () => {
  let service: Underwriting;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Underwriting);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
