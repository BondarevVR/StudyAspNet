import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleServise } from '../services/vehicle.service';
import { forkJoin } from 'rxjs';
import { saveVehicle } from "../models/saveVehicle";
import { vehicle } from "../models/vehicle";
import * as _ from "underscore"

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes: any[];
  models: any[];
  feachers: any[];
  vehicle: saveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: ''
    }
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

      if (this.vehicle.id) {
        this.setVehicle(data[2]);
        this.populateModels();
      }
    }, err => {
        if (err.status == 404) {
          this.router.navigate(['']);
        }
    })
  }

  private setVehicle(v: vehicle) {
      this.vehicle.id = v.id,
      this.vehicle.makeId = v.make.id,
      this.vehicle.modelId = v.model.id,
      this.vehicle.isRegistered = v.isRegistered,
      this.vehicle.contact = v.contact,
      this.vehicle.features = _.pluck(v.features, 'id')
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
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
    var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
    result$.subscribe(vehicle => {
      console.log(vehicle);
      this.router.navigate(['/vehicles/', this.vehicle.id]);
    });

    
    /*if (this.vehicle.id) {
      this.vehicleService.update(this.vehicle).subscribe(x => console.log(x));
    }
    this.vehicleService.create(this.vehicle).subscribe(x => console.log(x));*/
  }

  /*delete() {
    if (confirm("Are you shure?"))
      this.vehicleService.delete(this.vehicle.id).subscribe(x => {
        console.log("Deleted sucsesfully");
        this.router.navigate(['']);
      });
  }*/
}
