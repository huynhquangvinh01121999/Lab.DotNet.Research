import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { OrderFailureComponent } from './order-failure/order-failure.component';
import { OrderPayComponent } from './order-pay/order-pay.component';
import { OrderSuccessComponent } from './order-success/order-success.component';

const routes: Routes = [
  { path: '', component: OrderPayComponent },
  { path: "order-success", component: OrderSuccessComponent },
  { path: 'order-failure', component: OrderFailureComponent },
  { path: 'order-detail', component: OrderDetailComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
