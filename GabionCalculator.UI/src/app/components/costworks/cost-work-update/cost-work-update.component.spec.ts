import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CostWorkUpdateComponent } from './cost-work-update.component';

describe('CostWorkUpdateComponent', () => {
  let component: CostWorkUpdateComponent;
  let fixture: ComponentFixture<CostWorkUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CostWorkUpdateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CostWorkUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
