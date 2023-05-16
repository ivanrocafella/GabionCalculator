import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { MaterialListComponent } from './components/materials/material-list/material-list.component';
import { MaterialCreateComponent } from './components/materials/material-create/material-create.component';
import { FormsModule } from '@angular/forms';
import { GabionCreateComponent } from './components/gabions/gabion-create/gabion-create.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent, MaterialListComponent, MaterialCreateComponent, GabionCreateComponent
  ],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, RouterModule.forRoot([
      { path: 'User', loadChildren: () => import('src/app/modules_spec/authentification/authentification.module').then(m => m.AuthentificationModule) },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
