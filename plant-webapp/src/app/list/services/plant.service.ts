import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, shareReplay, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlantService {
  private readonly baseUri = 'http://localhost:5000/api/plant';

  // list of plants
  plants$ = this.httpClient
  .get<Array<any>>(this.baseUri)
		.pipe(
			shareReplay({ bufferSize: 1, refCount: true })
		);
  // subject of every time a plant is edited
  readonly plantEdited$ = new BehaviorSubject<any>(undefined);

  constructor(
    protected httpClient: HttpClient
  ) {}

  startWatering(id: string) {
    return this.httpClient.post(`${this.baseUri}/${id}/start`, undefined);
  }

  stopWatering(id: string) {
    return this.httpClient.post(`${this.baseUri}/${id}/stop`, undefined);
  }
}
