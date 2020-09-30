import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VehicleServise {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get('/api/makes')
  }

  getFeature() {
    return this.http.get('/api/features')
  }

  create(vehicle) {
    return this.http.post('/api/vehicles', vehicle)
  }
}