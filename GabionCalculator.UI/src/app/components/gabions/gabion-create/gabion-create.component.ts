import { Component } from '@angular/core';
import { ApiResultCreateGabionModel } from 'src/app/models/apiResultCreateGabionModel.model';
import { ApiResultResponseGabionModel } from 'src/app/models/apiResultResponseGabionModel.model';
import { GabionsService } from 'src/app/components/services/gabions.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

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

  constructor(private gabionService: GabionsService) {
  };

  ngOnInit(): void {
    this.createGabionForm = new FormGroup({
      MaterialId: new FormControl("", [Validators.required]),
      MaterialDiameter: new FormControl("", [Validators.required]),
      Length: new FormControl("", [Validators.required]),
      Width: new FormControl("", [Validators.required]),
      Height: new FormControl("", [Validators.required]),
      CellHeight: new FormControl("", [Validators.required]),
      CellWidth: new FormControl("", [Validators.required]),
      Quantity: new FormControl("", [Validators.required])
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
    this.gabionService.submitForm(this.formData).subscribe(
      {
        next: (ApiResultResponseGabionModel) => {           
           this.apiResultTempGab = ApiResultResponseGabionModel;
           console.log('Form submitted', this.apiResultTempGab);
           this.divDescriptAnchor.style.display = "flex";
           if (this.apiResultTempGab.result?.Svg != null) {
             this.divSvg.innerHTML = this.apiResultTempGab.result.Svg;
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
    return this.createGabionForm.get(controlName)!.invalid && this.createGabionForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
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
