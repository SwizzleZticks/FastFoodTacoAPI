import { TestBed } from '@angular/core/testing';

import { DrinkApiService } from './drink-api.service';

describe('DrinkApiService', () => {
  let service: DrinkApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DrinkApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
