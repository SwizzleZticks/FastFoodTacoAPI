import { Component, OnInit } from '@angular/core';
import { DrinkApiService } from '../../services/drink-api.service';
import { CurrencyPipe } from '@angular/common';
import { Drink } from '../../interfaces/drink';

@Component({
  selector: 'app-drink-list',
  standalone: true,
  imports: [ CurrencyPipe ],
  templateUrl: './drink-list.component.html',
  styleUrl: './drink-list.component.css'
})
export class DrinkListComponent implements OnInit {
  drinks: Drink[] = [];

  constructor(private drinkApiService: DrinkApiService){}

  async ngOnInit() {
    this.drinks = await this.drinkApiService.getDrinkList();
  };
}
