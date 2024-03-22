import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatListModule } from '@angular/material/list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatRadioModule } from '@angular/material/radio';
import { LayoutModule } from '@angular/cdk/layout';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatTreeModule } from '@angular/material/tree';


import { AppComponent } from './app.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HomeComponent } from './components/home/home.component';
import { EmailInputComponent } from './components/inputs/email-input/email-input.component';
import { PasswordInputComponent } from './components/inputs/password-input/password-input.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { InvitationListComponent } from './components/lists/invitation-list/invitation-list.component';
import { ViewerInvitationsComponent } from './components/viewers/viewer-invitations/viewer-invitations.component';
import { MedicineListComponent } from './components/lists/medicine-list/medicine-list.component';
import { ViewerMedicinesComponent } from './components/viewers/viewer-medicines/viewer-medicines.component';
import { StockRequestListComponent } from './components/lists/stock-request-list/stock-request-list.component';
import { ViewerStockRequestsComponent } from './components/viewers/viewer-stock-requests/viewer-stock-requests.component';
import { ShoppingListComponent } from './components/lists/shopping-list/shopping-list.component';
import { ViewerShoppingComponent } from './components/viewers/viewer-shopping/viewer-shopping.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { UsernameInputComponent } from './components/inputs/username-input/username-input.component';
import { DirectionInputComponent } from './components/inputs/direction-input/direction-input.component';
import { CodeInputComponent } from './components/inputs/code-input/code-input.component';
import { AddInvitationComponent } from './components/creators/add-invitation/add-invitation.component';
import { PharmacyInputComponent } from './components/inputs/pharmacy-input/pharmacy-input.component';
import { RoleSelectComponent } from './components/inputs/role-select/role-select.component';
import { CreatePharmacyComponent } from './components/creators/create-pharmacy/create-pharmacy.component';
import { CreateMedicineComponent } from './components/creators/create-medicine/create-medicine.component';
import { MedicineInputComponent } from './components/inputs/medicine-input/medicine-input.component';
import { MatSelectModule } from '@angular/material/select';
import { SymptomsInputComponent } from './components/inputs/symptoms-input/symptoms-input.component';
import { QuantityInputComponent } from './components/inputs/quantity-input/quantity-input.component';
import { PriceInputComponent } from './components/inputs/price-input/price-input.component';
import { PresentationInputComponent } from './components/inputs/presentation-input/presentation-input.component';
import { UnitInputComponent } from './components/inputs/unit-input/unit-input.component';
import { AddStockrequestComponent } from './components/creators/add-stockrequest/add-stockrequest.component';
import { MedicinecodeInputComponent } from './components/inputs/medicinecode-input/medicinecode-input.component';
import { PharmacyListComponent } from './components/lists/pharmacy-list/pharmacy-list.component';
import { ViewerPharmaciesComponent } from './components/viewers/viewer-pharmacies/viewer-pharmacies.component';
import { IdInputComponent } from './components/inputs/id-input/id-input.component';
import { CreateExportComponent } from './components/creators/create-export/create-export.component';
import { DeleteMedicineComponent } from './components/delete-medicine/delete-medicine.component';
import { ShoppingStateComponent } from './components/shopping-state/shopping-state.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    LoginComponent,
    DashboardComponent,
    HomeComponent,
    EmailInputComponent,
    PasswordInputComponent,
    NotFoundComponent,
    InvitationListComponent,
    ViewerInvitationsComponent,
    MedicineListComponent,
    ViewerMedicinesComponent,
    StockRequestListComponent,
    ViewerStockRequestsComponent,
    ShoppingListComponent,
    ViewerShoppingComponent,
    RegisterUserComponent,
    UsernameInputComponent,
    DirectionInputComponent,
    CodeInputComponent,
    AddInvitationComponent,
    PharmacyInputComponent,
    RoleSelectComponent,
    CreatePharmacyComponent,
    CreateMedicineComponent,
    MedicineInputComponent,
    SymptomsInputComponent,
    QuantityInputComponent,
    PriceInputComponent,
    PresentationInputComponent,
    UnitInputComponent,
    AddStockrequestComponent,
    MedicinecodeInputComponent,
    PharmacyListComponent,
    ViewerPharmaciesComponent,
    IdInputComponent,
    CreateExportComponent,
    DeleteMedicineComponent,
    ShoppingStateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    LayoutModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ScrollingModule,
    MatDialogModule,
    HttpClientModule,
    MatRadioModule,
    MatSnackBarModule,
    MatSelectModule,
    MatTreeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
export class SidenavAutosizeExample {
  showFiller = false;
}
