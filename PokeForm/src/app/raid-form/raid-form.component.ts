import { Component, OnInit } from '@angular/core';
import { DataService } from '../data.service'; 
@Component({
  selector: 'app-raid-form',
  templateUrl: './raid-form.component.html',
  styleUrls: ['./raid-form.component.css']
})
export class RaidFormComponent implements OnInit {

  pageTitle: string = 'Add a Raid'
  event: string;
  starList : number[] = [1, 2, 3, 4, 5];
  name: string;
  canBeShiny: boolean;
  _stars: number;
  eventLocked: boolean;
  eventList: string[];

  constructor(private dataService: DataService) { }

  set stars(value:number){
    this._stars = value;
  }
  get stars(): number{
    return this._stars;
  }
  toggleEventLock(): void {
    this.eventLocked = !this.eventLocked;
  }
  starSelect(event:Event): void {
    this.stars = +(event.target as HTMLSelectElement).value;
    console.log("stars: " + this.stars);
  }
  shinySelect(event:Event): void {
    this.canBeShiny = (event.target as HTMLSelectElement).value === 'Yes';
    console.log("shiny: " + this.canBeShiny);
  }

  postRaid():void {
    let raid = {
      "stars": this.stars,
      "pokemon": this.name,
      "total": 0,
      "caught": 0,
      "shiny": this.canBeShiny?0:1,
      "eventName": this.event
    }
    console.log(raid);
    this.dataService.addRaid(raid).subscribe(
      result => console.log(result)
    );
  }
  ngOnInit(): void {

    this.dataService.getCurrentEvents().subscribe({
      next: currentEvent => {
        this.event = currentEvent[0].raids;
        console.log("Event " + this.event);
      }
    });
    this.dataService.getRaidEvents().subscribe({
      next: eventList =>{
        this.eventList = eventList.reverse();
      }
    });
    this.eventLocked = true;
    this.name = '';
    this.canBeShiny = true;
    this.stars = 1;
  }

}
