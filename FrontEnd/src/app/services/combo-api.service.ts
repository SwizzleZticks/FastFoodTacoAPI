import { Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ComboApiService {

  private listOfCombos: any[] = [];

  // Adjust this as needed.
  private baseUrl: string = "https://localhost:7041/api";

  constructor(private http: HttpClient) { }

  async getDrinkList(): Promise<any[]> {
    this.listOfCombos = await lastValueFrom(this.http.get<any[]>(this.baseUrl + "/Combos"));

    return this.listOfCombos;
  }
}