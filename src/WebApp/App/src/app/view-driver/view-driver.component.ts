import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpProviderService } from '../Service/http-provider.service';
import { WebApiService } from '../Service/web-api.service';
@Component({
  selector: 'app-view-driver',
  templateUrl: './view-driver.component.html',
  styleUrl: './view-driver.component.scss'
})
export class ViewDriverComponent implements OnInit {
  driverId: any;
  driverDetail : any= [];

  constructor(public webApiService: WebApiService, private route: ActivatedRoute, private httpProvider : HttpProviderService) { }

  ngOnInit(): void {
    this.driverId = this.route.snapshot.params['driverId'];
    this.getDriverDetailById();
  }

  getDriverDetailById() {
    this.httpProvider.getDriverById(this.driverId).subscribe((data : any) => {
        if (data != null && data.body != null) {
          var resultData = data.body;
          if (resultData) {
            this.driverDetail = resultData;
          }
        }
      },
      (error :any)=> { });
  }

}
