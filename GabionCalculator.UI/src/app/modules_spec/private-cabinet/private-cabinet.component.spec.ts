import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrivateCabinetComponent } from './private-cabinet.component';

describe('PrivateCabinetComponent', () => {
  let component: PrivateCabinetComponent;
  let fixture: ComponentFixture<PrivateCabinetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrivateCabinetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrivateCabinetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
