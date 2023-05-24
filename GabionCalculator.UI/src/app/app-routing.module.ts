import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaterialListComponent } from './components/materials/material-list/material-list.component';
import { MaterialCreateComponent } from './components/materials/material-create/material-create.component';
import { GabionCreateComponent } from './components/gabions/gabion-create/gabion-create.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  { path: 'Material/Materials', component: MaterialListComponent, canActivate: [AuthGuard] },
  { path: 'Material/Post', component: MaterialCreateComponent },
  { path: '', component: GabionCreateComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
