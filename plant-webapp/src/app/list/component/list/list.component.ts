import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { catchError, combineLatest, map, Observable, startWith, switchMap, take, throwError } from 'rxjs';
import { untilComponentDestroyed } from 'src/app/shared/component-destroyed';
import { TrackByService } from 'src/app/shared/services';
import { NotificationService } from '../../services/notifier.service';
import { PlantService } from '../../services/plant.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  plants$: Observable<any> | undefined;

  constructor(
		private readonly cdRef: ChangeDetectorRef,
		private readonly plantSvc: PlantService,
    private readonly notificationSvc: NotificationService,
		public trackBySvc: TrackByService
	) { }

  ngOnInit(): void {
    this.plants$ = combineLatest([
      this.plantSvc.plants$,
      this.plantSvc.plantEdited$
    ])
    .pipe(
      map(([plants, updated]) => {
        if (updated)
        {
          const plantIndex = plants.findIndex(p => p.id == updated.id);
          plants[plantIndex] = updated;
        }

        return plants;
      })
    );
  }

	ngOnDestroy(): void {
		// required to properly unsubscribe observables
	}

  start(id: string): void {
    this.plantSvc.startWatering(id).pipe(take(1)).subscribe();
  }

  stop(id: string): void {
    this.plantSvc.stopWatering(id).pipe(take(1)).subscribe();
  }
}
