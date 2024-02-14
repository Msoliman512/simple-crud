import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  logError(message: string, data?: any) {
    console.error(`Error: ${message}`, data);
  }

  logInfo(message: string, data?: any) {
    console.info(`Info: ${message}`, data);
  }

  logEvent(message: string, data?: any) {
    console.log(`Event: ${message}`, data);
  }

  // this section for sneeding logs to server if we want.
  private logEndpoint = 'http://your-server.com/log';
  constructor(private http: HttpClient) { }

  sendlogError(message: string, data?: any) {
    this.sendLog('ERROR', message, data);
  }

  sendLogInfo(message: string, data?: any) {
    this.sendLog('INFO', message, data);
  }

  sendLogEvent(message: string, data?: any) {
    this.sendLog('EVENT', message, data);
  }

  private sendLog(level: string, message: string, data?: any) {
    const logMessage = {
      timestamp: new Date().toISOString(),
      level,
      message,
      data
    };

    this.http.post(this.logEndpoint, logMessage).subscribe();
  }
}