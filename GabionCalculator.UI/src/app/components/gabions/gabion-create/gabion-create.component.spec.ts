import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GabionCreateComponent } from './gabion-create.component';

describe('GabionCreateComponent', () => {
  let component: GabionCreateComponent;
  let fixture: ComponentFixture<GabionCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GabionCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GabionCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
