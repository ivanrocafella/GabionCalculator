import { Component } from '@angular/core';
import { UsersService } from 'src/app/components/services/users.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiResultResponseListUser } from 'src/app/models/apiResultResponseListUserModel.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-private-cabinet',
  templateUrl: './private-cabinet.component.html',
  styleUrls: ['./private-cabinet.component.css']
})
export class PrivateCabinetComponent {
  public isAdmin!: boolean;
  apiResult: Partial<ApiResultResponseListUser> = {};

  constructor(private usersService: UsersService, private router: Router) {
    this.usersService.isAdminChanged
      .subscribe(res => {
        this.isAdmin = res;
      })
  }

  ngOnInit(): void {
    if (this.usersService.isUserAdmin())
      this.usersService.sendIsAdminStateChangeNotification(true);

    this.usersService.getAllUsers().subscribe
    ({
      next: (response: ApiResultResponseListUser) => {
        this.apiResult = response; console.log(response);
      },
      error: (response) => { console.log(response); console.log("Error!"); }
    })
  }
}
