import { Component, OnInit } from '@angular/core';
import { StockRequestsService } from 'src/app/services/stock-requests.service';
import { Petition } from 'src/models/Petition';
import { StockRequest } from 'src/models/StockRequest';

@Component({
  selector: 'app-stock-request-list',
  templateUrl: './stock-request-list.component.html',
  styleUrls: ['./stock-request-list.component.css']
})
export class StockRequestListComponent implements OnInit {

  stockRequests: StockRequest[];
  selectedStockRequest: StockRequest;
  token: string|null;
  petitions: Petition[];
  idRequest: number;
  constructor(private stockRequestService: StockRequestsService) { 
    this.stockRequests = [];
    this.token = localStorage.getItem('token');
    this.petitions = [];
    this.idRequest = 0;
    this.selectedStockRequest = new StockRequest;
  }

  ngOnInit(): void {
    this.GetStockRequests();
  }

  GetInputIdNumber(idNumber:string){
    this.idRequest = Number(idNumber);
  }

  GetStockRequests(){
    this.stockRequestService.GetStockRequests(this.token).subscribe(
      (res) => {
        this.stockRequests = res;
      },
      (err) => {
        alert(err.error);
      }
    );
  }

  ChargePetitions(stockRequest:StockRequest){
    this.petitions = stockRequest.petitions;
    this.selectedStockRequest = stockRequest;
  }

  AcceptPetition(){
    if(this.selectedStockRequest.id == 0){
      alert("Debes seleccionar una solicitud y luego aceptarla.");
    }else{
      this.stockRequestService.AcceptStockRequest(this.selectedStockRequest.id,this.token).subscribe(
        (res) =>{
          alert("Se ha aceptado correctamente la solicitud de stock.");
          this.ngOnInit();
          this.petitions = [];
          this.selectedStockRequest = new StockRequest;
        },
        (err) =>{
          alert(err.error.errorMessage);
        }
      );
    }
  }

  DeclinePetition(){
    if(this.selectedStockRequest.id == 0){
      alert("Debes seleccionar una solicitud y luego rechazarla.");
    }else{
      this.stockRequestService.DeclineStockRequest(this.selectedStockRequest.id,this.token).subscribe(
        (res) =>{
          alert(res);
          this.ngOnInit();
          this.petitions = [];
          this.selectedStockRequest = new StockRequest;
        },
        (err) =>{
          alert(err.error.errorMessage);
        }
      );
    }
  }
}
