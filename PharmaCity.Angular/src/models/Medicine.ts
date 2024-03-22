export class Medicine {

    id: number;
    code: string;
    name: string;
    presentation: string;
    unit: string;
    receipt: string;
    symptoms: string;
    price: number;
    quantity: number;
    stock: number;
    pharmacy: string;

    constructor() {
        this.id = 0;
        this.code = "";
        this.name = "";
        this.presentation = "";
        this.unit = "";
        this.receipt = "";
        this.symptoms = "";
        this.price = 0;
        this.quantity = 0;
        this.stock = 0;
        this.pharmacy = "";
    }

}