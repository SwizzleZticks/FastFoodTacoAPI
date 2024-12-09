import { Component, OnInit } from '@angular/core';
import { TacoApiService } from '../../services/taco-api.service';
import { Taco } from '../../interfaces/taco';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-taco-list',
  standalone: true,
  imports: [ CurrencyPipe ],
  templateUrl: './taco-list.component.html',
  styleUrl: './taco-list.component.css'
})
export class TacoListComponent  implements OnInit {
  tacos:Taco[] = [];

  constructor(private tacoApiService:TacoApiService){}

  async ngOnInit() {
       this.tacos = await this.tacoApiService.getTacoList();
  };
}
