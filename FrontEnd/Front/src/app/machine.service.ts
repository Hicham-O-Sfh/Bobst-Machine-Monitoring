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
  readonly machinesUrl: string = `${environment.URL}/machines`;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getMachines(): Observable<Machine[]> {
    return this.http.get<Machine[]>(this.machinesUrl)
      .pipe(
        tap(_ => console.log("success !"))
      );
  }
}
