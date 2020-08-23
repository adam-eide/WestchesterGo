import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RaidOverviewComponent } from './raid-overview/raid-overview.component';
import { EggOverviewComponent } from './egg-overview/egg-overview.component';
import { AboutComponent } from './about/about.component';

const routes: Routes = [
  {path: '', redirectTo: '/raids', pathMatch: 'full'},
  {path: 'raids', component: RaidOverviewComponent },
  {path: 'eggs', component: EggOverviewComponent },
  {path: 'about', component: AboutComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
