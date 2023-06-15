import { Component, Input, OnInit } from '@angular/core';
import { ApiResultUpdateCostWorkModel } from 'src/app/models/apiResultUpdateCostWorklModel.model';
import { ApiResultResponseCostWorkModel } from 'src/app/models/apiResultResponseCostWorklModel.model';
import { CostWorkService } from 'src/app/components/services/costWork.service';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-cost-work-update',
  templateUrl: './cost-work-update.component.html',
  styleUrls: ['./cost-work-update.component.css']
})
export class CostWorkUpdateComponent implements OnInit{
  apiResultUpdateCostWorkModel: Partial<ApiResultUpdateCostWorkModel> = {};
  id: number = 0;
  formData: any = {};
  editCosWorklForm!: FormGroup

  constructor(private costWorkService: CostWorkService, private route: ActivatedRoute, private snackBar: MatSnackBar) { };

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.editCosWorklForm = new FormGroup(
      {
        ExchangeDollar: new FormControl("", [Validators.required, Validators.min(0)]),
        TimeWeldingOneCrossBar: new FormControl("", [Validators.required, Validators.min(0)]),
        TimeSettingEguip: new FormControl("", [Validators.required, Validators.min(0)]),
        PNR: new FormControl("", [Validators.required, Validators.min(0)]),
        Margin: new FormControl("", [Validators.required, Validators.min(0)]),
      }
    );

    this.costWorkService.getUpdateCostWorkModel(this.id).subscribe(
      {
        next: ApiResultUpdateCostWorkModel => {
          this.apiResultUpdateCostWorkModel = ApiResultUpdateCostWorkModel;
          this.formData.ExchangeDollar = ApiResultUpdateCostWorkModel.result.ExchangeDollar;
          this.formData.TimeWeldingOneCrossBar = ApiResultUpdateCostWorkModel.result.TimeWeldingOneCrossBar;
          this.formData.TimeSettingEguip = ApiResultUpdateCostWorkModel.result.TimeSettingEguip;
          this.formData.PNR = ApiResultUpdateCostWorkModel.result.PNR;
          this.formData.Margin = ApiResultUpdateCostWorkModel.result.Margin;
          console.log(this.apiResultUpdateCostWorkModel);
        },
        error: (err: HttpErrorResponse) => {
          console.log(err.message);
        }
      }
    )
  };

  public validateControl = (controlName: string) => {
    return this.editCosWorklForm.get(controlName)?.invalid && this.editCosWorklForm.get(controlName)?.touched;
  };

  public hasError = (controlName: string, errorName: string) => {
    return this.editCosWorklForm.get(controlName)?.hasError(errorName);
  };

  onEdit(): void {
    this.costWorkService.submitFormPut(this.id, this.formData).subscribe({
      next: ApiResultResponseCostWorkModel => {
        console.log(ApiResultResponseCostWorkModel.result);
        this.snackBar.open("Стоимость работ успешно изменена!", "Закрыть", {
          duration: 5000, // Длительность отображения в миллисекундах
        });
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open('Упс! Произошла ошибка: ' + err.message + '', "Закрыть", {
          duration: 5000, // Длительность отображения в миллисекундах
        });
      }
    })
  }

  restrictMaxLength(idInput: string, maxLength: number) {
    var input = document.getElementById(idInput) as HTMLInputElement;
    if (input != null) {
      if (input.value.length > maxLength) {
        input.value = input.value.slice(0, maxLength); // Обрезаем значение до максимальной длины
      }
    } 
  }

}
