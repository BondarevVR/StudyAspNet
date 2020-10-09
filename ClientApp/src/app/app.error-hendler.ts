//import * as Sentry from "@sentry/angular";
import { ErrorHandler } from '@angular/core'

export class AppErrorHandler implements ErrorHandler {
  handleError(error: any): void {
    //Sentry.captureException(error.originalError || error);
    console.log("ERROR");
  }
}
