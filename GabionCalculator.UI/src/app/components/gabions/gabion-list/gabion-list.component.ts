import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { ApiResultResponseListGabion } from 'src/app/models/apiResultResponseListGabionModel.model';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-gabion-list',
  templateUrl: './gabion-list.component.html',
  styleUrls: ['./gabion-list.component.css']
})
export class GabionListComponent implements OnInit {

  apiResultListGabion: Partial<ApiResultResponseListGabion> = {};
  errorMessage!: string;
  showError!: boolean;
  svgSafeHtml: SafeHtml[] = [];

  @ViewChild(MatPaginator)
  paginator?: MatPaginator;
  pageEvent?: PageEvent;
  datasource?: null;
  pageIndex?: number;
  pageSize?: number;
  length?: number;


  constructor(private gabions: GabionsService, private sanitizer: DomSanitizer) { }


  ngOnInit(): void {
    this.getSrverData();
  }

  public getSrverData() {
    this.gabions.getAllGabions().subscribe(
      {
        next: (ApiResultListGabion) => {
          this.apiResultListGabion = ApiResultListGabion; console.log(this.apiResultListGabion);
          var parser = new DOMParser();
          var svgElem;
          var svgStr;
          this.svgSafeHtml = [];
          var sanitizedHtml;
          this.apiResultListGabion.result?.forEach(
            (value) => {
              svgElem = parser.parseFromString(value.Svg!, "image/svg+xml").documentElement;
              svgElem.setAttribute('style', 'height: auto; width: 100%;');
              svgStr = new XMLSerializer().serializeToString(svgElem);
              sanitizedHtml = this.sanitizer.bypassSecurityTrustHtml(svgStr);
              this.svgSafeHtml.push(sanitizedHtml);
            })
        },
        error: (err: HttpErrorResponse) => {
          console.log("error");
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }

}

