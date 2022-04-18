import { HttpHeaders } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { forkJoin, interval, Subscription } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { MachineService } from '../machine.service';
import { Machine } from '../Models/Machine';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {
  readonly machineId: number;
  machine?: Machine;
  interval: number = 5000;
  subscription?: Subscription;

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
    this.getAllData();
    this.updateProductionValue(this.machineId);
  }

  getAllData(): void {
    forkJoin(
      this.machineService.findMachine(this.machineId),
      this.getTotalProduction(this.machineId)
    ).subscribe(
      (results) => {
        this.machine = results[0];
        this.machine.production = results[1].totalproduction;
      },
      (error) => console.log(error)
    );
  }

  updateProductionValue(machineId: number): void {
    this.subscription = interval(this.interval)
      .pipe(mergeMap(() => this.getTotalProduction(machineId)))
      .subscribe(data => {
        if (this.machine)
          this.machine.production = data.totalproduction
      });
  }

  getTotalProduction(machineId: number) {
    return this.machineService.getTotalProduction(machineId);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
