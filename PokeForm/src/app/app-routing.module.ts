import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RaidFormComponent } from './raid-form/raid-form.component';
import { EggFormComponent } from './egg-form/egg-form.component';

const routes: Routes = [
  {path: '', redirectTo: '/raids', pathMatch: 'full'},
  {path: 'raids', component: RaidFormComponent },
  {path: 'eggs', component: EggFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
