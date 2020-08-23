import {Injectable} from '@angular/core';

import { IRaid } from '../raid-overview/raid';
import { IEgg } from '../egg-overview/IEgg';
import { IEvents } from './IEvents';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class DataService {
    //private dataUrl = 'assets/raids.json';

    constructor(private http: HttpClient ) {}

    getRaids(): Observable<IRaid[]> {
        return this.http.get<IRaid[]>('http://westchestergo.com/api/raids').pipe(
            tap(data => console.log('All: ' + JSON.stringify(data))),
            catchError(this.handleError)
        ); }
    
    getCurrentEvents(): Observable<IEvents[]>{
        return this.http.get<IEvents[]>('http://westchestergo.com/api/events').pipe(
            tap(data => console.log('All: ' + JSON.stringify(data))),
            catchError(this.handleError)
        ); 
    }

    getRaidEvents(): Observable<string[]>{
        return this.http.get<string[]>('http://westchestergo.com/api/raidevents').pipe(
            tap(data => console.log('All: ' + JSON.stringify(data))),
            catchError(this.handleError)
        ); 
    }

    getEggEvents(): Observable<string[]>{
        return this.http.get<string[]>('http://westchestergo.com/api/eggevents').pipe(
            tap(data => console.log('All: ' + JSON.stringify(data))),
            catchError(this.handleError)
        ); 
    }

    getEggs(): Observable<IEgg[]> {
        return this.http.get<IEgg[]>('http://westchestergo.com/api/eggs').pipe(
            tap(data => console.log('All: ' + JSON.stringify(data))),
            catchError(this.handleError)
        ); 
    }

    
    private handleError(err: HttpErrorResponse) {

        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        let errorMessage = '';
        if (err.error instanceof ErrorEvent) {
        // A client-side or network error occurred. Handle it accordingly.
        errorMessage = `An error occurred: ${err.error.message}`;
        } else {
        // The backend returned an unsuccessful response code.
        // The response body may contain clues as to what went wrong,
        errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.error(errorMessage);
        return throwError(errorMessage);
        }
}