import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { MaterialListComponent } from './components/materials/material-list/material-list.component';
import { MaterialCreateComponent } from './components/materials/material-create/material-create.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent, MaterialListComponent, MaterialCreateComponent
  ],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule, FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
