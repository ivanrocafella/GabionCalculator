import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { MaterialListComponent } from './components/materials/material-list/material-list.component';
import { MaterialCreateComponent } from './components/materials/material-create/material-create.component';
import { FormsModule } from '@angular/forms';
import { GabionCreateComponent } from './components/gabions/gabion-create/gabion-create.component';
import { RouterModule } from '@angular/router';
import { ErrorHandlerService } from 'src/app/components/services/error-handler.service';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from './shared/guards/auth.guard';
import { PrivacyComponent } from './components/materials/privacy/privacy.component';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialEditComponent } from './components/materials/material-edit/material-edit.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { GabionListComponent } from './components/gabions/gabion-list/gabion-list.component';
import { GabionDetailsComponent } from './components/gabions/gabion-details/gabion-details.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';


export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent, MaterialListComponent, MaterialCreateComponent, GabionCreateComponent, PrivacyComponent, ForbiddenComponent, MaterialEditComponent, GabionListComponent, GabionDetailsComponent
  ],
  imports: [
    BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, MatSnackBarModule,
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }),
    RouterModule.forRoot([{ path: 'User', loadChildren: () => import('src/app/modules_spec/authentification/authentification.module').then(m => m.AuthentificationModule) }      
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7083"],
        disallowedRoutes: []
      }
    }),
    BrowserAnimationsModule, MatPaginatorModule, MatTableModule
  ],
  providers: [AuthGuard,
    {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorHandlerService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
