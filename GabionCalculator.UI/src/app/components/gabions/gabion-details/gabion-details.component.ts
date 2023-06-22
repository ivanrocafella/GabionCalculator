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
export class GabionDetailsComponent implements AfterViewInit {
  apiResultResponseGabion: Partial<ApiResultResponseGabionModel> = {};
  id!: number;
  svgSafeHtml?: SafeHtml;
  dateCreate: Date = new Date();
  

  constructor(private gabionService: GabionsService, private route: ActivatedRoute
    , private sanitizer: DomSanitizer, private datePipe: DatePipe) { }

  ngAfterViewInit(): void {
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
          this.makeContent();
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

    var priceWeightStr = '<div class="card-body" id="PriceWeight">' +
      '<h2 class="card-title mb-1" ># ' + this.apiResultResponseGabion.result?.Id + '</h2>' +
      '<p class="card-text"> Кол-во: ' + this.apiResultResponseGabion.result?.Quantity + ' шт</p>' +
      '<p class="card-text"> Себестоимость: ' + this.roundNumber(this.apiResultResponseGabion.result?.Sebes, true) + ' сом</p>' +
      '<p class="card-text"> Общая себестоимость: ' + this.roundNumber(this.apiResultResponseGabion.result?.BatchSebes, true) + ' сом</p>' +
      '<p class="card-text"> Цена: ' + this.roundNumber(this.apiResultResponseGabion.result?.Price, true) + ' сом </p>' +
      '<p class="card-text"> Общая стоимость: ' + this.roundNumber(this.apiResultResponseGabion.result?.BatchPrice, true) + ' сом</p>' +
      '<p class="card-text"> Расход мат - ла на 1 шт: ' + this.roundNumber(this.apiResultResponseGabion.result?.MaterialTotalLength, true) + ' м</p>' +
      '<p class="card-text"> Вес 1 шт: ' + this.roundNumber(this.apiResultResponseGabion.result?.Weight, false) + ' кг</p>' +
      '<p class="card-text"> Вес ' + this.apiResultResponseGabion.result?.Quantity + ' шт: ' + this.roundNumber(this.apiResultResponseGabion.result?.BatchWeight, false) + ' кг</p>' +
      '<p class="card-text">Цена мат-ла на 1 шт: ' + this.roundNumber(this.apiResultResponseGabion.result?.PriceMaterial, true) + ' сом</p>' +
      '<p class="card-text">Общая стоим-ть мат-ла: ' + this.roundNumber(this.apiResultResponseGabion.result?.PriceMaterialBatch, true) + ' сом</p>' +
      '<p class="card-text">Ширина карты: ' + this.apiResultResponseGabion.result?.CardWidth + ' мм</p>' +
      '<p class="card-text">Высота карты: ' + this.apiResultResponseGabion.result?.CardHeight + ' мм</p>' +
      '<p class="card-text">Материал: ' + this.apiResultResponseGabion.result?.Material?.FullName + '</p>' +
      '<p class="card-text">Исполнитель: ' + this.apiResultResponseGabion.result?.User?.UserName + '</p></div>';
                                    
    notes_calculation.innerHTML = priceWeightStr;
  }

  makeContent() {
    this.makeContentDraw();
    this.makeContentCalculation();
  }
}
