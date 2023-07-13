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
import { ApiResultResponseGabionModel } from '../../../models/apiResultResponseGabionModel.model';
import { UsersService } from 'src/app/components/services/users.service';



@Component({
  selector: 'app-gabion-list',
  templateUrl: './gabion-list.component.html',
  styleUrls: ['./gabion-list.component.css']
})
export class GabionListComponent implements OnInit {
  public isAdmin!: boolean;
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
    , private materials: MaterialsService
    , private users: UsersService) {
    this.users.isAdminChanged
      .subscribe(res => {
        this.isAdmin = res;
      })
  }


  ngOnInit(): void {
    if (this.users.isUserAdmin())
      this.users.sendIsAdminStateChangeNotification(true);
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
    this.getSrverData(this.itemsPerPage, this.currentPage, this.filterDateFrom, this.filterDateBefore, this.filterByExecut, this.filterMaterialName);
  }

  public getSrverData(itemsPerPage: number, currentPage: number, filterDateFrom: Date | null, filterDateBefore: Date | null
                      , filterByExecut: string | null, filterMaterialName: string | null) {
    this.gabions.getGabions(itemsPerPage, currentPage, filterDateFrom, filterDateBefore, filterByExecut, filterMaterialName).subscribe(
      {
        next: (ApiResultListGabion) => {
          this.apiResultListGabion = ApiResultListGabion; console.log(this.apiResultListGabion);

          this.listGabions = ApiResultListGabion.result!;
          this.totalItems = ApiResultListGabion.additNum;
          console.log(this.totalItems)

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
    this.getSrverData(this.itemsPerPage, this.currentPage, this.filterDateFrom, this.filterDateBefore, this.filterByExecut, this.filterMaterialName);
  }

  ruLocale() {
    this.dateAdapter.setLocale('ru-Ru');
  }

  cleareFilters() {
    this.filterDateFrom = null;
    this.filterDateBefore = null;
    this.filterByExecut = null;
    this.filterMaterialName = null;
    this.getSrverData(this.itemsPerPage, this.currentPage, this.filterDateFrom, this.filterDateBefore, this.filterByExecut, this.filterMaterialName);
  }

  setIdForButton(Id: number) {
    console.log(Id);
    var gabionLine = document.getElementById('card-' + Id + '')
    console.log(gabionLine);
    var gabionName = gabionLine!.getElementsByTagName('div')[0]
      .getElementsByTagName('div')[0]
      .getElementsByTagName('div')[1]
      .getElementsByTagName('div')[0].getElementsByTagName('h4')[0]
      .textContent?.replace('Наименование: ', '');
    console.log(gabionName);
    var btnDelCard = document.getElementsByClassName("btnDelCard")[0];
    var modal_body = document.getElementById("modal_body");
    modal_body!.innerHTML = 'Вы действительно хотите удалить ' + gabionName + '?';
    btnDelCard.setAttribute('id', Id.toString());
  }

  delGabion(event: Event) {
    event.preventDefault();
    var target = event.target || event.currentTarget;
    if (target instanceof Element) {
      var id = parseInt(target.getAttribute("id")!)
      var gabionLine = document.getElementById('card-' + id + '')
      gabionLine!.remove();
      console.log(id)
      this.gabions.deleteGabion(id!).subscribe
        ({
          next: (response: ApiResultResponseGabionModel) => {console.log(response); this.totalItems--},
          error: (response) => { console.log(response) }
        })
    }
  }
}



