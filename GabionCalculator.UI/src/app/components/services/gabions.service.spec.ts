import { TestBed } from '@angular/core/testing';

import { GabionsService } from './gabions.service';

describe('GabionsService', () => {
  let service: GabionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GabionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
