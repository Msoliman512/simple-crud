import { Component, ElementRef, ViewChild} from '@angular/core';
import {AbstractControl, FormControl, ValidatorFn, Validators} from '@angular/forms';
import {LoggingService} from '../Service/logging.service';

@Component({
  selector: 'app-upload-task',
  templateUrl: './upload-task.component.html',
  styleUrls: ['./upload-task.component.scss']
})
export class UploadTaskComponent  {
  fileContent: string | ArrayBuffer = '';
  wordCountMap: { [word: string]: number } = {}; // changed for map to this to solve NG0100: ExpressionChangedAfterItHasBeenCheckedError
  isUploading: boolean = false;
  uploadProgress: number = 0;

  fileControl = new FormControl('', [Validators.required, this.fileValidator()]);

  @ViewChild('fileInput') fileInput!: ElementRef;

  constructor(
    public loggingService: LoggingService,
  ) {
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    this.loggingService.logEvent('File selected', file.name);
    // Check if no file is selected
    if (!file) {
      this.fileControl.setErrors({'required': true});
      this.loggingService.logInfo('No file selected');
      return;
    }

    // Check if the file has a valid extension (.txt)
    if (!this.isValidFileExtension(file.name)) {
      this.fileControl.setErrors({'invalidExtension': true});
      this.loggingService.logInfo('Invalid File selected (Invalid Extension)', file.name);
      this.fileInput.nativeElement.value = ''; // Reset input value
      this.fileContent = ''; // Reset file content
      return;
    }

    // If the file is valid, clear any existing errors
    this.fileControl.setErrors(null);
    this.loggingService.logInfo('valid File selected', file.name);
    const reader: FileReader = new FileReader();

    reader.onload = (e: any) => {
            this.fileContent = e.target.result;
            this.countWords();
    };

    reader.onerror = (e: ProgressEvent<FileReader>) => {
      alert('Error reading file. Please make sure the file is not empty and try again.');
      this.loggingService.logError('Error Reading file', file.name);
    };

    reader.readAsText(file);
  }

  fileValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const file = control.value;
      if (!file) {
        return {'required': true};
      }
      if (!this.isValidFileExtension(file.name)) {
        return {'invalidExtension': true};
      }
      return null;
    };
  }

  isValidFileExtension(fileName: string): boolean {
    const allowedExtensions = ['txt'];
    const fileExtension = fileName.slice((fileName.lastIndexOf('.') - 1 >>> 0) + 2);
    return allowedExtensions.includes(fileExtension.toLowerCase());
  }

  countWords() {
    const words = this.fileContent.toString().toLowerCase().split(/\s+/).filter(Boolean);
    if (words.length === 0) {
      this.fileContent = 'No content';
      this.loggingService.logInfo('Empty File');
    }
    // Reset wordCountMap object
    this.wordCountMap = {};

    // Count words
    words.forEach(word => {
      this.wordCountMap[word] = (this.wordCountMap[word] || 0) + 1;
    });

    this.isUploading = false; // Set to false after content is loaded
    this.uploadProgress = 0; // Reset progress
  }

  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }


  updateProgress(loaded: number, total: number) {
    if (this.uploadProgress < 100) {
      setTimeout(() => {
        this.uploadProgress = Math.min((loaded / total) * 100, 100);
        this.updateProgress(loaded, total);
      }, 200);
    }
  }

  resetFile() {
    this.fileInput.nativeElement.value = '';
    this.fileContent = '';
    this.wordCountMap = {};
    this.isUploading = false;
    this.uploadProgress = 0;
    this.loggingService.logEvent('File reset');
  }
}
