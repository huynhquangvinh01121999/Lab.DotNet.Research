using BasicPayment.Common;
using BasicPayment.Services.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace BasicPayment.Services
{
    public static class BaoKimApiService
    {
        public static string GetOrderDetail(GetOrderDetailRequest request)
        {
            StringContent dataPost = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var url = string.Format("https://dev-api.baokim.vn/payment/api/v5/order/detail?jwt={0}&id={1}&mrc_order_id={2}", BaoKimApi.JWT, request.id, request.mrc_order_id);

            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(url).Result;
                    string data = result.Content.ReadAsStringAsync().Result;

                    return data;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}