import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Machine } from './Models/Machine';

@Injectable({
  providedIn: 'root'
})
export class MachineService {

  readonly getAllMachinesUrl: string = `${environment.URL}/machines`;
  readonly findMachineUrl: string = `${environment.URL}/machine/`;
  readonly getProductionOfMachine: string = `${environment.URL}/machine/totalproduction`;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getMachines(): Observable<Machine[]> {
    return this.http.get<Machine[]>(this.getAllMachinesUrl);
  }

  findMachine(id: number): Observable<Machine> {
    return this.http.get<Machine>(this.findMachineUrl + id);
  }

  getTotalProduction(machineId: number): Observable<any> {
    return this.http.get<number>(this.getProductionOfMachine, {
      params: {
        id: machineId
      },
    });
  }
}
