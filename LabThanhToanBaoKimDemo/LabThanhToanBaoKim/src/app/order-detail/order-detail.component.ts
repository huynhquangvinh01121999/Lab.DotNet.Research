import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit {
  isLoading: boolean = false;

  status = ''
  oid = '';
  checksum = '';
  url_thanhtoan = '';
  jwt: any;
  BASE_URL = 'https://dev-api.baokim.vn/payment/api/v5/order';
  MERCHANT_ID = 40002;

  modelOrder = {
    mrc_order_id: '228',
    customer_name: 'NGUYỄN VĂN A',
    customer_email: 'nguyenvana@gmail.com',
    customer_phone: '09099909999',
    description: 'Đóng tiền phí đào tạo tiếng nhật khóa N4',
    total_amount: '14000000',
    merchant_id: this.MERCHANT_ID,
    url_detail: '',
    url_success: '',
    webhooks: ''
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

  fnCheckComplete(params: any) {
    let url = `https://devtest.baokim.vn:9243/payment-v2/check-order-complete-v2?order_id=${params.order_id}&checksum=${params.checksum}`
    return this.http.get(url).toPromise();
  }

  fnGetDetail(params: any) {
    let url = `${this.BASE_URL}/detail?jwt=${params.jwt}&id=${params.id}&mrc_order_id=${params.mrc_order_id}`
    return this.http.get(url).toPromise();
  }

  async ThanhToan() {
    this.isLoading = true;

    var result: any = await this.fnGetToken();
    this.jwt = result.jwt;

    var today = new Date();
    var datetime = today.getFullYear() + '' + (today.getMonth() + 1) + '' + today.getDate() + '' + today.getHours() + '' + today.getMinutes() + '' + today.getSeconds();

    this.modelOrder.mrc_order_id = `${this.modelOrder.mrc_order_id}_${datetime}`;
    console.log(this.modelOrder);

    this.fnCreateOrder(this.modelOrder).subscribe((resp: any) => {
      console.log(resp.data);
      this.url_thanhtoan = resp.data.payment_url;
      this.oid = resp.data.payment_url.split('?')[1].split('&')[0].split('=')[1]
      this.checksum = resp.data.payment_url.split('?')[1].split('&')[1].split('=')[1]
      console.log('oid', this.oid);
      console.log('checksum', this.checksum);

      this.isLoading = false;

      // setInterval(async () => {
      //   var result: any = await this.fnGetToken();

      //   var params = {
      //     jwt: result.jwt,
      //     id: this.oid,
      //     mrc_order_id: this.modelOrder.mrc_order_id
      //   }
      //   var result: any = await this.fnGetDetail(params);
      //   this.status = result.data.stat;
      //   console.log(result);
      // }, 10000);
    })
  }

  async ViewDetail() {
    var result: any = await this.fnGetToken();

    var params = {
      jwt: result.jwt,
      id: this.oid,
      mrc_order_id: this.modelOrder.mrc_order_id
    }
    var result: any = await this.fnGetDetail(params);
    this.status = result.data.stat;
    console.log(result);
  }
}
