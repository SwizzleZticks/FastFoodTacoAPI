import { Drink } from "./drink";
import { Taco } from "./taco";

export interface Combo{
    id: number;
    name: string;
    tacoId: number;
    drinkId: string;
    cost: number;
    drink: Drink;
    taco: Taco;
}