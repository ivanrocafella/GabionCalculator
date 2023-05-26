import { Component } from '@angular/core';
import { UsersService } from 'src/app/components/services/users.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-private-cabinet',
  templateUrl: './private-cabinet.component.html',
  styleUrls: ['./private-cabinet.component.css']
})
export class PrivateCabinetComponent {
  public isUserAdmin!: boolean;
  constructor(private usersService: UsersService, private router: Router) {
    this.isUserAdmin = this.usersService.isUserAdmin();
  }
}
