import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TempServiceService {
  data: {
    name: string;
    designation: string;
    department: string;
  }[] = [
    {
      name: 'John Doe',
      designation: 'Software Engineer',
      department: 'Engineering',
    },
    {
      name: 'Jane Smith',
      designation: 'HR Manager',
      department: 'Human Resources',
    },
    {
      name: 'Alice Johnson',
      designation: 'Marketing Specialist',
      department: 'Marketing',
    },
    // Add more objects as needed
  ];

  constructor() {}
}
