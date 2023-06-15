import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { ApiResultResponseListGabion } from 'src/app/models/apiResultResponseListGabionModel.model';
import { ResponseGabionModel } from 'src/app/models/responseGabionModel.model';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DateAdapter } from '@angular/material/core';
import { MaterialsService } from 'src/app/components/services/materials.service'
import { ApiResultResponseListMaterial } from '../../../models/apiResultResponseListMaterial.model';
import { ResponseMaterialModel } from '../../../models/responseMaterialModel.model';



@Component({
  selector: 'app-gabion-list',
  templateUrl: './gabion-list.component.html',
  styleUrls: ['./gabion-list.component.css']
})
export class GabionListComponent implements OnInit {


  apiResultListGabion: Partial<ApiResultResponseListGabion> = {};
  apiResultListMaterial: Partial<ApiResultResponseListMaterial> = {};
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
  listMaterials: ResponseMaterialModel[] = [];

  //Filters
  filterDateFrom: Date | null = null;
  filterDateBefore: Date | null = null;
  filterByExecut: string | null = null;
  filterMaterialName: string | null = null;

  constructor(private gabions: GabionsService
    , private sanitizer: DomSanitizer
    , private dateAdapter: DateAdapter<any>
    , private materials: MaterialsService) { 
  }


  ngOnInit(): void {
    this.ruLocale();
    this.materials.getAllMaterials().subscribe(
      {
        next: (apiResultResponseListModel) => {
          this.apiResultListMaterial = apiResultResponseListModel; console.log(this.apiResultListMaterial);
          this.listMaterials = apiResultResponseListModel.result;
          this.listMaterials.sort((a, b) => a.FullName.localeCompare(b.FullName));
        },
        error: (response) => { console.log(response); }
      }
    )
    this.getSrverData(this.itemsPerPage, this.currentPage);
  }

  public getSrverData(itemsPerPage: number, currentPage: number) {
    this.gabions.getGabions(itemsPerPage, currentPage).subscribe(
      {
        next: (ApiResultListGabion) => {
          this.apiResultListGabion = ApiResultListGabion; console.log(this.apiResultListGabion);

          this.listGabions = ApiResultListGabion.result!;
          this.totalItems = ApiResultListGabion.additNum;

          //Filtration
          this.listGabions = this.listGabions
            .filter(gabion => this.filterDateFrom ? new Date(gabion.DateStart) >= this.filterDateFrom : true);
          this.listGabions = this.listGabions
            .filter(gabion => this.filterDateBefore ? new Date(gabion.DateStart) <= this.filterDateBefore : true);
          this.listGabions = this.listGabions
            .filter(gabion => this.filterByExecut ? gabion.User?.UserName!.toLowerCase().includes(this.filterByExecut.toLowerCase()) : true);
          this.listGabions = this.listGabions
            .filter(gabion => this.filterMaterialName ? gabion.Material?.FullName! === this.filterMaterialName : true);
          console.log(this.filterMaterialName);

          this.paginator._intl.itemsPerPageLabel = "Элементов на странице";
          this.paginator._intl.nextPageLabel = "Следующая страница";
          this.paginator._intl.previousPageLabel = "Предыдущая страница";
          this.paginator._intl.lastPageLabel = "Последняя страница";
          this.paginator._intl.firstPageLabel = "Первая страница";

          this.paginator.pageSize = this.itemsPerPage;
          this.paginator.pageIndex = this.currentPage;

          var parser = new DOMParser();
          var svgElem;
          var svgStr;
          this.svgSafeHtml = [];
          var sanitizedHtml;
          this.listGabions.forEach(
            (value) => {
              svgElem = parser.parseFromString(value.Svg!, "image/svg+xml").documentElement;
              svgElem.setAttribute('style', 'height: auto; width: 100%;');
              svgStr = new XMLSerializer().serializeToString(svgElem);
              sanitizedHtml = this.sanitizer.bypassSecurityTrustHtml(svgStr);
              this.svgSafeHtml.push(sanitizedHtml);
            })
        },
        error: (err: HttpErrorResponse) => {
          console.log(err.message);
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

  ruLocale() {
    this.dateAdapter.setLocale('ru-Ru');
  }

  cleareFilters() {
    this.filterDateFrom = null;
    this.filterDateBefore = null;
    this.filterByExecut = null;
    this.filterMaterialName = null;
    this.getSrverData(this.itemsPerPage, this.currentPage);
  }
}



