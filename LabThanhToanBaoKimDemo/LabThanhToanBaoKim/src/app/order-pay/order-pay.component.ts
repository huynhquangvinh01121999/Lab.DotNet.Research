import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-order-pay',
  templateUrl: './order-pay.component.html',
  styleUrls: ['./order-pay.component.css']
})
export class OrderPayComponent implements OnInit {

  isLoading = false;
  jwt: any;
  BASE_URL = 'https://dev-api.baokim.vn/payment/api/v5/order';
  URL_DETAIL = 'http://localhost:4002/order-failure';
  URL_SUCCESS = 'http://localhost:4002/order-success';
  URL_WEBHOOKS = 'https://localhost:4002/Order/WebhookNotification';
  MERCHANT_ID = 40002;

  modelOrder = {
    mrc_order_id: '228',
    customer_name: 'NGUYỄN VĂN A',
    customer_email: 'nguyenvana@gmail.com',
    customer_phone: '09099909999',
    description: 'Đóng tiền phí đào tạo tiếng nhật khóa N4',
    total_amount: '14000000',
    merchant_id: this.MERCHANT_ID,
    url_detail: this.URL_DETAIL,
    url_success: this.URL_SUCCESS,
    webhooks: this.URL_WEBHOOKS
  };

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  fnGetToken() {
    return this.http.get('https://localhost:6001/PayOrder/GetToken').toPromise();
  }

  fnCreateOrder(model: any): Observable<any> {
    let url = `${this.BASE_URL}/send?jwt=${this.jwt}`;
    return this.http.post(url, model);
  }

  async fnThanhToan() {
    this.isLoading = true;

    var result: any = await this.fnGetToken();
    this.jwt = result.jwt;

    var today = new Date();
    var datetime = today.getFullYear() + '' + (today.getMonth() + 1) + '' + today.getDate() + '' + today.getHours() + '' + today.getMinutes() + '' + today.getSeconds();

    this.modelOrder.mrc_order_id = `${this.modelOrder.mrc_order_id}_${datetime}`;
    console.log(this.modelOrder);

    this.fnCreateOrder(this.modelOrder).subscribe((resp: any) => {
      console.log(resp.data);
      window.open(resp.data.payment_url);
      this.isLoading = false;
    });
  }
}
