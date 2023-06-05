import { Component } from '@angular/core';
import { ApiResultResponseListMaterial } from 'src/app/models/apiResultResponseListMaterial.model';
import { MaterialsService } from 'src/app/components/services/materials.service'
import { ApiResultResponseMaterialModel } from '../../../models/apiResultResponseMaterialModel.model';

@Component({
  selector: 'app-material-list',
  templateUrl: './material-list.component.html',
  styleUrls: ['./material-list.component.css']
})

export class MaterialListComponent {
  hasNoMaterials: boolean = false;  
  apiResult: Partial<ApiResultResponseListMaterial> = {};
  responseMaterial: Partial<ApiResultResponseMaterialModel> = {};

  constructor(private materialsService: MaterialsService) { };
  ngOnInit(): void {
    this.materialsService.getAllMaterials().subscribe(
      {
        next: (apiResultResponseListModel) => {
          this.apiResult = apiResultResponseListModel; console.log(this.apiResult);
          this.hasNoMaterials = !(this.apiResult.result!.length > 0);
        },
        error: (response) => { console.log(response); console.log("Р С•РЎв‚¬Р С‘Р В±Р С”Р В°!"); }
      }
    )
  }

  setIdForButton(Id: number) {
    console.log(Id);
    var materialLine = document.getElementById('material-' + Id + '')
    console.log(materialLine);
    var materialFullName = materialLine!.getElementsByTagName('td')[0].textContent;
    var btnDelCard = document.getElementsByClassName("btnDelCard")[0];
    var modal_body = document.getElementById("modal_body");
    modal_body!.innerHTML = 'Вы действительно хотите удалить материал ' + materialFullName + '?';
    btnDelCard.setAttribute('id', ''+ Id +'');
  }

  delMaterial(event: Event) {
    event.preventDefault();
    var target = event.target || event.currentTarget;
    if (target instanceof Element) {
      var id = target.getAttribute("id")
      var idInt: number = + id!;
      var materialLine = document.getElementById('material-' + id + '')
      materialLine!.remove();
      console.log(id)
      this.materialsService.deleteMaterial(idInt!).subscribe
        ({
          next: (response: ApiResultResponseMaterialModel) => {
            this.responseMaterial = response; console.log(response);
          },
          error: (response) => { console.log(response) }
        })
    }
  }

}
