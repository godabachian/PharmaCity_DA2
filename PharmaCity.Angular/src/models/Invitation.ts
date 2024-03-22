export class Invitation {

    id: number;
    userName: string;
    pharmacyName: string;
    role: number;
    code: string;
    state: string;

    constructor() {
        this.id = 0;
        this.userName = "";
        this.role = 0;
        this.code = "";
        this.state = "";
        this.pharmacyName = "";
    }

}