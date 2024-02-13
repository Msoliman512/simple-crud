import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AddDriverComponent } from './add-driver/add-driver.component';
import { ViewDriverComponent } from './view-driver/view-driver.component';
import { EditDriverComponent } from './edit-driver/edit-driver.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UploadTaskComponent } from './upload-task/upload-task.component';

@NgModule({
  declarations: [
    HomeComponent,
    AppComponent,
    AddDriverComponent,
    ViewDriverComponent,
    EditDriverComponent,
    UploadTaskComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    ToastrModule.forRoot({positionClass: 'toast-bottom-right'}),
    BrowserAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
  ],
  providers: [
    provideClientHydration()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
