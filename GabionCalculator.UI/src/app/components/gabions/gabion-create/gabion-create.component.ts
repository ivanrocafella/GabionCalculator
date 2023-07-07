import { Component, ViewChild, ElementRef } from '@angular/core';
import { ApiResultCreateGabionModel } from 'src/app/models/apiResultCreateGabionModel.model';
import { ApiResultResponseGabionModel } from 'src/app/models/apiResultResponseGabionModel.model';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { UsersService } from 'src/app/components/services/users.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CellDividinngValidatorService } from 'src/app/components/validators/cell-dividinng-validator.service';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-gabion-create',
  templateUrl: './gabion-create.component.html',
  styleUrls: ['./gabion-create.component.css']
})
export class GabionCreateComponent {
  @ViewChild('draw_card', { static: false }) draw_card!: ElementRef;

  kFactor: number = 0;
  bendRadius: number = 17.5;
  diameterMaterial: number = 0;
  length: number = 0;
  width: number = 0;
  apiResultCreateGab: Partial<ApiResultCreateGabionModel> = {};
  apiResultTempGab: Partial<ApiResultResponseGabionModel> = {};
  formData: any = {};
  imageUrl: string = './assets/images/gabion.png';
  divSvg: any = {};
  divDescriptAnchor: any = {};
  createGabionForm!: FormGroup;
  errorMessage: string = '';
  showError!: boolean;
  saveBtn?: HTMLButtonElement;
  drawingBtn?: HTMLButtonElement;
  calculationBtn?: HTMLButtonElement;
  isUserAuthenticated!: boolean;
  dateCreate: Date = new Date();

  constructor(private gabionService: GabionsService, private snackBar: MatSnackBar, private userService: UsersService
    , private divideValidator: CellDividinngValidatorService, private datePipe: DatePipe) {
    this.userService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  };

  initializingForm() {
    if (this.userService.isUserAuthenticated())
      this.userService.sendAuthStateChangeNotification(true);

    this.createGabionForm = new FormGroup({
      MaterialId: new FormControl(""),
      MaterialDiameter: new FormControl(""),
      Length: new FormControl("", [Validators.required, Validators.min(250)]),
      Width: new FormControl("", [Validators.required, Validators.min(250)]),
      Height: new FormControl("", [Validators.required, Validators.min(200), Validators.max(2000), this.divideValidator.dividingValidator(200, 2000, 100)]),
      CellHeight: new FormControl("", [Validators.required, Validators.min(50), Validators.max(100), this.divideValidator.dividingValidator(50, 100, 50)]),
      CellWidth: new FormControl("", [Validators.required, Validators.min(50), Validators.max(100), this.divideValidator.dividingValidator(50, 100, 50)]),
      Quantity: new FormControl("", [Validators.required, Validators.min(1)])
    });
    this.formData.MaterialId = 0;
    this.formData.MaterialDiameter = 0;
    this.showError = false;

    this.gabionService.getCreateGabionModel().subscribe(
      {
        next: (ApiResultCreateGabionModel) => {
          this.apiResultCreateGab = ApiResultCreateGabionModel; console.log(this.apiResultCreateGab);
        },
        error: (response) => { console.error(response); }
      }
    )
  }

  ngOnInit(): void {
    this.initializingForm();
  };

  ngAfterViewInit() {
    this.divSvg = document.getElementById("Svg") as HTMLDivElement;
    this.divDescriptAnchor = document.getElementById("DescriptAnchor") as HTMLDivElement;
    console.log(this.divSvg);
  };

  onSubmit(): void {
    if (this.saveBtn !== undefined) {
      this.saveBtn.disabled = false;
    }
    this.gabionService.submitForm(this.formData).subscribe(
      {
        next: (ApiResultResponseGabionModel) => {
          if (this.isUserAuthenticated) {
            this.drawingBtn = document.getElementById("drawingBtn") as HTMLButtonElement;
            this.calculationBtn = document.getElementById("calculationBtn") as HTMLButtonElement;
            this.drawingBtn.disabled = true;
            this.calculationBtn.disabled = true; 
           }
             
           this.showError = false;
           this.apiResultTempGab = ApiResultResponseGabionModel;
           console.log('Form submitted', this.apiResultTempGab);
           this.divDescriptAnchor.style.display = "flex";
          if (this.apiResultTempGab.result?.Svg != null) {
            var svg = this.apiResultTempGab.result!.Svg;            
            this.divSvg.innerHTML = svg;
            var svgHtmlElem = this.divSvg.querySelectorAll('svg')[0];
            console.log(svgHtmlElem);
            svgHtmlElem.setAttribute('style', 'height: auto; width: 130%;');
           }          
        },
        error: (err: HttpErrorResponse) => {
          console.log('Form has not been submitted', err.message);
            this.errorMessage = err.message;
            this.showError = true;
            console.log(err.message)
        }
      }
    )
  };

