import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Policyserviceingss } from './policyserviceingss';

describe('Policyserviceingss', () => {
  let component: Policyserviceingss;
  let fixture: ComponentFixture<Policyserviceingss>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Policyserviceingss]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Policyserviceingss);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
