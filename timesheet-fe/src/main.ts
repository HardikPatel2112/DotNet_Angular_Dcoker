import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { TimesheetComponent } from './app/timesheet/timesheet.component';

bootstrapApplication(TimesheetComponent, {
  providers: [provideHttpClient()] // 👈 This is essential for HttpClient to work
})
  .catch((err) => console.error(err));