  post() {
    this.gabionService.post(this.apiResultTempGab.result!).subscribe(
      {
        next: (ApiResultGabionModel) => {
          this.makeContentCalculation();
          this.makeContentDraw();
          console.log(ApiResultGabionModel);
          this.drawingBtn = document.getElementById("drawingBtn") as HTMLButtonElement;
          this.calculationBtn = document.getElementById("calculationBtn") as HTMLButtonElement;
          this.saveBtn = document.getElementById("saveBtn") as HTMLButtonElement;
          this.saveBtn.disabled = true;
          this.drawingBtn!.disabled = false;
          this.calculationBtn!.disabled = false; 
          this.snackBar.open("Габион успешно сохранён!", "Закрыть", {
            duration: 5000, // Длительность отображения в миллисекундах
          });
        },
        error: (err: HttpErrorResponse) => {
          console.log(err.message)
          this.snackBar.open('Упс! Произошла ошибка: ' + err.message +'', "Закрыть", {
            duration: 5000, // Длительность отображения в миллисекундах
          });
        }
      }
    )
  };

  setMaterialDiameter(Id: number) {
    console.log(Id);
    if (Id > 0) {
      const foundMaterial = this.apiResultCreateGab.result?.Materials.find(obj => obj.Id == Id);      
      if (foundMaterial != null) {
        this.formData.MaterialDiameter = foundMaterial.Size;
      }      
    }
    else {
      this.formData.MaterialDiameter = 0;
    }
  };

  public validateControl = (controlName: string) => {
    return this.createGabionForm.get(controlName)!.invalid && this.createGabionForm.get(controlName)!.touched;
  }

  public validateControlForDissable = (controlName: string) => {
    return this.createGabionForm.get(controlName)!.invalid && (this.createGabionForm.get(controlName)!.touched || this.createGabionForm.get(controlName)!.dirty)
  }

  public hasError = (controlName: string, errorName: string ) => {
    return this.createGabionForm.get(controlName)!.hasError(errorName)  
  }

  public checkSize = (event: Event) => {
    var diameterMaterialInput = document.getElementById("MaterialDiameter") as HTMLInputElement;
    var lengthlInput = document.getElementById("Length") as HTMLInputElement;
    var widthInput = document.getElementById("Width") as HTMLInputElement;

    this.length = +lengthlInput;
    this.width = +widthInput;
    this.diameterMaterial = +diameterMaterialInput.value;
    if (this.diameterMaterial > 0) {
      this.kFactor = 1 / Math.log(1 + this.diameterMaterial / this.bendRadius) - this.bendRadius / this.diameterMaterial;
      console.log(this.kFactor);
    } 
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
    svg_draw.innerHTML = this.apiResultTempGab.result?.Svg!;
    var stamp = '<table class="table table-bordered border-dark">' +
      '<thead>' +
      '<tr>' +
      '<th colspan="2" class="text-center">Габион Ø' + this.apiResultTempGab.result?.Material?.Size + ' ' + this.apiResultTempGab.result?.Width + 'x'
      + this.apiResultTempGab.result?.Length + 'x' + this.apiResultTempGab.result?.Height + ', ячейка '
      + this.apiResultTempGab.result?.CellHeight + 'x' + this.apiResultTempGab.result?.CellWidth + '</th>' +
      '<td class="text-left">Тип заказа:</th>' +
      '<td class="text-center">' + this.apiResultTempGab.result?.Material?.FullName + '</td>' +
      '</tr>' +
      '</thead>' +
      '<tbody>' +
      '<tr>' +
      '<th scope="col" class="w-25">Исполнитель</th>' +
      '<td class="w-30">' + this.apiResultTempGab.result?.User?.UserName + '</td>' +
      '<td class="w-25"></td>' +
      '<td class="text-center w-20">' + this.datePipe.transform(this.dateCreate, 'dd.MM.yyyy') + '</td>' +
      '</tr>' +
      '</tbody>' +
      '</table>';
    var notes =
      '<div><p class="card-text fw-bold">Кол - во: ' + this.apiResultTempGab.result?.Quantity + ' шт.</p>' +
      '<p class="card-text">1. Размер карты-заготовки ' + this.apiResultTempGab.result?.CardWidth + 'x' + this.apiResultTempGab.result?.CardHeight + ' мм</p>' +
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
    svg_calculation.innerHTML = this.apiResultTempGab.result?.Svg!;

    var PriceWeightDiv = document.getElementById("PriceWeight") as HTMLDivElement;
    notes_calculation.innerHTML = PriceWeightDiv.innerHTML;
  }


}
