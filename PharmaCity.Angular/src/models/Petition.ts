import { State } from "./State";

export class Petition {

    id: number;
    medicineCode: string;
    quantity: number;
    state: State;
    stateString: string;

    constructor(){
        this.id = 0;
        this.medicineCode = "";
        this.quantity = 0;
        this.state = State.Inactive;
        this.stateString = "";
    }
}