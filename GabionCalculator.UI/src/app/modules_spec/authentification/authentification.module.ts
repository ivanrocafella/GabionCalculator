import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from 'src/app/modules_spec/user-register/user-register.component';
import { UserAuthentificationComponent } from 'src/app/modules_spec/user-authentification/user-authentification.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorHandlerService } from 'src/app/components/services/error-handler.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';


@NgModule({
  declarations: [UserRegisterComponent, UserAuthentificationComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'Login', component: UserAuthentificationComponent },
      { path: 'Register', component: UserRegisterComponent }      
    ])
  ] 
})
export class AuthentificationModule { }
