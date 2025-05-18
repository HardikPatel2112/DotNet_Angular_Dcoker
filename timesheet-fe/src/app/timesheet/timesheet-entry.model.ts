export interface TimesheetEntry {
  id?: number;
  date: string; // ISO date string
  startTime: string; // HH:mm:ss
  endTime: string;
  projectName: string;
  task: string;
  remarks: string;
}
