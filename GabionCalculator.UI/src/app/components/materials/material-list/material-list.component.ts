import { Component } from '@angular/core';
import { ApiResultResponseListMaterial } from 'src/app/models/apiResultResponseListMaterial.model';
import { MaterialsService } from 'src/app/components/services/materials.service'

@Component({
  selector: 'app-material-list',
  templateUrl: './material-list.component.html',
  styleUrls: ['./material-list.component.css']
})

export class MaterialListComponent {
  apiResult: Partial<ApiResultResponseListMaterial> = {};
  constructor(private materialsService: MaterialsService) { };
  ngOnInit(): void {
    this.materialsService.getAllMaterials().subscribe(
      {
        next: (apiResultResponseListModel) => {
          this.apiResult = apiResultResponseListModel; console.log(this.apiResult);
        },
        error: (response) => { console.log(response); }
      }
    )
  }
}
