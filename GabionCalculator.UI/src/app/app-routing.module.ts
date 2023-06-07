import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaterialListComponent } from './components/materials/material-list/material-list.component';
import { MaterialCreateComponent } from './components/materials/material-create/material-create.component';
import { MaterialEditComponent } from './components/materials/material-edit/material-edit.component';
import { PrivacyComponent } from './components/materials/privacy/privacy.component';
import { GabionCreateComponent } from './components/gabions/gabion-create/gabion-create.component';
import { GabionListComponent } from './components/gabions/gabion-list/gabion-list.component';
import { ForbiddenComponent } from 'src/app/components/forbidden/forbidden.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { AdminGuard } from './shared/guards/admin.guard';

const routes: Routes = [
  { path: 'Material/Materials', component: MaterialListComponent, canActivate: [AuthGuard] },
  { path: 'Gabion/Gabions', component: GabionListComponent, canActivate: [AuthGuard] },
  { path: 'Material/Privacy', component: PrivacyComponent, canActivate: [AuthGuard, AdminGuard] },
  { path: 'Forbidden', component: ForbiddenComponent },
  { path: 'Material/Post', component: MaterialCreateComponent },
  { path: 'Material/Update/:id', component: MaterialEditComponent },
  { path: '', component: GabionCreateComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
