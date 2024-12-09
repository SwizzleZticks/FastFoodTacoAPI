import { CurrencyPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Combo } from '../../interfaces/combo';
import { ComboApiService } from '../../services/combo-api.service';

@Component({
  selector: 'app-combo-list',
  standalone: true,
  imports: [ CurrencyPipe ],
  templateUrl: './combo-list.component.html',
  styleUrl: './combo-list.component.css'
})
export class ComboListComponent implements OnInit {
  combos: Combo[] = [];

  constructor(private comboApiService: ComboApiService){}

  async ngOnInit() {
    this.combos = await this.comboApiService.getDrinkList();
    console.log(this.combos);
  };
}
