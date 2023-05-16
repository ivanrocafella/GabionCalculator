import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRegisterComponent } from 'src/app/modules_spec/user-register/user-register.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [UserRegisterComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'Post', component: UserRegisterComponent },
    ])
  ]
})
export class AuthentificationModule { }
