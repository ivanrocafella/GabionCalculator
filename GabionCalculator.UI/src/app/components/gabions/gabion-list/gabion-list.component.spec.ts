import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GabionListComponent } from './gabion-list.component';

describe('GabionListComponent', () => {
  let component: GabionListComponent;
  let fixture: ComponentFixture<GabionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GabionListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GabionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
