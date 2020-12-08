import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reports-production',
  templateUrl: './reports-production.component.html',
  styleUrls: ['./reports-production.component.scss']
})
export class ReportsProductionComponent implements OnInit {
  checked = false;
  indeterminate = false;
  constructor() { }

  ngOnInit(): void {
  }

}
