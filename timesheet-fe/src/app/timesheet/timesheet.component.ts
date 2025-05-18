import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,  ReactiveFormsModule } from '@angular/forms';
import { TimesheetService } from '../timesheet.service';
import { TimesheetEntry } from './timesheet-entry.model';
import { RouterOutlet } from '@angular/router';
import { CommonModule, NgFor } from '@angular/common';


@Component({
  selector: 'app-root',
  templateUrl: './timesheet.component.html',
    standalone: true,
    imports: [RouterOutlet,ReactiveFormsModule,NgFor,CommonModule],
})
export class TimesheetComponent implements OnInit {
  timesheetForm: FormGroup;
  entries: TimesheetEntry[] = [];

  constructor(private fb: FormBuilder, private service: TimesheetService) {
    this.timesheetForm = this.fb.group({
      date: [''],
      startTime: [''],
      endTime: [''],
      projectName: [''],
      task: [''],
      remarks: ['']
    });
  }

  ngOnInit(): void {
    this.loadEntries();
  }

  loadEntries(): void {
    this.service.getAll().subscribe(data => this.entries = data);
  }

  submit(): void {
    const newEntry: TimesheetEntry = this.timesheetForm.value;
    this.service.create(newEntry).subscribe(() => {
      this.timesheetForm.reset();
      this.loadEntries();
    });
  }

  deleteEntry(id: number): void {
    this.service.delete(id).subscribe(() => this.loadEntries());
  }
}
