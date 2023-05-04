import { Component } from '@angular/core';
import { ApiResultCreateGabionModel } from 'src/app/models/apiResultCreateGabionModel.model';
import { ApiResultResponseGabionModel } from 'src/app/models/apiResultResponseGabionModel.model';
import { GabionsService } from 'src/app/components/services/gabions.service';

@Component({
  selector: 'app-gabion-create',
  templateUrl: './gabion-create.component.html',
  styleUrls: ['./gabion-create.component.css']
})
export class GabionCreateComponent {
  apiResultCreateGab: Partial<ApiResultCreateGabionModel> = {};
  apiResultTempGab: Partial<ApiResultResponseGabionModel> = {};
  formData: any = {};
  imageUrl: string = 'assets/images/gabion.png';
  divSvg: any = {};
  divDescriptAnchor: any = {};

  constructor(private gabionService: GabionsService) {
    this.formData.MaterialId = 0;
    this.formData.MaterialDiameter = 0;
  };

  ngOnInit(): void {
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
           this.divDescriptAnchor.style.display = "block";
           if (this.apiResultTempGab.result?.Svg != null) {
             this.divSvg.innerHTML = this.apiResultTempGab.result.Svg;
           }          
        },
        error: (response) => {
          console.log('Form has not been submitted', response); }
      }
    )
  };

  setMaterialDiameter(Id: number) {
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
  
}
