import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TimesheetEntry } from './timesheet/timesheet-entry.model';


@Injectable({ providedIn: 'root' })
export class TimesheetService {
  private apiUrl = 'https://localhost:32770/api/timesheet';

  constructor(private http: HttpClient) {}

  getAll(): Observable<TimesheetEntry[]> {
    return this.http.get<TimesheetEntry[]>(this.apiUrl);
  }

  create(entry: TimesheetEntry): Observable<TimesheetEntry> {
    return this.http.post<TimesheetEntry>(this.apiUrl, entry);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
