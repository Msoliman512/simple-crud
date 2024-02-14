import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UploadTaskComponent } from './upload-task.component';
import { LoggingService } from '../Service/logging.service';
import { HttpClientModule } from '@angular/common/http';
import { ElementRef } from '@angular/core';

describe('UploadTaskComponent', () => {
  let component: UploadTaskComponent;
  let fixture: ComponentFixture<UploadTaskComponent>;
  let loggingService: LoggingService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule, ReactiveFormsModule, HttpClientModule],
      declarations: [UploadTaskComponent],
      providers: [LoggingService]
    })
      .compileComponents();

    fixture = TestBed.createComponent(UploadTaskComponent);
    component = fixture.componentInstance;
    loggingService = TestBed.inject(LoggingService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should validate file extension', () => {
    expect(component.isValidFileExtension('test.txt')).toBeTrue();
    expect(component.isValidFileExtension('test.pdf')).toBeFalse();
  });

  // count words need integrations test and complicated mocking; to-be done later
  it('should count words', () => {
    component.fileContent = 'Hello world! Hello again.';
    component.wordCountMap = {}; // Initialize wordCountMap
    component.countWords(component.fileContent);
    expect(component.wordCountMap['Hello']).toEqual(2);
    expect(component.wordCountMap['world!']).toEqual(1);
    expect(component.wordCountMap['again.']).toEqual(1);
  });

  it('should reset file', () => {
    spyOn(loggingService, 'logEvent');
    component.resetFile();
    expect(component.fileContent).toEqual('');
    expect(component.wordCountMap).toEqual({});
    expect(component.isUploading).toBeFalse();
    expect(component.uploadProgress).toEqual(0);
    expect(loggingService.logEvent).toHaveBeenCalledWith('File reset');
  });
  
  // count words need integrations test and complicated mocking; to-be done later (need tomock file reader)
  it('should set fileContent and wordCountMap when file with valid extension is selected', () => {
    const component = new UploadTaskComponent(loggingService);
    const file = new File(['file content'], 'file.txt', { type: 'text/plain' });
    const event = { target: { files: [file] } };

    component.onFileSelected(event);

    expect(component.fileContent).toEqual('file content');
    expect(component.wordCountMap).toEqual({});
  });

  it('should set fileControl errors and log info when no file is selected', () => {
    const component = new UploadTaskComponent(loggingService);
    const event = { target: { files: [] } };

    // Define fileInput before calling onFileSelected
    component.fileInput = new ElementRef(document.createElement('input'));

    // Create a spy for logInfo
    spyOn(component.loggingService, 'logInfo');

    component.onFileSelected(event);

    expect(component.fileControl.errors).toEqual({ required: true });
    expect(component.loggingService.logInfo).toHaveBeenCalledWith('No file selected');
  });

  it('should set fileControl errors, reset input value and fileContent, and log info when file with invalid extension is selected', () => {
    const component = new UploadTaskComponent(loggingService);
    const file = new File(['file content'], 'file.jpg', { type: 'image/jpeg' });
    const event = { target: { files: [file] } };

    // Define fileInput before calling onFileSelected
    component.fileInput = new ElementRef(document.createElement('input'));

    // Create a spy for logInfo
    spyOn(component.loggingService, 'logInfo');

    component.onFileSelected(event);

    expect(component.fileControl.errors).toEqual({ invalidExtension: true });
    expect(component.fileInput.nativeElement.value).toEqual('');
    expect(component.fileContent).toEqual('');
    expect(component.loggingService.logInfo).toHaveBeenCalledWith('Invalid File selected (Invalid Extension)', 'file.jpg');
  });

  // count words need integrations test and complicated mocking; to-be done later
  it('should count words and set isUploading to false when FileReader onload is triggered', (done) => {

    const component = new UploadTaskComponent(loggingService);
    const file = new File(['file content'], 'file.txt', { type: 'text/plain' });
    const event = { target: { files: [file] } };

    // Create a mock FileReader
    const reader = jasmine.createSpyObj('FileReader', ['readAsText', 'onload']);

    spyOn(window, 'FileReader').and.returnValue(reader);

    // Simulate FileReader onload event
    reader.onload.and.callFake(() => {
      component.onFileSelected(event);
      expect(component.fileContent).toEqual('file content');
      expect(component.wordCountMap).toEqual({ file: 2, content: 1 });
      expect(component.isUploading).toBe(false);
      done(); // Tell Jasmine that the asynchronous operation has completed
    });

    component.onFileSelected(event);
  });

  it('should reset fileInput value, fileContent, wordCountMap, isUploading, and uploadProgress and log event when resetFile is called', () => {
    const component = new UploadTaskComponent(loggingService);

    component.fileInput = { nativeElement: { value: 'file.txt' } } as ElementRef;
    component.fileContent = 'file content';
    component.wordCountMap = { 'word': 1 };
    component.isUploading = true;
    component.uploadProgress = 50;

    spyOn(loggingService, 'logEvent'); // Create a spy for logEvent

    component.resetFile();

    expect(component.fileInput.nativeElement.value).toEqual('');
    expect(component.fileContent).toEqual('');
    expect(component.wordCountMap).toEqual({});
    expect(component.isUploading).toBe(false);
    expect(component.uploadProgress).toBe(0);
    expect(loggingService.logEvent).toHaveBeenCalledWith('File reset'); // Now logEvent is a spy
  });

});
