import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing'; // import HttpClientTestingModule

import { WebApiService } from './web-api.service';

describe('WebApiService', () => {
  let service: WebApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule], // add HttpClientTestingModule to the imports array
    });
    service = TestBed.inject(WebApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});