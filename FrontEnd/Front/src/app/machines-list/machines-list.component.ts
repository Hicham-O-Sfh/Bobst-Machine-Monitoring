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
    this.machineService.getMachines().subscribe(machines => this.machines = machines);
  }

  deleteMachine(event: MouseEvent, id: number): void {
    event.stopPropagation();
    this.machineService.deleteMachine(id).subscribe(isDeleted => {
      if (isDeleted)
        this.machines = this.machines.filter(m => m.machineId !== id);
    },
      (error) => console.log(error)
    );
  }

  goToDashboard(id: number): void {
    this.router.navigate(['Dashboard/', id]);
  }
}
