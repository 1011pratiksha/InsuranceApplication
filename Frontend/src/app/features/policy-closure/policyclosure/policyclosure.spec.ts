import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Policyclosure } from './policyclosure';

describe('Policyclosure', () => {
  let component: Policyclosure;
  let fixture: ComponentFixture<Policyclosure>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Policyclosure]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Policyclosure);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
