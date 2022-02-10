import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root'
})

/** A service which contains a number of trackBy functions to be used with ngFors throughout the program */
/** EVERY instance of ngFor should be accompanied by a trackBy function of some sort */
export class TrackByService {

	/** Should be used to track basic datatypes (ex. string) */
	trackByIndex(index: number, item: any): any {
		return item ? index : undefined;
	}
}
