import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-failure',
  templateUrl: './order-failure.component.html',
  styleUrls: ['./order-failure.component.css']
})
export class OrderFailureComponent implements OnInit {

  _orderFailure: any;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      console.log(params);
      this._orderFailure = params;
    });
  }

}
