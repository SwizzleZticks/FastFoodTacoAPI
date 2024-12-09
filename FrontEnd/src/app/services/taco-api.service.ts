import { Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TacoApiService {

  private listOfTacos: any[] = []

  // Adjust this as needed.
  baseUrl: string = "https://localhost:7041/api";

  constructor(private http: HttpClient) { }

  async getTacoList(): Promise<any[]> {
    const theTacos: any[] = await lastValueFrom(this.http.get<any[]>(this.baseUrl + "/Tacos"));

    this.listOfTacos = theTacos;

    return this.listOfTacos;
  }
}
