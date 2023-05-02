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
}
