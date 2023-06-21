import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ApiResultResponseGabionModel } from 'src/app/models/apiResultResponseGabionModel.model';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-gabion-details',
  templateUrl: './gabion-details.component.html',
  styleUrls: ['./gabion-details.component.css']
})
export class GabionDetailsComponent implements OnInit {
  apiResultResponseGabion: Partial<ApiResultResponseGabionModel> = {};
  id!: number;
  svgSafeHtml?: SafeHtml;
  dateCreate: Date = new Date();
  

  constructor(private gabionService: GabionsService, private route: ActivatedRoute
    , private sanitizer: DomSanitizer, private datePipe: DatePipe) { }

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

  roundNumber(value: number | undefined, rounded: boolean): number {
    if (typeof value == 'number') {
      if (rounded) {
        return Math.round(value);
      }
      else {
        return parseFloat(value.toFixed(1));
      }
    }
    return 0;
  }

  makeContentDraw() {
    var svg_draw = document.getElementById("svg_draw") as HTMLDivElement;
    svg_draw.innerHTML = this.apiResultResponseGabion.result?.Svg!;
    var stamp = '<table class="table table-bordered border-dark">' +
      '<thead>' +
      '<tr>' +
      '<th colspan="2" class="text-center">Габион Ø' + this.apiResultResponseGabion.result?.Material?.Size + ' ' + this.apiResultResponseGabion.result?.Width + 'x'
      + this.apiResultResponseGabion.result?.Length + 'x' + this.apiResultResponseGabion.result?.Height + ', ячейка '
      + this.apiResultResponseGabion.result?.CellHeight + 'x' + this.apiResultResponseGabion.result?.CellWidth + '</th>' +
      '<td class="text-left">Тип заказа:</th>' +
      '<td class="text-center">' + this.apiResultResponseGabion.result?.Material?.FullName + '</td>' +
      '</tr>' +
      '</thead>' +
      '<tbody>' +
      '<tr>' +
      '<th scope="col" class="w-25">Исполнитель</th>' +
      '<td class="w-30">' + this.apiResultResponseGabion.result?.User?.UserName + '</td>' +
      '<td class="w-25"></td>' +
      '<td class="text-center w-20">' + this.datePipe.transform(this.dateCreate, 'dd.MM.yyyy') + '</td>' +
      '</tr>' +
      '</tbody>' +
      '</table>';
    var notes =
      '<div><p class="card-text fw-bold">Кол - во: ' + this.apiResultResponseGabion.result?.Quantity + ' шт.</p>' +
      '<p class="card-text">1. Размер карты-заготовки ' + this.apiResultResponseGabion.result?.CardWidth + 'x' + this.apiResultResponseGabion.result?.CardHeight + ' мм</p>' +
      '<p class="card-text">2. Неуказанные предельные отклонения размеров: H14, h14, ±t2/2</p>' +
      '<p class="card-text d-flex justify-content-between"></span><span style="text-align: right;"></span></p></div>';

    var stamp_draw = document.getElementById("stamp_draw") as HTMLDivElement;
    stamp_draw.innerHTML = stamp;
    var notes_draw = document.getElementById("notes_draw") as HTMLDivElement;
    notes_draw.innerHTML = notes;
  }

  makeContentCalculation() {
    var svg_calculation = document.getElementById("svg_calculation") as HTMLDivElement;
    var notes_calculation = document.getElementById("notes_calculation") as HTMLDivElement;
    svg_calculation.innerHTML = this.apiResultResponseGabion.result?.Svg!;

    var PriceWeightDiv = document.getElementById("PriceWeight") as HTMLDivElement;
    console.log(PriceWeightDiv)
    notes_calculation.innerHTML = PriceWeightDiv.innerHTML;
  }

  makeContent() {
    this.makeContentDraw();
    this.makeContentCalculation();
  }
}
