import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/components/services/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'GabionCalculator.UI';
  public isUserAuthenticated!: boolean;
  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
    this.usersService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  }

  public logout = () => {
    this.usersService.logout();
    this.router.navigate(["/"]);
  }
}
