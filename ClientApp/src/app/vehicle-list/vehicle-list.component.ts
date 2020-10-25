import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { KeyValuePair } from '../models/KeyValuePair';
import { vehicle } from '../models/vehicle';
import { VehicleServise } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {

  private readonly PAGE_SIZE = 10
  queryResoult: any = {}
  makes: KeyValuePair
  query: any = {
    pageSize: this.PAGE_SIZE,
  }

  constructor(private vehicleService: VehicleServise) { }

  ngOnInit() {
    var sourses = [
      this.vehicleService.getMakes()
    ]

    forkJoin(sourses).subscribe((data: any[]) => {
      this.makes = data[0]
    })

    this.populateVehicles()
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateVehicles()
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    }
    this.populateVehicles()
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName)
      this.query.isSortAssending = !this.query.isSortAssending;
    else {
      this.query.sortBy = columnName;
      this.query.isSortAssending = false;
    }

    this.populateVehicles()
  }

  onPageChange(page) {
    this.query.page = page;
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query).subscribe((resoult: any[]) => this.queryResoult = resoult);
  }
}
