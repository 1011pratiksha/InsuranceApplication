import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Adminservicerequest } from './adminservicerequest';

describe('Adminservicerequest', () => {
  let component: Adminservicerequest;
  let fixture: ComponentFixture<Adminservicerequest>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Adminservicerequest]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Adminservicerequest);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
