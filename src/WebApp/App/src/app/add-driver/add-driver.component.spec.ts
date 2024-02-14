// add-driver.component.spec.ts
import { TestBed, async } from '@angular/core/testing';
import { AddDriverComponent } from './add-driver.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('AddDriverComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgbModule,
        ToastrModule.forRoot(),
        FormsModule,
        ReactiveFormsModule
      ],
      declarations: [
        AddDriverComponent
      ],
    }).compileComponents();
  }));

  it('should create', () => {
    const fixture = TestBed.createComponent(AddDriverComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });
});