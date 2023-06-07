import { HttpErrorResponse } from '@angular/common/http';
import { AfterContentInit, Component, OnInit } from '@angular/core';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { ApiResultResponseListGabion } from 'src/app/models/apiResultResponseListGabionModel.model';

@Component({
  selector: 'app-gabion-list',
  templateUrl: './gabion-list.component.html',
  styleUrls: ['./gabion-list.component.css']
})
export class GabionListComponent implements OnInit, AfterContentInit {

  apiResultListGabion: Partial<ApiResultResponseListGabion> = {};
  errorMessage!: string;
  showError!: boolean;

  constructor(private gabions: GabionsService) { }

  ngOnInit(): void {
    this.gabions.getAllGabions().subscribe(
      {
        next: (ApiResultListGabion) => {
          this.apiResultListGabion = ApiResultListGabion; console.log(this.apiResultListGabion);
          window.onload = () => {
            console.log('works')
            console.log(this.apiResultListGabion.result);
            var divSvg;
            this.apiResultListGabion.result?.forEach(
              function (value) {
                divSvg = document.getElementById('svg-' + value.Id + '');
                console.log(divSvg)
                divSvg!.innerHTML = value.Svg!;
              }) }
        },
        error: (err: HttpErrorResponse) => {
          console.log("error");
          this.errorMessage = err.message;
          this.showError = true;
        }
      }
    )
  };

  ngAfterContentInit(): void {
   
  };

    
}
