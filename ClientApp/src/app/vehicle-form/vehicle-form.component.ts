import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { VehicleServise } from '../services/vehicle.service';
import { forkJoin } from 'rxjs';

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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleServise){
    //if (this.vehicle.id)
    /*this.route.params.subscribe(p => {
          this.vehicle.id = +p['id'];
        });*/
    //this.vehicle.id = route.params['id'];
  }

  ngOnInit() {

    this.vehicle.id = this.route.snapshot.params['id'];

    var sourses = [
      this.vehicleService.getFeature(),
      this.vehicleService.getMakes(),
    ];

    if (this.vehicle.id) {
      sourses.push(this.vehicleService.getVehicle(this.vehicle.id));
    }

    forkJoin(sourses).subscribe((data: any[]) => {
      this.feachers = data[0];
      this.makes = data[1];

      if (this.vehicle.id)
        this.vehicle = data[2];
    }, err => {
        if (err.status == 404) {
          this.router.navigate(['']);
        }
    })
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
