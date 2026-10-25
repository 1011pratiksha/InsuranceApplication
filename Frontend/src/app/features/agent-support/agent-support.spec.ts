import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentSupport } from './agent-support';

describe('AgentSupport', () => {
  let component: AgentSupport;
  let fixture: ComponentFixture<AgentSupport>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AgentSupport]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgentSupport);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
