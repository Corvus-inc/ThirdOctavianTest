import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormDeptComponent } from './form-dept.component';

describe('FormDeptComponent', () => {
  let component: FormDeptComponent;
  let fixture: ComponentFixture<FormDeptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormDeptComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormDeptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
