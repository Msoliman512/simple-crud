import { Component, Input, OnInit, Type, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'ng-modal-confirm',
  template: `
    <div class="modal-header">
      <h5 class="modal-title" id="modal-title">Delete Confirmation</h5>
      <button type="button" class="btn close" aria-label="Close button" aria-describedby="modal-title" (click)="dismissModal()">
        <span aria-hidden="true">×</span>
      </button>
    </div>
    <div class="modal-body">
      <p>Are you sure you want to delete?</p>
    </div>
    <div class="modal-footer">
      <button type="button" class="btn btn-outline-secondary" (click)="modal.dismiss('cancel click')">CANCEL</button>
      <button type="button" ngbAutofocus class="btn btn-success" (click)="confirmDelete()">OK</button>
    </div>
  `,
})

export class NgModalConfirm {
  constructor(public modal: NgbActiveModal) { }
  dismissModal() {
    this.modal.dismiss('Cross click');
  }
  confirmDelete() {
    this.modal.close('Ok click');
  }
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
  @ViewChild('bulkAddModal') bulkAddModal: any;
  bulkAddForm!: FormGroup; // Define form group for bulk add
  submitted = false;
  constructor(private router: Router,
              private modalService: NgbModal,
              private toastr: ToastrService,
              private httpProvider : HttpProviderService,
              private formBuilder: FormBuilder
  ) {
    this.bulkAddForm = this.formBuilder.group({
      numberOfDrivers: ['', [Validators.required, Validators.min(1), Validators.max(10)]] // Add validation for number of drivers
    });
  }

  // Convenience getter for easy access to form fields
  get f() { return this.bulkAddForm.controls; }

  // Method to handle form submission
  onSubmit() {
    this.submitted = true;

    // Stop here if form is invalid
    if (this.bulkAddForm.invalid) {
      return;
    }

    // Form is valid, proceed with your logic here
    const count = this.bulkAddForm.value.numberOfDrivers;
    this.BulkRandomDriversInsertion(count);
  }

  // Open modal for bulk adding drivers
  // Open modal for bulk adding drivers
  openBulkAddModal(content: any) {

    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      if (result === 'save') {
        if (this.bulkAddForm.invalid) {
          return;
        }
        const count = this.bulkAddForm.value.numberOfDrivers;
        this.BulkRandomDriversInsertion(count);
      } else {
        this.resetForm();
      }
    }, (reason) => {
      this.resetForm();
      console.log(`Dismissed with reason: ${reason}`);
    });
  }




  resetForm() {
    this.bulkAddForm.reset();
    this.submitted = false;
  }

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
    const formData = new FormData();
    formData.append("Count", count.toString());
    console.log(formData);
    this.httpProvider.CreateRandomDriversBulk(formData).subscribe((data : any) => {
          if (data != null && data.body != null) {
            var resultData = data;
            if (resultData != null && resultData.body.rowsInserted > 0) {
              this.toastr.success("Drivers Added successfully");
              this.modalService.dismissAll();
              this.getAllDrivers(this.model);
            }
          }
        },
        (error : any) => {
          console.log(error);
          this.toastr.error("Error occurred while Adding Random drivers Bulk");
        });
  }

}
