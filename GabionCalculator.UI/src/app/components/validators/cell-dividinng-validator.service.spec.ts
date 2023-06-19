import { TestBed } from '@angular/core/testing';

import { CellDividinngValidatorService } from './cell-dividinng-validator.service';

describe('CellDividinngValidatorService', () => {
  let service: CellDividinngValidatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CellDividinngValidatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
