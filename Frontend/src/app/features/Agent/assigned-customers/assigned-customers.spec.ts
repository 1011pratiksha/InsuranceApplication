import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AssignedCustomersComponent } from './assigned-customers';

describe('AssignedCustomersComponent', () => {
  let component: AssignedCustomersComponent;
  let fixture: ComponentFixture<AssignedCustomersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignedCustomersComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(AssignedCustomersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});