namespace BasicPayment.Models
{
    public class OrderDetailResponse
    {
        public int code { get; set; }
        public string[] message { get; set; }
        public int count { get; set; }
        public OrderDetail data { get; set; }
        public class OrderDetail
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public string mrc_order_id { get; set; }
            public int txn_id { get; set; }
            public string ref_no { get; set; }
            public int merchant_id { get; set; }
            public string total_amount { get; set; }
            public string description { get; set; }
            public string url_success { get; set; }
            public string url_cancel { get; set; }
            public string url_detail { get; set; }
            public string stat { get; set; }    // trạng thái đơn hàng
            public string lang { get; set; }
            public int bpm_id { get; set; }
            public int type { get; set; }
            public int accept_qrpay { get; set; }
            public int accept_bank { get; set; }
            public int accept_cc { get; set; }
            public int accept_ib { get; set; }
            public int accept_ewallet { get; set; }
            public int accept_installments { get; set; }
            public string email { get; set; }
            public string name { get; set; }
            public string webhooks { get; set; }
            public string customer_name { get; set; }
            public string customer_email { get; set; }
            public string customer_phone { get; set; }
            public string customer_address { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
        }
    }
}