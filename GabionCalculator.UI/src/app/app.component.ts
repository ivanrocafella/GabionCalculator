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
  public isUserAdmin!: boolean;
  constructor(private usersService: UsersService, private router: Router) {
    this.usersService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
    this.usersService.isAdminChanged
      .subscribe(res => {
        this.isUserAdmin = res;
      })
  }

  ngOnInit(): void {

    if (this.usersService.isUserAuthenticated()) {
      this.usersService.sendAuthStateChangeNotification(true);
      console.log(this.isUserAuthenticated)
    }
    if (this.usersService.isUserAdmin()) {
      this.usersService.sendIsAdminStateChangeNotification(true);
      console.log(this.isUserAdmin)
    }    
  }

  public logout = () => {
    this.usersService.logout();
    this.router.navigate(["/"]);
  }
}
