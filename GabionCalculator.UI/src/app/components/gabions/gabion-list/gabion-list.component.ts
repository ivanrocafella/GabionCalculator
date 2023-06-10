import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { ApiResultResponseListGabion } from 'src/app/models/apiResultResponseListGabionModel.model';
import { ResponseGabionModel } from 'src/app/models/responseGabionModel.model';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

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

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  pageEvent?: PageEvent;
  dataSource = MatTableDataSource<ResponseGabionModel>
  // Переменная для хранения текущей страницы
  currentPage = 0;
  // Переменная для хранения количества элементов на странице
  itemsPerPage = 5;
  // Переменная для хранения общего количества элементов
  totalItems = 0;
  // Пример массива данных
  listGabions: ResponseGabionModel[] = [];

  constructor(private gabions: GabionsService, private sanitizer: DomSanitizer) { 
  }


  ngOnInit(): void {
    this.getSrverData(this.itemsPerPage, this.currentPage);
  }

  public getSrverData(itemsPerPage: number, currentPage: number) {
    this.gabions.getGabions(itemsPerPage, currentPage).subscribe(
      {
        next: (ApiResultListGabion) => {
          this.apiResultListGabion = ApiResultListGabion; console.log(this.apiResultListGabion);

          this.listGabions = ApiResultListGabion.result!;
          this.totalItems = ApiResultListGabion.additNum;

          this.paginator.pageSize = this.itemsPerPage;
          this.paginator.pageIndex = this.currentPage;

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

  onPageChange(event: PageEvent) {
    this.currentPage = event.pageIndex;
    this.itemsPerPage = event.pageSize;
    this.getSrverData(this.itemsPerPage, this.currentPage);
  } 
}

