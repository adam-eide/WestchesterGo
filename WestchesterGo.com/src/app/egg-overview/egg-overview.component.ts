import { Component, OnInit } from '@angular/core';
import { IEgg } from '../egg-overview/IEgg';
import { DataService } from '../shared/data.service'
import { IEvents } from '../shared/IEvents';
import { prepareEventListenerParameters } from '@angular/compiler/src/render3/view/template';

@Component({
  selector: 'app-egg-overview',
  templateUrl: './egg-overview.component.html',
  styleUrls: ['./egg-overview.component.css']
})
export class EggOverviewComponent implements OnInit {

  pageTitle: string = 'Egg Data';
  filterTypes: string[] = [ '2km', '5km', '7km', '10km' ];
  _eggFilter: string;
  _eventFilter: string;
  events: string[];
  errorMessage: string = '';
  eggs: IEgg[] = [];
  filteredEggs: IEgg[];
  eventsExpanded: boolean;
  currentEvents: IEvents;

  
  get eggFilter(): string {
    return this._eggFilter;
  }
  set eggFilter(value: string){
    this._eggFilter = value;
    switch (value){
      case "2km":
        this.eventFilter = this.currentEvents.eggs2;
        break;
      case "5km":
        this.eventFilter = this.currentEvents.eggs5;
        break;
      case "7km":
        this.eventFilter = this.currentEvents.eggs7;
        break;
      case "10km":
        this.eventFilter = this.currentEvents.eggs10;
        break;
    }
    //this.updateEggList(); this should happen when eventFilter changes
  }

  
  get eventFilter(): string {
    return this._eventFilter;
  }
  set eventFilter(value: string){
    this._eventFilter = value;
    this.eventsExpanded = false;
    this.updateEggList();
  }

  updateEggList(): void {
    switch(this._eggFilter){
      case '2km': {
        this.filteredEggs = this.eggs.filter(
          (egg: IEgg) =>
          egg.distance == 2);
        break;
      }
      case '5km': {
        this.filteredEggs = this.eggs.filter(
          (egg: IEgg) =>
          egg.distance == 5);
        break;
      }
      case '10km': {
        this.filteredEggs = this.eggs.filter(
          (egg: IEgg) =>
          egg.distance == 10);
        break;
      }
      case '7km': {
        this.filteredEggs = this.eggs.filter(
          (egg: IEgg) =>
          egg.distance == 7);
        this.filteredEggs = this.filteredEggs.filter(
          (egg: IEgg) =>
          egg.eventName.toLowerCase().indexOf(this._eventFilter?.toLowerCase()) !== -1);
        break;
      }
      
      
    }
  }

  toggleEventList(): void {
    this.eventsExpanded = !this.eventsExpanded;
  }

  getTotalEggsInPool(): number {
    let total: number = 0;
    for( let egg of this.filteredEggs){
      total += egg.hatched;
    }
    return total;
  }

  constructor(private dataService: DataService) { }

  selectEggFilter(value: string): void{
    this.eggFilter = value;
  }

  ngOnInit(): void {
    this.eventsExpanded = false;
    this._eggFilter = '7km';
    this.dataService.getEggEvents().subscribe({
      next: events => {
        this.events = events;
      }
    });
    let getIsDone = false;
    this.dataService.getCurrentEvents().subscribe({
      next: currentEvent => {
        this.currentEvents = currentEvent[0];
        this._eventFilter = this.currentEvents.eggs7;
        getIsDone ? this.updateEggList() : getIsDone = true;
      }
    });
    this.dataService.getEggs().subscribe({
      next: eggs => {
        this.eggs = eggs;
        this.filteredEggs = this.eggs;
        getIsDone ? this.updateEggList() : getIsDone = true;
      },
      error: err => this.errorMessage = err
    });
    
  }

}
