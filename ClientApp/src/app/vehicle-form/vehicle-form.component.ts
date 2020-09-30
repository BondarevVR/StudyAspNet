import { Component, OnInit } from '@angular/core';
import { VehicleServise } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any[];
  models: any[];
  feachers: any[];
  vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(private vehicleService: VehicleServise) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe((makes: any[]) => this.makes = makes)
    this.vehicleService.getFeature().subscribe((feachers: any[]) => this.feachers = feachers)
  }

  onMakeChange() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeID);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelID;
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    } else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    this.vehicleService.create(this.vehicle).subscribe(x => console.log(x));
  }
}
