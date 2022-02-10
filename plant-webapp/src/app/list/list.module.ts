import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './component/list/list.component';
import { MaterialModule } from '../shared';
import { NotificationService } from './services/notifier.service';


@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
	providers: [
		NotificationService
	]
})
export class ListModule { }
