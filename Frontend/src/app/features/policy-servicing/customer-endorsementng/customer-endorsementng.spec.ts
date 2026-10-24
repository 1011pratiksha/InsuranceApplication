import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerEndorsementng } from './customer-endorsementng';

describe('CustomerEndorsementng', () => {
  let component: CustomerEndorsementng;
  let fixture: ComponentFixture<CustomerEndorsementng>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerEndorsementng]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomerEndorsementng);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
