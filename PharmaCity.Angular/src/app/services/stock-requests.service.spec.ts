import { TestBed } from '@angular/core/testing';

import { StockRequestsService } from './stock-requests.service';

describe('StockRequestsService', () => {
  let service: StockRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StockRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
