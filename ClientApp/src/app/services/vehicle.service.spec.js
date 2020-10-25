"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var vehicle_service_1 = require("./vehicle.service");
describe('MakeService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(vehicle_service_1.VehicleServise);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=vehicle.service.spec.js.map