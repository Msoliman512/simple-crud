import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';
@Component({
  selector: 'app-add-driver',
  templateUrl: './add-driver.component.html',
  styleUrl: './add-driver.component.scss'
})
export class AddDriverComponent {
  addDriverForm: driverForm = new driverForm();

  @ViewChild("driverForm")
  driverForm!: NgForm;
  isSubmitted: boolean = false;
  constructor(private router: Router, private httpProvider: HttpProviderService, private toastr: ToastrService) { }

  ngOnInit(): void {  }

  AddDriver(isValid: any) {
    this.isSubmitted = true;
    if (isValid) {
      const formData = new FormData();
      formData.append("FirstName", this.addDriverForm.FirstName);
      formData.append("LastName", this.addDriverForm.LastName);
      formData.append("Email", this.addDriverForm.Email);
      if (this.addDriverForm.PhoneNumber != null)
       formData.append("PhoneNumber", this.addDriverForm.PhoneNumber);
      this.httpProvider.createDriver(formData).subscribe(async data => {
            if (data != null && data.body != null) {
              var resultData = data.body
              if (data.ok && resultData.id > 0) {
                this.toastr.success("Driver added successfully");
                setTimeout(() => {
                  this.router.navigate(['/Home']);
                }, 500);
              }
            }
        },
        async error => {
        console.log(error);
          this.toastr.error("Error while adding driver");
          setTimeout(() => {
            this.router.navigate(['/Home']);
          }, 500);
        });
    }
  }
}


export class driverForm {
  FirstName: string = "";
  LastName: string = "";
  Email: string = "";
  PhoneNumber: string = "";
}
