import { Component, HostListener } from '@angular/core';
import { UsersService } from 'src/app/components/services/users.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiResultResponseListUser } from 'src/app/models/apiResultResponseListUserModel.model';
import { HttpErrorResponse } from '@angular/common/http';
import { ApiResultResponseUserModel } from '../../models/apiResultResponseUserModel.model';
import encodeUtf8 from 'encode-utf8';
import { ResponseUserModel } from '../../models/responseUserModel.model';

@Component({
  selector: 'app-private-cabinet',
  templateUrl: './private-cabinet.component.html',
  styleUrls: ['./private-cabinet.component.css']
})
export class PrivateCabinetComponent {
  public isAdmin!: boolean;
  apiResult: Partial<ApiResultResponseListUser> = {};
  responseUser: Partial<ApiResultResponseUserModel> = {};
  currrentUser: Partial<ResponseUserModel> = {};

  constructor(private usersService: UsersService, private router: Router) {
    this.usersService.isAdminChanged
      .subscribe(res => {
        this.isAdmin = res;
      })
  }

  ngOnInit(): void {
    this.usersService.getUser().subscribe({
      next: ApiResultResponseUserModel => {
        this.currrentUser = ApiResultResponseUserModel.result
      },
      error: (err: HttpErrorResponse) => {
        console.log(err.message);
      }
    })

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

  setIdForButton(Id: string) {
    console.log(Id);
    var userLine = document.getElementById('user-' + Id + '')
    console.log(userLine);
    var userLogin = userLine!.getElementsByTagName('td')[1].textContent;
    var btnDelCard = document.getElementsByClassName("btnDelCard")[0];
    var modal_body = document.getElementById("modal_body");
    modal_body!.innerHTML = 'Вы действительно хотите удалить пользователя ' + userLogin + '?';
    btnDelCard.setAttribute('id', Id);
  }

  delUser(event: Event) {
    event.preventDefault();
    var target = event.target || event.currentTarget;
    if (target instanceof Element) {
      var id = target.getAttribute("id")
      var userLine = document.getElementById('user-' + id + '')
      userLine!.remove();
      console.log(id)
      this.usersService.deleteUser(id!).subscribe
      ({
        next: (response: ApiResultResponseUserModel) => {
          this.responseUser = response; console.log(response);
        },
        error: (response) => { console.log(response) }
      })
    }
  }
}
