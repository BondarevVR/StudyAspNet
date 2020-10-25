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

  getVehicles(filter) {
    return this.http.get('/api/vehicles' + '?' + this.toQueryString(filter))
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) {
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
      }
    }

    return parts.join('&');
  }

  create(vehicle) {
    return this.http.post('/api/vehicles', vehicle)
  }

  getVehicle(id) {
    return this.http.get('/api/vehicles/' + id);
  }

  update(vehicle) {
    return this.http.put('/api/vehicles/' + vehicle.id, vehicle);
  }

  delete(id) {
    return this.http.delete('/api/vehicles/' + id);
  }
}
