import { TestBed } from '@angular/core/testing';

import { AssignedCustomers } from './assigned-customers';

describe('AssignedCustomers', () => {
  let service: AssignedCustomers;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssignedCustomers);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
