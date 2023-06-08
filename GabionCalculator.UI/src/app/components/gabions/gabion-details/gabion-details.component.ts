import { Component, OnInit } from '@angular/core';
import { ApiResultResponseGabionModel } from 'src/app/models/apiResultResponseGabionModel.model';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-gabion-details',
  templateUrl: './gabion-details.component.html',
  styleUrls: ['./gabion-details.component.css']
})
export class GabionDetailsComponent implements OnInit {
  apiResultResponseGabion: Partial<ApiResultResponseGabionModel> = {};
  id!: number;
  svgSafeHtml?: SafeHtml;
  

  constructor(private gabionService: GabionsService, private route: ActivatedRoute, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.gabionService.getGabionResponseModel(this.id).subscribe(
      {
        next: (ApiResultResponseGabion) => {
          this.apiResultResponseGabion = ApiResultResponseGabion;
          console.log(this.apiResultResponseGabion.result);
          var parser = new DOMParser();
          var svgElem;
          var svgStr;
          svgElem = parser.parseFromString(ApiResultResponseGabion.result.Svg!, "image/svg+xml").documentElement;
          svgElem.setAttribute('style', 'height: auto; width: 100%;');
          svgStr = new XMLSerializer().serializeToString(svgElem);
          this.svgSafeHtml = this.sanitizer.bypassSecurityTrustHtml(svgStr);
        },
        error: (err: HttpErrorResponse) => {
          console.log(err.error);
        }
      })
    }
}
