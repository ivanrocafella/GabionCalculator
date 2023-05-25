import { Component, OnInit } from '@angular/core';
import { RepositoryService } from 'src/app/components/services/repository.service';

@Component({
  selector: 'app-privacy',
  templateUrl: './privacy.component.html',
  styleUrls: ['./privacy.component.css']
})
export class PrivacyComponent implements OnInit {
  public claims: [] = [];
  constructor(private _repository: RepositoryService) { }
  ngOnInit(): void {
    this.getClaims();
  }
  public getClaims = () => {
    this._repository.getClaims()
      .subscribe(res => {
        this.claims = res as [];
        for (var i = 0; i < this.claims.length; i++) {
          console.log(this.claims[i])
        }
      })
  }
}
