using BasicPayment.Common;
using BasicPayment.Models;
using BasicPayment.Services;
using BasicPayment.Services.Models;
using BasicPayment.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace BasicPayment.Controllers
{
    public class OrderController : Controller
    {
        private const string _baseDomain = "https://localhost:44344";

        /// <summary>
        /// The default page init
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(new SendOrderViewModel ());
        }

        /// <summary>
        /// This api call server baokim to redirect payment page
        /// </summary>
        /// <param name="request">Baokim params requied. See more: https://developer.baokim.vn/payment/#send-order </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Send(OrderSendRequest request)
        {
            request.merchant_id = BaoKimApi.MERCHANT_ID;

            // Callback success
            request.url_success = _baseDomain + "/Order/Success";

            // Callback failure
            request.url_detail = _baseDomain + "/Order/Failure";

            // Webhook callback
            request.webhooks = _baseDomain + "/Order/WebhookNotification";

            StringContent dataPost = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var url = string.Format("https://dev-api.baokim.vn/payment/api/v5/order/send?jwt={0}", BaoKimApi.JWT);

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsync(url, dataPost).Result;
                    string result = response.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(result))
                    {
                        OrderSendResponse res = JsonConvert.DeserializeObject<OrderSendResponse>(result);

                        if (res.code == 0 && res.Data?.payment_url != null)
                        {
                            var vm = new SendOrderViewModel { payment_url = res.Data.payment_url };
                            //return Redirect(res.Data.payment_url);
                            return View("Index", vm);
                        }
                    }

                    ViewBag.Error = result;
                    return View("Error");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
        }

        /// <summary>
        /// This api is callback by baokim for notify order status
        /// </summary>
        /// <param name="response">The response baokim return.  See more: https://developer.baokim.vn/payment/#webhook-notification </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WebhookNotification(OrderWebhookResponse response)
        {
            var webhookData = new
            {
                order = response.order,
                txn = response.txn,
                dataToken = response.dataToken
            };

            var data = JsonConvert.SerializeObject(webhookData);

            // Create sign form webhookData using Sha256
            string clientSign = BaoKimApi.HmacSha256Encode(data);

            // Check server sign is equal with client sign
            if (response.sign == clientSign)
            {
                // Save data respose to the folder WebhookNotification
                string fileName = string.Format("{0}\\WebhookNotification\\{1}.txt", System.Web.HttpRuntime.AppDomainAppPath, response.order.mrc_order_id);
                System.IO.File.WriteAllText(fileName, JsonConvert.SerializeObject(response));
            }
            return View("Success");
        }

        /// <summary>
        /// This page is callback by baokim when order payment is sucess
        /// </summary>
        /// <param name="response">The response baokim return. See more https://developer.baokim.vn/payment/#d-liu-tr-v-trn-url_success </param>
        /// <returns></returns>
        public ActionResult Success(OrderSuccessResponse response)
        {
            var request = new GetOrderDetailRequest { id = response.id, mrc_order_id = response.mrc_order_id };

            string result = BaoKimApiService.GetOrderDetail(request);

            if (!string.IsNullOrEmpty(result))
            {
                var orderDetail = JsonConvert.DeserializeObject<OrderDetailResponse>(result);
                ViewBag.Response = JsonConvert.SerializeObject(result);
                return View(orderDetail);
            }

            ViewBag.Error = result;
            return View("Error");

        }

        /// <summary>
        /// This page is callback by baokim when order payment is failure
        /// </summary>
        /// <param name="response">The response baokim return.</param>
        /// <returns></returns>
        public ActionResult Failure(OrderFailureResponse response)
        {
            var request = new GetOrderDetailRequest { id = response.id, mrc_order_id = response.mrc_order_id };
            string result = BaoKimApiService.GetOrderDetail(request);

            if (!string.IsNullOrEmpty(result))
            {
                var orderDetail = JsonConvert.DeserializeObject<OrderDetailResponse>(result);

                ViewBag.Title = "Đơn hàng thanh toán thất bại";
                ViewBag.Response = JsonConvert.SerializeObject(result);
                return View(orderDetail);
            }

            ViewBag.Error = result;
            return View("Error");
        }

        // tạo token
        [HttpPost]
        public ActionResult GenerateToken()
        {
            return Json(BaoKimApi.JWT);
        }

        // khởi tạo đơn hàng
        [HttpPost]
        public ActionResult GenerateOrder(OrderSendRequest request)
        {
            request.merchant_id = BaoKimApi.MERCHANT_ID;
            request.url_success = _baseDomain + "/Order/Success";
            request.url_detail = _baseDomain + "/Order/Failure";
            request.webhooks = _baseDomain + "/Order/WebhookNotification";

            StringContent dataPost = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var url = string.Format("https://dev-api.baokim.vn/payment/api/v5/order/send?jwt={0}", BaoKimApi.JWT);

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.PostAsync(url, dataPost).Result;
                    string result = response.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrEmpty(result))
                        return Json(new { data = JsonConvert.DeserializeObject<OrderSendResponse>(result) });

                    return Json(null);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}