import { Component, Input, OnInit, Type } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';

@Component({
  selector: 'ng-modal-confirm',
  template: `
  <div class="modal-header">
    <h5 class="modal-title" id="modal-title">Delete Confirmation</h5>
    <button type="button" class="btn close" aria-label="Close button" aria-describedby="modal-title" (click)="modal.dismiss('Cross click')">
      <span aria-hidden="true">×</span>
    </button>
  </div>
  <div class="modal-body">
    <p>Are you sure you want to delete?</p>
  </div>
  <div class="modal-footer">
    <button type="button" class="btn btn-outline-secondary" (click)="modal.dismiss('cancel click')">CANCEL</button>
    <button type="button" ngbAutofocus class="btn btn-success" (click)="modal.close('Ok click')">OK</button>
  </div>
  `,
})

export class NgModalConfirm {
  constructor(public modal: NgbActiveModal) { }
}

const MODALS: { [name: string]: Type<any> } = {
  deleteModal: NgModalConfirm,
};

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  closeResult = '';
  driversList: any[] = [];
  model = {
    PageIndex: 1,
    PageSize: 10,
    Keyword: null,  //search keyword
    OrderByColumn: null, // column name
    OrderBy: null //bool
  };
  constructor(private router: Router, private modalService: NgbModal,
              private toastr: ToastrService, private httpProvider : HttpProviderService) { }

  ngOnInit(): void {
    this.getAllDrivers(this.model );
  }
  async getAllDrivers(model: any) {
    this.httpProvider.getDrivers(model).subscribe((data : any) => {
        if (data && data.body ) {
          var resultData = data.body.items;
          if (resultData) {
            this.driversList = resultData;
          }
        }
      },
      (error : any)=> {
        if (error) {
          if (error.status == 404) {
            if(error.error && error.error.message){
              this.driversList = [];
            }
          }
        }
      });
  }

  AddDriver() {
    this.router.navigate(['AddDriver']);
  }

  deleteDriverConfirmation(driver: any) {
    this.modalService.open(MODALS['deleteModal'],
      {
        ariaLabelledBy: 'modal-basic-title'
      }).result.then((result: any) => {
        this.deleteDriver(driver);
      },
      (reason: any) => {});
  }

  deleteDriver(driver: any) {
    this.httpProvider.deleteDriver(driver.id).subscribe((data : any) => {
        if (data != null && data.body != null) {
          var resultData = data.body;
          if (resultData != null && resultData.isSuccess) {
            this.toastr.success(resultData.message);
            this.getAllDrivers(this.model);
          }
        }
      },
      (error : any) => {});
  }
}
