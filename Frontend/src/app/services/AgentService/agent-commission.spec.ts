import { TestBed } from '@angular/core/testing';

import { AgentCommission } from './agent-commission';

describe('AgentCommission', () => {
  let service: AgentCommission;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AgentCommission);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
