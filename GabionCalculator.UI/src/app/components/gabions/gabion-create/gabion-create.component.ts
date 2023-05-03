import { Component } from '@angular/core';
import { ApiResultCreateGabionModel } from 'src/app/models/apiResultCreateGabionModel.model';
import { GabionsService } from 'src/app/components/services/gabions.service';

@Component({
  selector: 'app-gabion-create',
  templateUrl: './gabion-create.component.html',
  styleUrls: ['./gabion-create.component.css']
})
export class GabionCreateComponent {
  apiResult: Partial<ApiResultCreateGabionModel> = {};
  formData: any = {};
  imageUrl: string = 'assets/images/gabion.png';

  constructor(private gabionService: GabionsService) {
    this.formData.MaterialId = 0;
  };

  ngOnInit(): void {
    this.gabionService.getCreateGabionModel().subscribe(
      {
        next: (ApiResultCreateGabionModel) => {
          this.apiResult = ApiResultCreateGabionModel; console.log(this.apiResult);
        },
        error: (response) => { console.log(response); }
      }
    )
  };

  onSubmit(): void { this.gabionService.submitForm(this.formData); console.log(this.formData) };

  setMaterialDiameter(Id: number) {
    if (Id > 0) {
      const foundMaterial = this.apiResult.result?.Materials.find(obj => obj.Id == Id);      
      if (foundMaterial != null) {
        this.formData.MaterialDiameter = foundMaterial.Size;
      }      
    }
    else {
      this.formData.MaterialDiameter = "";
    }
  };
  
}
