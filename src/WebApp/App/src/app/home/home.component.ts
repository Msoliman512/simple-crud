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
    PageSize: 5,
    Keyword: null,  //search keyword
    OrderByColumn: null as string | null, // column name
    OrderBy: null as boolean | null //bool
  };
  constructor(private router: Router,
              private modalService: NgbModal,
              private toastr: ToastrService,
              private httpProvider : HttpProviderService) { }

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
  onPageChange(pageIndex: number) {
    this.model.PageIndex = pageIndex;
    this.getAllDrivers(this.model);
  }
  onPageSizeChange() {
    this.model.PageIndex = 1; // Reset the page index to 1 when changing page size
    this.getAllDrivers(this.model);
  }
  onSort(columnName: string) {
    if (this.model.OrderByColumn === columnName) {
      this.model.OrderBy = this.model.OrderBy === true ? false : true;
    } else {
      this.model.OrderByColumn = columnName;
      this.model.OrderBy = true;
    }
    this.getAllDrivers(this.model);
  }
  reset() {
    // Reset all settings to their default values
    this.model.PageSize = 5; // Set default page size
    this.model.PageIndex = 1; // Set default page index
    this.model.Keyword = null; // Reset search keyword
    this.model.OrderByColumn = null; // Reset sorting column
    this.model.OrderBy = null; // Reset sorting order
    this.getAllDrivers(this.model); // Refresh data
  }

  onSearch() {
    this.getAllDrivers(this.model);
  }
  AddDriver() {
    this.router.navigate(['AddDriver']);
  }
  BulkAddDrivers() {

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
          var resultData = data;
          if (resultData != null && resultData.body.result == true) {
            this.toastr.success("Driver deleted successfully");
            this.getAllDrivers(this.model);
          }
        }
      },
      (error : any) => {
        console.log(error);
        this.toastr.error("Error occurred while deleting driver");
      });
  }

  BulkRandomDriversInsertion(count: any) {
    this.httpProvider.CreateRandomDriversBulk(count).subscribe((data : any) => {
          debugger
          if (data != null && data.body != null) {
            var resultData = data;
            if (resultData != null && resultData.body.result > 0) {
              this.toastr.success("Driver deleted successfully");
              this.getAllDrivers(this.model);
            }
          }
        },
        (error : any) => {
          debugger
          console.log(error);
          this.toastr.error("Error occurred while Adding Random drivers Bulk");
        });
  }

}
