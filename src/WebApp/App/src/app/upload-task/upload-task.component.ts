import { Component, ElementRef, ViewChild } from '@angular/core';
import {AbstractControl, FormControl, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-upload-task',
  templateUrl: './upload-task.component.html',
  styleUrls: ['./upload-task.component.scss']
})
export class UploadTaskComponent {
  fileContent: string | ArrayBuffer = '';
  wordCountMap: Map<string, number> = new Map();
  isUploading: boolean = false;
  uploadProgress: number = 0;

  fileControl = new FormControl('', [Validators.required, this.fileValidator()]);

  @ViewChild('fileInput') fileInput!: ElementRef;

  constructor() { }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    // Check if no file is selected
    if (!file) {
      this.fileControl.setErrors({ 'required': true });
      return;
    }

    // Check if the file has a valid extension (.txt)
    if (!this.isValidFileExtension(file.name)) {
      this.fileControl.setErrors({ 'invalidExtension': true });
      this.fileInput.nativeElement.value = ''; // Reset input value
      this.fileContent = ''; // Reset file content
      return;
    }

    // If the file is valid, clear any existing errors
    this.fileControl.setErrors(null);
    
    const reader: FileReader = new FileReader();

    reader.onload = (e: any) => {
      this.fileContent = e.target.result;
      this.countWords();
    };

    reader.onerror = (e: ProgressEvent<FileReader>) => {
      alert('Error reading file. Please make sure the file is not empty and try again.');
    };

    reader.readAsText(file);
  }
  fileValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const file = control.value;
      if (!file) {
        return { 'required': true };
      }
      if (!this.isValidFileExtension(file.name)) {
        return { 'invalidExtension': true };
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
    if(words.length === 0)
       this.fileContent = 'No content';
    this.wordCountMap.clear();
    words.forEach(word => {
      this.wordCountMap.set(word, (this.wordCountMap.get(word) || 0) + 1);
    });
    this.isUploading = false; // Set to false after content is loaded
    this.uploadProgress = 0; // Reset progress
  }

  updateProgress(loaded: number, total: number) {
    if (this.uploadProgress < 100) {
      setTimeout(() => {
        this.uploadProgress = Math.min((loaded / total) * 100, 100);
        this.updateProgress(loaded, total); // Call the function recursively
      }, 200); // Increase the delay to slow down the progress update
    }
  }

  resetFile() {
    this.fileInput.nativeElement.value = '';
    this.fileContent = '';
    this.wordCountMap.clear();
    this.isUploading = false;
    this.uploadProgress = 0;
  }
}
