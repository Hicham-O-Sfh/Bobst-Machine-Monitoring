import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Machine } from './Models/Machine';

@Injectable({
  providedIn: 'root'
})
export class MachineService {

  // my APIs urls
  readonly getAllMachines_Url: string = `${environment.URL}/machines`;
  readonly findMachine_Url: string = `${environment.URL}/machine/`;
  readonly deleteMachine_URL: string = `${environment.URL}/machine/`;
  readonly getProductionOfMachine_URL: string = `${environment.URL}/machine/totalproduction`;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  // Get : all machines
  getMachines(): Observable<Machine[]> {
    return this.http.get<Machine[]>(this.getAllMachines_Url);
  }

  // Get : find specific machine by id
  findMachine(id: number): Observable<Machine> {
    return this.http.get<Machine>(this.findMachine_Url + id);
  }

  // Get : total productions of specific machine
  getTotalProduction(machineId: number): Observable<any> {
    return this.http.get<number>(this.getProductionOfMachine_URL, {
      params: {
        id: machineId
      },
    });
  }

  // Delete : delete specific machine and all it's details 
  deleteMachine(machineId: number): Observable<any> {
    return this.http.delete(this.deleteMachine_URL + machineId);
  };
}
