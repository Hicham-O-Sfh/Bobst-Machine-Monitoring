import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { interval } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { MachineService } from '../machine.service';
import { Machine } from '../Models/Machine';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  readonly machineId: number;
  machine?: Machine;
  interval: number = 0;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private machineService: MachineService,
    private actRoute: ActivatedRoute
  ) {
    this.machineId = this.actRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.machineService.findMachine(this.machineId).subscribe(
      machine => this.machine = machine,
      err => console.log(err),
    );
    this.updateProductionValue(this.machineId);
    this.interval = 5000; // 5000 millisecondes
  }

  updateProductionValue(machineId: number): void {
    interval(this.interval)
      .pipe(mergeMap(() => this.getTotalProduction(machineId)))
      .subscribe(data => {
        if (this.machine)
          this.machine.production = data.totalproduction
      })
  }

  getTotalProduction(machineId: number) {
    return this.machineService.getTotalProduction(machineId);
  }
}
