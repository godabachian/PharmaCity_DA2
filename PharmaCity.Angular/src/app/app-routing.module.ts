import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddInvitationComponent } from './components/creators/add-invitation/add-invitation.component';
import { AddStockrequestComponent } from './components/creators/add-stockrequest/add-stockrequest.component';
import { CreateExportComponent } from './components/creators/create-export/create-export.component';
import { CreateMedicineComponent } from './components/creators/create-medicine/create-medicine.component';
import { CreatePharmacyComponent } from './components/creators/create-pharmacy/create-pharmacy.component';
import { DeleteMedicineComponent } from './components/delete-medicine/delete-medicine.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { ShoppingStateComponent } from './components/shopping-state/shopping-state.component';
import { ViewerInvitationsComponent } from './components/viewers/viewer-invitations/viewer-invitations.component';
import { ViewerMedicinesComponent } from './components/viewers/viewer-medicines/viewer-medicines.component';
import { ViewerPharmaciesComponent } from './components/viewers/viewer-pharmacies/viewer-pharmacies.component';
import { ViewerShoppingComponent } from './components/viewers/viewer-shopping/viewer-shopping.component';
import { ViewerStockRequestsComponent } from './components/viewers/viewer-stock-requests/viewer-stock-requests.component';
import { AdminGuard } from './guards/admin-guard';
import { EmployeeGuard } from './guards/employee-guard';
import { OwnerGuard } from './guards/owner-guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'invitations', component: ViewerInvitationsComponent, canActivate: [AdminGuard]},
  {path: 'medicines', component: ViewerMedicinesComponent},
  {path: 'stockRequests', component: ViewerStockRequestsComponent, canActivate: [OwnerGuard]},
  {path: 'shopping', component: ViewerShoppingComponent, canActivate: [EmployeeGuard]},
  {path: 'shopping-state', component: ShoppingStateComponent},
  {path: 'pharmacies', component: ViewerPharmaciesComponent},
  {path: 'register', component: RegisterUserComponent},
  {path: 'create-invitation', component: AddInvitationComponent, canActivate: [AdminGuard]},
  {path: 'create-pharmacy', component: CreatePharmacyComponent, canActivate: [AdminGuard]},
  {path: 'create-medicine', component: CreateMedicineComponent, canActivate: [EmployeeGuard]},
  {path: 'delete-medicine', component: DeleteMedicineComponent, canActivate: [EmployeeGuard]},
  {path: 'create-stockRequest', component: AddStockrequestComponent, canActivate: [EmployeeGuard]},
  {path: 'export', component: CreateExportComponent, canActivate: [EmployeeGuard]},
  
  
  {path: '**', component: NotFoundComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})

export class AppRoutingModule { }
