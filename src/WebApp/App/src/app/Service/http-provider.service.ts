import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiService } from './web-api.service';

var apiUrl = "http://localhost:5000";

var httpLink = {
  getDrivers: apiUrl + "/drivers",
  deleteDriver: apiUrl + "/drivers",
  getDriverById: apiUrl + "/drivers/",
  createDriver: apiUrl + "/drivers",
  randomBulkSeed: apiUrl + "/drivers/random-bulk-seed"
}

@Injectable({
  providedIn: 'root'
})

export class HttpProviderService {
  constructor(private webApiService: WebApiService) { }

  public getDrivers(model: any): Observable<any> {
    var url = httpLink.getDrivers;
    if (!model) {
      return this.webApiService.get(url);
    }
    // Initialize an empty array to store query parameters
    let queryParams = [];

    // Iterate over the properties of the model
    for (const key in model) {
      // Check if the property exists and is not null or undefined
      if (model.hasOwnProperty(key) && model[key] != null) {
        // Add the property and its value to the query parameters
        queryParams.push(`${key}=${encodeURIComponent(model[key])}`);
      }
    }

    if (queryParams.length > 0) {
      url = httpLink.getDrivers + `?${queryParams.join('&')}`;
    }

    return this.webApiService.get(url);
  }
  public deleteDriver(model: any): Observable<any> {
    return this.webApiService.post(httpLink.deleteDriver + '?Id=' + model, "");
  }
  public getDriverById(model: any): Observable<any> {
    return this.webApiService.get(httpLink.getDriverById + model);
  }
  public createDriver(model: any): Observable<any> {
    return this.webApiService.post(httpLink.createDriver, model);
  }
  public CreateRandomDriversBulk(model: any): Observable<any> {
    return this.webApiService.post(httpLink.randomBulkSeed, model);
  }
}
