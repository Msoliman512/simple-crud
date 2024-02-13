import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';
@Component({
  selector: 'app-edit-driver',
  templateUrl: './edit-driver.component.html',
  styleUrl: './edit-driver.component.scss'
})
export class EditDriverComponent {
  editDriverForm: driverForm = new driverForm();

  @ViewChild("driverForm")
  driverForm!: NgForm;

  isSubmitted: boolean = false;
  driverId: any;

  constructor(private toastr: ToastrService, private route: ActivatedRoute, private router: Router,
              private httpProvider: HttpProviderService) { }

  ngOnInit(): void {
    this.driverId = this.route.snapshot.params['driverId'];
    this.getDriverDetailById();
  }
  getDriverDetailById() {
    this.httpProvider.getDriverById(this.driverId).subscribe((data: any) => {
        if (data != null && data.body != null) {
          var resultData = data.body;
          if (resultData) {
            this.editDriverForm.Id = resultData.id;
            this.editDriverForm.FirstName = resultData.firstName;
            this.editDriverForm.LastName = resultData.lastName;
            this.editDriverForm.Email = resultData.email;
            this.editDriverForm.PhoneNumber = resultData.phoneNumber;
          }
        }
      },
      (error: any) => {
        console.log(error.message);
        this.toastr.error("Error while getting driver details");
        setTimeout(() => {
          this.router.navigate(['/Home']);
        }, 500);
      });
  }

  EditDriver(isValid: any) {
    this.isSubmitted = true;
    if (isValid) {
      const formData = new FormData();
      formData.append("Id", this.editDriverForm.Id.toString());
      formData.append("FirstName", this.editDriverForm.FirstName);
      formData.append("LastName", this.editDriverForm.LastName);
      formData.append("Email", this.editDriverForm.Email);
      if (this.editDriverForm.PhoneNumber != null)
        formData.append("PhoneNumber", this.editDriverForm.PhoneNumber);
      this.httpProvider.updateDriver(formData).subscribe(async data => {
          if (data != null && data.body != null) {
            var resultData = data.body
            if (data.ok && resultData.result) {
              this.toastr.success("Driver updated successfully");
              setTimeout(() => {
                this.router.navigate(['/Home']);
              }, 500);
            }
          }
        },
        async error => {
          console.log(error);
          this.toastr.error("Error while updating driver");
          setTimeout(() => {
            this.router.navigate(['/Home']);
          }, 500);
        });
    }
  }
}

export class driverForm {
  Id: number = 0;
  FirstName: string = "";
  LastName: string = "";
  Email: string = "";
  PhoneNumber: string = "";
}
