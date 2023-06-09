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
  filtSvgSafeHtml: SafeHtml[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  pageEvent?: PageEvent;
  dataSource = MatTableDataSource<ResponseGabionModel>
  // Переменная для хранения текущей страницы
  currentPage = 0;
  // Переменная для хранения количества элементов на странице
  itemsPerPage = 1;
  // Переменная для хранения общего количества элементов
  totalItems = 0;
  // Пример массива данных
  listGabions: ResponseGabionModel[] = [];
  filteredGabions: ResponseGabionModel[] = []; // Отфильтрованный массив данных на текущей странице


  constructor(private gabions: GabionsService, private sanitizer: DomSanitizer) { 
  }


  ngOnInit(): void {
    this.getSrverData();
  }

  public getSrverData() {
    this.gabions.getAllGabions().subscribe(
      {
        next: (ApiResultListGabion) => {
          this.apiResultListGabion = ApiResultListGabion; console.log(this.apiResultListGabion);

          this.listGabions = ApiResultListGabion.result!;
          console.log(this.listGabions)

          if (this.currentPage == 0) {
            this.filteredGabions = this.listGabions.slice(0, this.itemsPerPage);
          } // added filteredGabions during execution of first page 

          this.totalItems = this.listGabions.length;
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
          if (this.currentPage == 0) {
            this.filtSvgSafeHtml = this.svgSafeHtml.slice(0, this.itemsPerPage);
          } // added filtSvgSafeHtml during execution of first page 
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
    console.log(event.pageIndex)
    // Действия, выполняемые при изменении страницы
    const startIndex = event.pageIndex * event.pageSize;
    const endIndex = startIndex + event.pageSize;
    this.filteredGabions = this.listGabions.slice(startIndex, endIndex);
    this.filtSvgSafeHtml = this.svgSafeHtml.slice(startIndex, endIndex);
    console.log(this.filtSvgSafeHtml)
  } 
}

