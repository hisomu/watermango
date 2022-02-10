import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { PlantService } from './plant.service';

@Injectable()
export class NotificationService {
	private _hubConnection: HubConnection | undefined;
	private readonly SignalrHubUrl: string;

	constructor(
    private readonly sb: MatSnackBar,
    private readonly plantSvc: PlantService
    ) {
		this.SignalrHubUrl = this.getBaseUri();
		this.init();
	}

	stop(): void {
		this._hubConnection?.stop();
	}

	private readonly getBaseUri = () => {
		return `http://localhost:5000`;
	};

	private init() {
		this.register();
		this.stablishConnection();
		this.registerHandlers();
	}

	private register() {
		this._hubConnection = new HubConnectionBuilder()
			.withUrl(`${this.SignalrHubUrl}/hub/plant`)
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build();
	}

	private stablishConnection() {
		this._hubConnection?.start()
			.then(() => {
				// eslint-disable-next-line no-console
				console.log('Hub connection started');
			})
			.catch(() => {
				// eslint-disable-next-line no-console
				console.log('Error while establishing connection');
			});
	}

	private registerHandlers() {
		this._hubConnection?.on('UpdatedPlant', (msg: any) => {
			this.plantSvc.plantEdited$.next(msg);
		});
	}
}
