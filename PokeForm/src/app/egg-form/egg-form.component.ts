import { Component, OnInit } from '@angular/core';
import { IEvents } from '../IEvents';
import { DataService } from '../data.service';

@Component({
  selector: 'app-egg-form',
  templateUrl: './egg-form.component.html',
  styleUrls: ['./egg-form.component.css']
})
export class EggFormComponent implements OnInit {
  pageTitle: string = 'Add an Egg';
  event: string;
  distanceList: number[] = [2, 5, 7, 10];
  name: string;
  _distance: number;
  currentEvents: IEvents;
  eventLocked: boolean;
  
  constructor(private dataService: DataService) { }

  set distance(value:number){
    switch(value){
      case 2:
        this.event = this.currentEvents.eggs2;
      break;
      case 5:
        this.event = this.currentEvents.eggs5;
      break;
      case 7:
        this.event = this.currentEvents.eggs7;
      break;
      case 10:
        this.event = this.currentEvents.eggs10;
      break;
    }
    this._distance = value;
  }
  get distance(){
    return this._distance;
  }

  toggleEventLock(): void {
    this.eventLocked = !this.eventLocked;
  }

  distanceSelect(event:Event): void {
    this.distance = +(event.target as HTMLSelectElement).value;
  }

  postEgg():void {
    let egg = {
      "distance": this.distance,
      "name": this.name,
      "hatched": 0,
      "shiny": 0,
      "eventName": this.event
    }
    this.dataService.addEgg(egg).subscribe(
      result => console.log(result)
    );
  }

  ngOnInit(): void {

    this.dataService.getCurrentEvents().subscribe({
      next: currentEvent => {
        this.currentEvents = currentEvent[0];
        this.distance = 7;
      }
    });
    this.eventLocked = true;
    this.name = '';
    
  }

}
