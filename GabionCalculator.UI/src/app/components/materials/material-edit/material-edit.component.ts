import { Component } from '@angular/core';
import { ApiResultUpdateMaterialModel } from 'src/app/models/apiResultUpdateMaterialModel.model';
import { MaterialsService } from 'src/app/components/services/materials.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-material-edit',
  templateUrl: './material-edit.component.html',
  styleUrls: ['./material-edit.component.css']
})
export class MaterialEditComponent {
  id!: number;
  apiResult: Partial<ApiResultUpdateMaterialModel> = {};
  DefaultName: string | undefined;
  DefaultKindMaterial: string | undefined;
  DefaultKindMaterialId!: number;
  KindsMaterial: string[] | undefined;
  formData: any = {};
  editMaterialForm!: FormGroup

  constructor(private materialsService: MaterialsService, private route: ActivatedRoute) { };

  ngOnInit(): void {

    this.id = this.route.snapshot.params['id'];
    console.log(this.id);

    this.editMaterialForm = new FormGroup(
      {
        Name: new FormControl("", [Validators.required]),
        Size: new FormControl("", [Validators.required, Validators.min(0.5)]),
        MaterialKindId: new FormControl("", [Validators.required]),
        PricePerKg: new FormControl("", [Validators.required, Validators.min(0)])
      }
    );

    this.materialsService.getUpdateMaterialModel(this.id).subscribe(
      {
        next: (ApiResultUpdateMaterialModel) => {
          this.apiResult = ApiResultUpdateMaterialModel;
          this.DefaultName = this.apiResult.result?.Name;
          this.DefaultKindMaterialId = this.apiResult.result?.MaterialKindId!;
          this.KindsMaterial = this.apiResult.result?.KindsMaterial;
          this.formData.Name = this.DefaultName;         
          this.formData.MaterialKindId = this.DefaultKindMaterialId;
          this.formData.Size = this.apiResult.result?.Size;
          this.formData.PricePerKg = this.apiResult.result?.PricePerKg;
        },
        error: (response) => { console.log(response); }
      }
    )
  };

  public validateControl = (controlName: string) => {
    return this.editMaterialForm.get(controlName)!.invalid && this.editMaterialForm.get(controlName)!.touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.editMaterialForm.get(controlName)!.hasError(errorName)
  }

  onSubmit(): void { this.materialsService.submitFormPut(this.id, this.formData) };








}
