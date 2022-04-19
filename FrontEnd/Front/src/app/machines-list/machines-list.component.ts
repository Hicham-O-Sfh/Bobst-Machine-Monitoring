import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MachineService } from '../machine.service';
import { Machine } from '../Models/Machine';

@Component({
  selector: 'app-machines-list',
  templateUrl: './machines-list.component.html',
  styleUrls: ['./machines-list.component.css']
})
export class MachinesListComponent implements OnInit {
  machines: Machine[] = [];

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private machineService: MachineService, private router: Router) { }

  ngOnInit() {
    this.getMachines();
  }

  getMachines(): void {
    // calling the service's method to retrieve all machines
    // and iterrate over them later on the HTML part of the component
    this.machineService.getMachines().subscribe(machines => this.machines = machines);
  }

  deleteMachine(event: MouseEvent, id: number): void {
    // stoping propagating so the "click" event of the button
    // does not affect the "click" event of his parent : the table row (<tr>) 
    event.stopPropagation();
    // calling the service's method for deleting the machine
    this.machineService.deleteMachine(id).subscribe(isDeleted => {
      // deleting the specific machine from the current array
      // which is based on by the table in the curretn component
      if (isDeleted)
        this.machines = this.machines.filter(m => m.machineId !== id);
    },
      (error) => console.log(error)
    );
  }

  goToDashboard(id: number): void {
    // navigating "manually" to the attemted component after having clicked the table row
    this.router.navigate(['Dashboard/', id]);
  }
}
