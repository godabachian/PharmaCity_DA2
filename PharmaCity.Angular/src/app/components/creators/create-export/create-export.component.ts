import { Component, OnInit } from '@angular/core';
import { ExporterService } from 'src/app/services/exporter.service';

@Component({
  selector: 'app-create-export',
  templateUrl: './create-export.component.html',
  styleUrls: ['./create-export.component.css']
})
export class CreateExportComponent implements OnInit {

  exporters: string[];
  exporter: string;
  token: string|null;
  constructor(private exporterService: ExporterService) {
    this.exporters = [];
    this.exporter = "";
    this.token = localStorage.getItem('token');
  }

  ngOnInit(): void {
    this.GetExporters();
  }

  GetExporters() {
    this.exporterService.GetExporters(this.token).subscribe(
      (res) => {
        this.exporters = res;
      },
      (err) => {
        alert(err.error.errorMessage);
      }
    );
  }

  ExportMedicines() {
    if (this.exporter == "") {
      alert("Debes elegir un exportador.");
    } else {
      this.exporterService.ExportMedicines(this.exporter, this.token).subscribe(
        (res) => {
          alert("Se han exportado las medicinas en formato " + this.exporter + " correctamente.");
        },
        (err) => {
          alert("Se han exportado las medicinas en formato " + this.exporter + " correctamente.");
        }
      );
    }
  }

}
