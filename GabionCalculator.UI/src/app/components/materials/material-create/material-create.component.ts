import { Component } from '@angular/core';
import { ApiResultCreateMaterialModel } from 'src/app/models/apiResultCreateMatrialModel.model';
import { MaterialsService } from 'src/app/components/services/materials.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-material-create',
  templateUrl: './material-create.component.html',
  styleUrls: ['./material-create.component.css']
})

export class MaterialCreateComponent {
  apiResult: Partial<ApiResultCreateMaterialModel> = {};
  DefaultName: string | undefined;
  DefaultKindMaterial: string | undefined;
  KindsMaterial: string[] | undefined;
  formData: any = {};
  createMaterialForm!: FormGroup

  constructor(private materialsService: MaterialsService) {
  };

  ngOnInit(): void {
    this.createMaterialForm = new FormGroup(
      {
        Name: new FormControl("", [Validators.required]),
        Size: new FormControl("", [Validators.required, Validators.min(0.5)]),
        MaterialKindId: new FormControl("", [Validators.required]),
        PricePerKg: new FormControl("", [Validators.required, Validators.min(0)])
      }
    );

    this.materialsService.getCreateMaterialModel().subscribe(
      {
        next: (ApiResultCreateMaterialModel) => {
          this.apiResult = ApiResultCreateMaterialModel;
          this.DefaultName = this.apiResult.result?.Names[0];
          this.DefaultKindMaterial = this.apiResult.result?.KindsMaterial[0];
          this.KindsMaterial = this.apiResult.result?.KindsMaterial;
          this.formData.Name = this.DefaultName;
          this.formData.MaterialKindId = this.apiResult.result?.KindsMaterial.findIndex(x => x == this.apiResult.result?.KindsMaterial[0]);
          console.log(this.apiResult);
          console.log(this.DefaultName);
          console.log(this.formData.MaterialKindId);
        },
        error: (response) => { console.log(response); }
      }
    ) 
  };

  public validateControl = (controlName: string) => {
    return this.createMaterialForm.get(controlName)!.invalid && this.createMaterialForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.createMaterialForm.get(controlName)!.hasError(errorName)
  }

  onSubmit(): void { this.materialsService.submitFormPost(this.formData) };





  ngAfterViewInit() {
    console.log("DOM fully loaded and parsed");
    const mySelect = document.getElementById('Name') as HTMLSelectElement;
    console.log(mySelect);
    const myOption = document.getElementById('' + this.DefaultName + '') as HTMLOptionElement;
    console.log(myOption);
    const optionForSelect = mySelect.options.item(0) as HTMLOptionElement;
    console.log(optionForSelect);
  };

}
