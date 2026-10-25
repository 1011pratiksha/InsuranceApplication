import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentCommissions } from './agent-commissions';

describe('AgentCommissions', () => {
  let component: AgentCommissions;
  let fixture: ComponentFixture<AgentCommissions>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgentCommissions]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgentCommissions);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
