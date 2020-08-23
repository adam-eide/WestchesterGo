import { Component, OnInit } from '@angular/core';
import { IRaid } from './raid';
import { DataService } from '../shared/data.service'

@Component({
  selector: 'app-raid-overview',
  templateUrl: './raid-overview.component.html',
  styleUrls: ['./raid-overview.component.css']
})
export class RaidOverviewComponent implements OnInit {
  pageTitle: string = 'Raid Data';
  filterTypes: string[] = [ 'All Time', 'By Event', 'By Pokemon'];
  _raidFilter: string;
  _eventFilter: string;
  _pokemonFilter: string;
  events: string[];
  errorMessage: string = '';
  raids: IRaid[] = [];
  filteredRaids: IRaid[];
  raidTotals: IRaid[];
  eventsExpanded: boolean;

  get raidFilter(): string {
    return this._raidFilter;
  }
  set raidFilter(value: string){
    this._raidFilter = value;
    this.updateRaidList();
  }

  get pokemonFilter(): string {
    return this._pokemonFilter;
  }
  set pokemonFilter(value: string){
    this._pokemonFilter = value;
    this.updateRaidList();
  }

  get eventFilter(): string {
    return this._eventFilter;
  }
  set eventFilter(value: string){
    this._eventFilter = value;
    this.eventsExpanded = false;
    this.updateRaidList();
  }

  updateRaidList(): void {
    switch(this._raidFilter){
      case 'All Time': {
        this.filteredRaids = this.raids;
        break;
      }
      case 'By Event': {
        this.filteredRaids = this.raids.filter(
          (raid: IRaid) =>
          raid.eventName.toLowerCase().indexOf(this._eventFilter.toLowerCase()) !== -1);
        break;
      }
      case 'By Pokemon': {
        this.filteredRaids = this.raids.filter(
          (raid: IRaid) =>
          raid.pokemon.toLowerCase().indexOf(this._pokemonFilter) !== -1);
        break;
      }
    }
  }

  toggleEventList(): void {
    this.eventsExpanded = !this.eventsExpanded;
  }

  

  constructor(private dataService: DataService){

  }

  selectRaidFilter(value: string): void{
    this.raidFilter = value;
  }

  countRaidTotals():void{
    for( let r of this.raids){
      this.raidTotals[r.stars-1].caught += r.caught;
      this.raidTotals[r.stars-1].shiny += r.shiny;
      this.raidTotals[r.stars-1].total += r.total;
    }
  }
  ngOnInit(): void {
    this.raidTotals = [
      {
        "raidID": 0,
        "stars": 1,
        "pokemon": "0",
        "total": 0,
        "caught": 0,
        "shiny": 0,
        "eventName": "0"
      },
      {
        "raidID": 0,
        "stars": 2,
        "pokemon": "0",
        "total": 0,
        "caught": 0,
        "shiny": 0,
        "eventName": "0"
      },
      {
        "raidID": 0,
        "stars": 3,
        "pokemon": "0",
        "total": 0,
        "caught": 0,
        "shiny": 0,
        "eventName": "0"
      },
      {
        "raidID": 0,
        "stars": 4,
        "pokemon": "0",
        "total": 0,
        "caught": 0,
        "shiny": 0,
        "eventName": "0"
      },
      {
        "raidID": 0,
        "stars": 5,
        "pokemon": "0",
        "total": 0,
        "caught": 0,
        "shiny": 0,
        "eventName": "0"
      }
    ];
    console.log(this.raidTotals);
    this.eventsExpanded = false;
    this._raidFilter = 'All Time';
    this._pokemonFilter = '';
    this.dataService.getRaids().subscribe({
      next: raids => {
        this.raids = raids;
        this.filteredRaids = this.raids;
        this.countRaidTotals();
      },
      error: err => this.errorMessage = err
    });
    this.dataService.getRaidEvents().subscribe({
      next: events => {
        this.events = events;
      }
    });
    this.dataService.getCurrentEvents().subscribe({
      next: currentEvent => {
        this._eventFilter = currentEvent[0].raids;
      }
    });
    
  }

}
