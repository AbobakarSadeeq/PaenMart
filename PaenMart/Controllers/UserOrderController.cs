using AutoMapper;
using Business_Core.Entities.Order;
using Business_Core.Entities.Order.OrderDetail;
using Business_Core.IServices;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.OrderViewModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrderController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        private readonly IProductService _ProductService;


        public UserOrderController(DataContext dataContext, IMapper mapper, IProductService  ProductService)
        {
            _dataContext = dataContext;
            _ProductService = ProductService;
            this.mapper = mapper;
        }

        // Posting user orders
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddUserOrder(string userId, List<AddUserOrderViewModel> userOrders)
        {
            // checking quantity

            foreach (var order in userOrders)
            {
            var productFinding = await _dataContext.Products.Where(a=>a.ProductID == order.ProductId).FirstOrDefaultAsync();
            if(productFinding.Quantity < order.Quantity)
            {
                 return BadRequest("Sorry we don't have that much quantity of " + productFinding.ProductName + " you asking for");
            }
            }

            // First Add the Order Table Data and Save it
            var orderAcceptData = new Order
            {
                CustomIdentityId = userId,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                PaymentMethod = userOrders[0].PaymentMethod
            };

            await _dataContext.Orders.AddAsync(orderAcceptData);
            await _dataContext.SaveChangesAsync();

            // Second Add the its order Detail data

            var emailMsg = new StringBuilder($@"
            <div style='margin-left: auto; margin-right:auto; width:1350px;'>
            <h1><strong>Dear {userOrders[0].FullName},</strong></h1>
            <p>Your Order Has been placed Succeussfully Done!</p>
            <hr>
            <h1><strong>Order Detail</strong></h1>
            <hr>
            <div style='font-size: 20px; font-style: roboto;'>
            <p>Order Number: <strong>{orderAcceptData.OrderID}</strong></p>
            <p>Order by: <strong>{userOrders[0].FullName}</strong></p>
            <p>Email: <strong>{userOrders[0].UserEmail}</strong></p>
            <p>Phone Number: <strong> {userOrders[0].PhoneNumber} </strong></p>
            <p>Address: <strong> {userOrders[0].UserAddress} </strong></p>
            <p>Order Requested: <strong> {DateTime.Now.ToString("F")} </strong></p>
            </div>
            <br><br>
            <table style='border-collapse:collapse;'>
            <thead>
            <tr style ='   border-bottom: thick solid #03b1ca; '>   
            <th style='padding-right: 200px;' >Product Name</th>
            <th style='padding-right: 200px;' >Quantity</th>
            <th style='padding-right: 200px;' > Price</th>
            <th style='padding-right: 200px;' >TotalPriceWithQuantity</th>
            </tr>
            </thead>
            <tbody>");

            var orderDetailData = new List<OrderDetail>();
            int totalPrice = 0;
            foreach (var OrderData in userOrders)
            {
                orderDetailData.Add(new OrderDetail
                {
                    OrderId = orderAcceptData.OrderID,
                    ProductId = OrderData.ProductId,
                    Quantity = OrderData.Quantity,
                    Price = OrderData.Price
                });

                emailMsg.Append($@"
                <tr style='   border-bottom: 1pt solid black; '> 
                <td style='padding-right: 200px;' > {OrderData.ProductName} </td>
                <td style='padding-right: 200px;' > {OrderData.Quantity} </td>
                <td style='padding-right: 200px;' >Rs {OrderData.Price}</td>
                <td style='padding-right: 200px;' >Rs {OrderData.Price * OrderData.Quantity}</td>
                </tr>
                ");

                totalPrice = totalPrice + (OrderData.Price * OrderData.Quantity);

            }

            await _dataContext.OrderDetails.AddRangeAsync(orderDetailData);
            await _dataContext.SaveChangesAsync();

            emailMsg.Append($@"
            </tbody>
            </table>
            <h2 style='text-align: right; padding-right: 270px;'>TOTAL = Rs {totalPrice} </h2>
            <hr>
            </div>");

            var getEmailSendEmailData = await _dataContext.SendingEmails.FirstOrDefaultAsync();
            MailMessage msgObj = new MailMessage(getEmailSendEmailData.OwnerEmail, userOrders[0].UserEmail);
            msgObj.Subject = "Paen mart order";
            msgObj.IsBodyHtml = true;
            msgObj.Body = emailMsg.ToString();


            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword };
            client.Send(msgObj);



            return Ok();




        }

     
    }
}
