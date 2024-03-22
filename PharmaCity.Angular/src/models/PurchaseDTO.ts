import { Petition } from "./Petition";
import { Pharmacy } from "./Pharmacy";

export class PurchaseDTO{

    id: number;
    purchaseCode: string;
    shopping: Array<Petition>;
    state: string;

    constructor(){
        this.id = 0;
        this.purchaseCode = "";
        this.shopping = [];
        this.state = "";
    }
}