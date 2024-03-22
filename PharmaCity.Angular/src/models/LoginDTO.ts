export class LoginDTO{

    user: string;
    token: string;
    role: string;

    constructor(){
        this.user = "";
        this.token = "";
        this.role = "";
    }
}