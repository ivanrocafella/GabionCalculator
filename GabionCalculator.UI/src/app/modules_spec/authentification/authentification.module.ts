import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from 'src/app/modules_spec/user-register/user-register.component';
import { UserAuthentificationComponent } from 'src/app/modules_spec/user-authentification/user-authentification.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorHandlerService } from 'src/app/components/services/error-handler.service';
import { PrivateCabinetComponent } from 'src/app/modules_spec/private-cabinet/private-cabinet.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuard } from '../../shared/guards/auth.guard';
import { ChangePasswordComponent } from '../change-password/change-password.component';


@NgModule({
  declarations: [UserRegisterComponent, UserAuthentificationComponent, PrivateCabinetComponent, ChangePasswordComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'Login', component: UserAuthentificationComponent },
      { path: 'Register', component: UserRegisterComponent },
      { path: 'PrivateCabinet', component: PrivateCabinetComponent, canActivate: [AuthGuard] },
      { path: 'ChangePassword/:id', component: ChangePasswordComponent, canActivate: [AuthGuard] } 
    ])
  ] 
})
export class AuthentificationModule { }
