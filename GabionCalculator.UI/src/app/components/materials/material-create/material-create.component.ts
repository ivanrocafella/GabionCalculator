import { Component } from '@angular/core';
import { ApiResultCreateMaterialModel } from 'src/app/models/apiResultCreateMatrialModel.model';
import { MaterialsService } from 'src/app/components/services/materials.service';

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

  constructor(private materialsService: MaterialsService) { };

  ngOnInit(): void {
    this.materialsService.getCreateMaterialModel().subscribe(
      {
        next: (ApiResultCreateMaterialModel) => {
          this.apiResult = ApiResultCreateMaterialModel;
          this.DefaultName = this.apiResult.result?.Names[0];
          this.DefaultKindMaterial = this.apiResult.result?.KindsMaterial[0];
          this.KindsMaterial = this.apiResult.result?.KindsMaterial;
          console.log(this.apiResult);
          console.log(this.DefaultName);
          const mySelect = document.getElementById('Name') as HTMLSelectElement;
          const myOption = document.getElementById('' + this.DefaultName + '') as HTMLOptionElement;
          console.log(myOption);
          console.log(mySelect);
          const optionToSelect = mySelect.firstChild as HTMLOptionElement;
          console.log(optionToSelect);
        },
        error: (response) => { console.log(response); }
      }
    ) 
  };

  onSubmit(): void { this.materialsService.submitForm(this.formData) };

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
