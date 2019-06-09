using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSiteBanHang.Services;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Controllers
{

    public class MobileApiController : ApiController
    {
        private readonly MobileService mobileService = new MobileService();

        // GET: api/MobileApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MobileApi/5
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost]
        [Route("api/MobileApi/LoginMobile")]
        // POST: api/MobileApi/LoginMobile
        public IHttpActionResult LoginMobile([FromBody] CustomerLogin model)
        {
            var response = new ResponseDataModel()
            {
                IsSuccess = false,
            };

            if (model == null || !ModelState.IsValid)
            {
                response.Error = "Tài khoản hoặc mật khẩu không được để trống";
            }

            var customerInfo = mobileService.Login(model.EmailorPhone, model.Password);

            if (customerInfo == null)
            {
                response.Error = "Tài khoản hoặc mật khẩu không chính xác";
            }
            else // login succes
            {
                response.CustomerInfo = customerInfo;
                response.IsSuccess = true;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("api/MobileApi/GetOrderStatus")]
        //GET: api/MobileApi/GetOrderStatus?status=3&idCustomer=2
        public IHttpActionResult GetOrderStatuss(int? status, int? idCustomer)
        {
            if (status == null || idCustomer == null)
            {
                string error = "Dữ liệu không được bỏ trống";
                return BadRequest(error);
            }

            var orders = mobileService.GetOrderByStatus(status, idCustomer);

            return Ok(orders);
        }


        // PUT: api/MobileApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MobileApi/5
        public void Delete(int id)
        {
        }
    }
}
