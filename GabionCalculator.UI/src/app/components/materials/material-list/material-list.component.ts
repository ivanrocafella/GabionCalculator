import { Component } from '@angular/core';
import { Material } from 'src/app/models/material.model';
import { MaterialsService } from 'src/app/components/services/materials.service'

@Component({
  selector: 'app-material-list',
  templateUrl: './material-list.component.html',
  styleUrls: ['./material-list.component.css']
})

export class MaterialListComponent {
  listing: Material[] = [];
  constructor(private materialsService: MaterialsService) { };
  ngOnInit(): void {
    this.materialsService.getAllMaterials().subscribe(
      {
        next: (materials) => {
          this.listing = materials; console.log(this.listing);
        },
        error: (response) => { console.log(response); }
      }
    )
  }
}
