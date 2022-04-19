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

  // configuring httpOptions (for safe purpose)
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private machineService: MachineService,
    private actRoute: ActivatedRoute
  ) {
    // retrieving the "id" parameter from the url
    // so we can work with it and search using it
    this.machineId = this.actRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.getAllData();
    this.updateProductionValue(this.machineId);
  }

  getAllData(): void {
    forkJoin(
      // calling the service method to find the desired Machine by the given id  
      this.machineService.findMachine(this.machineId),
      // and getting its total productions
      this.getTotalProduction(this.machineId)
    ).subscribe(
      (results) => {
        // affecting results of each call to its specific field respectivly ..
        this.machine = results[0];
        this.machine.production = results[1].totalproduction;
      },
      // logging errors on Console, if any ..
      (error) => console.log(error)
    );
  }

  updateProductionValue(machineId: number): void {
    // refreshing the value each 5 secondes
    this.subscription = interval(this.interval)
      .pipe(mergeMap(() => this.getTotalProduction(machineId)))
      .subscribe(data => {
        if (this.machine)
          this.machine.production = data.totalproduction
      });
  }

  getTotalProduction(machineId: number) {
    // calling the service method to retrieve value of total productions 
    // of the current machine
    return this.machineService.getTotalProduction(machineId);
  }

  ngOnDestroy(): void {
    // unsubscribing on component destroy so
    // component's HTTP calls and behaviors do not continue after being unused  
    this.subscription?.unsubscribe();
  }
}
