import { TestBed } from '@angular/core/testing';

import { VehicleServise } from './vehicle.service';

describe('MakeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleServise = TestBed.get(VehicleServise);
    expect(service).toBeTruthy();
  });
});
