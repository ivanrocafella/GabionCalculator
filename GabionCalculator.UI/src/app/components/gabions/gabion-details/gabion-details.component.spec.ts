import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GabionDetailsComponent } from './gabion-details.component';

describe('GabionDetailsComponent', () => {
  let component: GabionDetailsComponent;
  let fixture: ComponentFixture<GabionDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GabionDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GabionDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
