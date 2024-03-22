import { Component, OnInit } from '@angular/core';
import { PharmacyService } from 'src/app/services/pharmacy.service';
import { Pharmacy } from 'src/models/Pharmacy';

@Component({
  selector: 'app-pharmacy-list',
  templateUrl: './pharmacy-list.component.html',
  styleUrls: ['./pharmacy-list.component.css']
})
export class PharmacyListComponent implements OnInit {

  pharmacies: Pharmacy[]
  constructor(private pharmacyService: PharmacyService) { 
    this.pharmacies = [];
  }

  ngOnInit(): void {
    this.GetPharmacies();
  }

  GetPharmacies(): void{
    this.pharmacyService.GetPharmacies().subscribe(
      (res) => {
        this.pharmacies = res;
      },
      (err) => {
        alert(err.error);
      }
    );
  }

}
