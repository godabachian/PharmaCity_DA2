import { Petition } from "./Petition";

export class StockRequest{
    id: number;
    employeeUserName: string;
    petitions: Array<Petition>;

    constructor(){
        this.id = 0;
        this.employeeUserName = "";
        this.petitions = [];
    }
}