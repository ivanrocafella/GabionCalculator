import { Component } from '@angular/core';
import { ApiResultCreateGabionModel } from 'src/app/models/apiResultCreateGabionModel.model';
import { ApiResultResponseGabionModel } from 'src/app/models/apiResultResponseGabionModel.model';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { UsersService } from 'src/app/components/services/users.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-gabion-create',
  templateUrl: './gabion-create.component.html',
  styleUrls: ['./gabion-create.component.css']
})
export class GabionCreateComponent {
  kFactor: number = 0;
  bendRadius: number = 17.5;
  diameterMaterial: number = 0;
  length: number = 0;
  width: number = 0;
  apiResultCreateGab: Partial<ApiResultCreateGabionModel> = {};
  apiResultTempGab: Partial<ApiResultResponseGabionModel> = {};
  formData: any = {};
  imageUrl: string = 'assets/images/gabion.png';
  divSvg: any = {};
  divDescriptAnchor: any = {};
  createGabionForm!: FormGroup;
  errorMessage: string = '';
  showError!: boolean;
  saveBtn?: HTMLButtonElement;
  isUserAuthenticated!: boolean;

  constructor(private gabionService: GabionsService, private snackBar: MatSnackBar, private userService: UsersService) {
    this.userService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  };

  ngOnInit(): void {
    if (this.userService.isUserAuthenticated())
      this.userService.sendAuthStateChangeNotification(true);

    this.createGabionForm = new FormGroup({
      MaterialId: new FormControl(""),
      MaterialDiameter: new FormControl(""),
      Length: new FormControl("", [Validators.required, Validators.min(250)]),
      Width: new FormControl("", [Validators.required, Validators.min(250)]),
      Height: new FormControl("", [Validators.required, Validators.min(200), Validators.max(2000)]),
      CellHeight: new FormControl("", [Validators.required, Validators.min(50), Validators.max(200)]),
      CellWidth: new FormControl("", [Validators.required, Validators.min(50), Validators.max(200)]),
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
           this.showError = false;
           this.apiResultTempGab = ApiResultResponseGabionModel;
           console.log('Form submitted', this.apiResultTempGab);
           this.divDescriptAnchor.style.display = "flex";
          if (this.apiResultTempGab.result?.Svg != null) {
            var svg = this.apiResultTempGab.result!.Svg;            
            this.divSvg.innerHTML = svg;
            var svgHtmlElem = this.divSvg.querySelectorAll('svg')[0];
            console.log(svgHtmlElem);
            svgHtmlElem.setAttribute('style','height: auto; width: 100%;');
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
          console.log(ApiResultGabionModel);
          this.saveBtn = document.getElementById("saveBtn") as HTMLButtonElement;
          this.saveBtn.disabled = true;
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

}
