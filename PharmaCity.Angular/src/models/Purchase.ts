import { Petition } from "./Petition";
import { Pharmacy } from "./Pharmacy";

export class Purchase{

    id: number;
    code: string;
    shopping: Array<Petition>;

    constructor(){
        this.id = 0;
        this.code = "";
        this.shopping = [];
    }
}