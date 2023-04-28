import { Component } from '@angular/core';
import { ApiResultCreateMaterialModel } from 'src/app/models/apiResultCreateMatrialModel.model';
import { MaterialsService } from 'src/app/components/services/materials.service'

@Component({
  selector: 'app-material-create',
  templateUrl: './material-create.component.html',
  styleUrls: ['./material-create.component.css']
})
export class MaterialCreateComponent {
  apiResult: Partial<ApiResultCreateMaterialModel> = {};
  constructor(private materialsService: MaterialsService) { };
  ngOnInit(): void {
    this.materialsService.getCreateMaterialModel().subscribe(
      {
        next: (ApiResultCreateMaterialModel) => {
          this.apiResult = ApiResultCreateMaterialModel; console.log(this.apiResult);
        },
        error: (response) => { console.log(response); }
      }
    )
  }
}
