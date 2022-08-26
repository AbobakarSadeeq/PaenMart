using AutoMapper;
using Business_Core.Entities.Identity.AdminAccount;
using Business_Core.Entities.Identity.Email;
using Business_Core.Entities.Order;
using Business_Core.Entities.Order.OrderDetail;
using Business_Core.Entities.OrderProductReviews;
using Business_Core.IServices;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModel.OrderViewModel;
using System.Diagnostics;
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

        // ------------------------- Admin section -------------------------




        // canceling the user order
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            var findingOrderId = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .Include(a => a.OrderDetails)
                .FirstOrDefaultAsync(a => a.OrderID == Id);

            findingOrderId.OrderStatus = "Canceled";
            await _dataContext.SaveChangesAsync();
            var getEmailSendEmailData = await _dataContext.SendingEmails.FirstOrDefaultAsync();

            string adminName = "";
            for (var a = 0; a < getEmailSendEmailData.OwnerEmail.Length; a++)
            {
                if (getEmailSendEmailData.OwnerEmail[a].ToString() != "@")
                {
                    adminName = adminName + getEmailSendEmailData.OwnerEmail[a].ToString();
                }
                else
                {
                    adminName[0].ToString().ToUpper();
                    break;
                }
            }

            MailMessage msgObj = new MailMessage(getEmailSendEmailData.OwnerEmail, findingOrderId.CustomIdentity.Email);
            msgObj.Subject = "Paen mart order";
            msgObj.IsBodyHtml = true;
            msgObj.Body = @$"<h1>Dear {findingOrderId.CustomIdentity.FullName},</h1>
            <p>We would like to cancel your order, purchase order number is <strong>{findingOrderId.OrderID}</strong> dated <strong>{findingOrderId.OrderDate}</strong> and number of product order <strong>{findingOrderId.OrderDetails.Count}</strong> We apologize for it.</p>
            <p>Our warehouse manager, who was on leave for a long time, has joined back this week. He has informed us that we have sufficient stock of the ordered goods in our warehouse,
            which would last for next months. Therefore, we have to cancel the order immediately and also the given product in short as well.</p>
            <p>I hope this does not cause too much of an inconvenience to you.</p>
            <p>Thank you for your understanding in the matter!</p>
            <p><strong>Regards,</strong></p>
            <p>{adminName}</p>
            <p><strong>Paen mart</strong></p>";


            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword};
            client.Send(msgObj);

            return Ok();
        }


        // accepting the user order by employee, admin

        [HttpPut("AcceptOrder")]
        public async Task<IActionResult> AcceptOrder(ConfirmOrderViewModel viewModel)
        {
            // Finding the Order
            var findOrder = await _dataContext.Orders
                .FirstOrDefaultAsync(a => a.OrderID == viewModel.OrderId);
            findOrder.OrderStatus = "Shipping pending";
            await _dataContext.SaveChangesAsync();

            var getEmailSendEmailData = await _dataContext.SendingEmails.FirstOrDefaultAsync();

            string multipleProductDataConcatinating = "";
            int noProduct = 1;
            int totalPrice = 0;



            foreach (var data in viewModel.OrderDetail)
            {

                var updateProductData = await _dataContext.Products
                    .FirstOrDefaultAsync(a=>a.ProductID== data.ProductId);

                if(updateProductData.Quantity < data.Quantity)
                {
                    // send email as well here to user and skip this product
                    string adminName = "";
                    for (var a = 0; a < getEmailSendEmailData.OwnerEmail.Length; a++)
                    {
                        if (getEmailSendEmailData.OwnerEmail[a].ToString() != "@")
                        {
                            adminName = adminName + getEmailSendEmailData.OwnerEmail[a].ToString();
                        }
                        else
                        {
                            break;
                        }
                    }

                    MailMessage cancelMsgObj = new MailMessage(getEmailSendEmailData.OwnerEmail, viewModel.Email);
                    cancelMsgObj.Subject = "Paen mart order";
                    cancelMsgObj.IsBodyHtml = true;
                    cancelMsgObj.Body = @$"<h1>Dear {viewModel.FullName},</h1>
                    <p>We would like to cancel your order product name <strong>{updateProductData.ProductName}</strong>, purchase order number is <strong>{viewModel.OrderId}</strong> dated <strong>{viewModel.OrderDate}</strong> We apologize for it.</p>
                    <p>Our warehouse manager, who was on leave for a long time, has joined back this week. He has informed us that we have sufficient stock of the ordered goods in our warehouse,
                    which would last for next months. Therefore, we have to cancel the order immediately and also the given product in short as well.</p>
                    <p>I hope this does not cause too much of an inconvenience to you.</p>
                    <p>Thank you for your understanding in the matter!</p>
                    <p><strong>Regards,</strong></p>
                    <p>{adminName}</p>
                    <p><strong>Paen mart</strong></p>";

                    SmtpClient clientt = new SmtpClient("smtp.gmail.com", 587);
                    clientt.EnableSsl = true;
                    clientt.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clientt.UseDefaultCredentials = false;
                    clientt.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword };
                    clientt.Send(cancelMsgObj);

                    if (updateProductData.Quantity == 0)
                    {
                        updateProductData.StockAvailiability = false;
                    }
                    await _dataContext.SaveChangesAsync();

                    if(viewModel.OrderDetail.Count == 1)
                    {
                        return Ok();
                    }
                    continue;
                }


                updateProductData.Quantity = updateProductData.Quantity - data.Quantity;
                updateProductData.SellUnits = updateProductData.SellUnits + data.Quantity;

                if (updateProductData.Quantity == 0)
                {
                    updateProductData.StockAvailiability = false;
                }
               await _dataContext.SaveChangesAsync();



                // Sending Email for Accepting Order
                totalPrice = totalPrice + (data.Quantity * data.Price);
                var newData = @$"<h3 style='text-decoration: underline;'>{viewModel.FullName},</h3><h3 style='text-decoration: underline;'>Product No: {noProduct},</h3><p>Your Order Has been Succeussfully Done!</p>  
                                 <p>Your Address is {viewModel.CompleteAddress},</p><p>Your Email Is: {viewModel.Email}</p>
                                  <p>Your Product Name is: {data.ProductName} and Quantity is {data.Quantity}</p>
                                  <p>Your Mobile Number Is: {viewModel.PhoneNumber}</p><p>Your Order Date Was: {viewModel.OrderDate}</p><hr>
                                   <p><strong>Your Total Amount of this Products Rs{data.Price * data.Quantity}</strong></p>
                                   <h3>Thank You For Order and your Order Has been Succeussfully done and delivering tommarow!</h3><p>Regards From <strong>Paen Mart Shop</strong></p><hr><br>";
                multipleProductDataConcatinating = multipleProductDataConcatinating + newData;
                if (noProduct == viewModel.OrderDetail.Count)
                {
                    string extraMoreEmailInfo = @$"<br><br><hr>
                    <h3 style = 'text-align: right; padding-right: 270px;' > TOTAL BILL = {totalPrice} </h3>";
                    multipleProductDataConcatinating = multipleProductDataConcatinating + extraMoreEmailInfo;
                }
                noProduct++;


                // Sending Email to the Admin if Quantity is less or equal to 5
                if (updateProductData.Quantity <= 5)
                {

                    MailMessage emailMessage = new MailMessage(getEmailSendEmailData.OwnerEmail, getEmailSendEmailData.OwnerEmail);
                    emailMessage.Subject = "Paen mart";
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = @$"
                        <h2 class='text-warning'>WARNING!</h2>
                        <h1>This product is less then 5 quantities remaining!</h1>
                        <p>Product Name is <strong> {data.ProductName} </strong> and Quantity is <strong> {data.Quantity} </strong> </p><hr>";

                    SmtpClient clientData = new SmtpClient("smtp.gmail.com", 587);
                    clientData.EnableSsl = true;
                    clientData.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clientData.UseDefaultCredentials = false;
                    clientData.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword };
                    clientData.Send(emailMessage);
                }

            }

            // Sending Email to the User that your order has been accepted

            var msgObj = new MailMessage(getEmailSendEmailData.OwnerEmail, viewModel.Email);
            msgObj.Subject = "Paen mart";
            msgObj.IsBodyHtml = true;
            msgObj.Body = multipleProductDataConcatinating;



            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword };
            client.Send(msgObj);

            return Ok();
        }

        // getting the orders all for admin

        [HttpGet("GetPendingOrderList/{pageNo}")]
        public async Task<IActionResult> GetPendingOrderList(int pageNo)
        {
            var gettingOrdersData = new List<Order>();
            var countList = await _dataContext.Orders
                    .Where(a => a.OrderStatus == "Pending")
                    .CountAsync();
            if (pageNo == 1)
            {
                 
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a=>a.OrderStatus == "Pending")
                .Take(12).ToListAsync();
            }else
            {
                int skipPageSize = (pageNo - 1) * 12;
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.OrderStatus == "Pending")
                .Skip(skipPageSize).Take(12).ToListAsync();
            }

            var convertDataToViewModel = new List<OrderListViewModel>();

            foreach (var listData in gettingOrdersData)
            {
                convertDataToViewModel.Add(new OrderListViewModel
                {
                    OrderId = listData.OrderID,
                    FullName = listData.CustomIdentity.FullName,
                    OrderStatus = listData.OrderStatus,
                    OrderDate = listData.OrderDate.ToString(),
                    CountryName = listData.CustomIdentity.Address.City.Country.CountryName,
                    OrderItemsCount = listData.OrderDetails.Count,
                    PaymentMethod = listData.PaymentMethod,
                });
            }
            return Ok(new
            {
                dataCount = countList,
                orderList = convertDataToViewModel
            });

        }

        [HttpGet("GetShippedOrderList/{pageNo}")]
        public async Task<IActionResult> GetShippedOrderList(int pageNo)
        {
            var gettingOrdersData = new List<Order>();
            var countList = await _dataContext.Orders
              .Where(a => a.OrderStatus == "Shipped")
              .CountAsync();
            if (pageNo == 1)
            {
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.OrderStatus == "Shipped")
                .Take(12).ToListAsync();
            }
            else
            {
                int skipPageSize = (pageNo - 1) * 12;
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.OrderStatus == "Shipped")
                .Skip(skipPageSize).Take(12).ToListAsync();
            }

            var convertDataToViewModel = new List<OrderListViewModel>();

            foreach (var listData in gettingOrdersData)
            {
                convertDataToViewModel.Add(new OrderListViewModel
                {
                    OrderId = listData.OrderID,
                    FullName = listData.CustomIdentity.FullName,
                    OrderStatus = listData.OrderStatus,
                    OrderDate = listData.OrderDate.ToString(),
                    CountryName = listData.CustomIdentity.Address.City.Country.CountryName,
                    OrderItemsCount = listData.OrderDetails.Count,
                    PaymentMethod = listData.PaymentMethod,
                    ShipperId = listData.ShipperId
                });
            }
            return Ok(new
            {
                dataCount = countList,
                orderList = convertDataToViewModel
            });

        }

        [HttpGet("GetCancelOrderList/{pageNo}")]
        public async Task<IActionResult> GetCancelOrderList(int pageNo)
        {
            var gettingOrdersData = new List<Order>();
            var countList = await _dataContext.Orders
            .Where(a => a.OrderStatus == "Canceled")
            .CountAsync();
            if (pageNo == 1)
            {
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.OrderStatus == "Canceled")
                .Take(12).ToListAsync();
            }
            else
            {
                int skipPageSize = (pageNo - 1) * 12;
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.OrderStatus == "Canceled")
                .Skip(skipPageSize).Take(12).ToListAsync();
            }

            var convertDataToViewModel = new List<OrderListViewModel>();

            foreach (var listData in gettingOrdersData)
            {
                convertDataToViewModel.Add(new OrderListViewModel
                {
                    OrderId = listData.OrderID,
                    FullName = listData.CustomIdentity.FullName,
                    OrderStatus = listData.OrderStatus,
                    OrderDate = listData.OrderDate.ToString(),
                    CountryName = listData.CustomIdentity.Address.City.Country.CountryName,
                    OrderItemsCount = listData.OrderDetails.Count,
                    PaymentMethod = listData.PaymentMethod
                });
            }
            return Ok(new
            {
                dataCount = countList,
                orderList = convertDataToViewModel
            });

        }

        [HttpGet("GetShippingPendingList/{pageNo}")]
        public async Task<IActionResult> GetShippingPendingList(int pageNo)
        {
            var gettingOrdersData = new List<Order>();
            var countList = await _dataContext.Orders
            .Where(a => a.OrderStatus == "Shipping pending")
            .CountAsync();
            if (pageNo == 1)
            {
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.OrderStatus == "Shipping pending")
                .Take(12).ToListAsync();
            }
            else
            {
                int skipPageSize = (pageNo - 1) * 12;
                gettingOrdersData = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.OrderStatus == "Shipping pending")
                .Skip(skipPageSize).Take(12).ToListAsync();
            }

            var convertDataToViewModel = new List<OrderListViewModel>();

            foreach (var listData in gettingOrdersData)
            {
                convertDataToViewModel.Add(new OrderListViewModel
                {
                    OrderId = listData.OrderID,
                    FullName = listData.CustomIdentity.FullName,
                    OrderStatus = listData.OrderStatus,
                    OrderDate = listData.OrderDate.ToString(),
                    CountryName = listData.CustomIdentity.Address.City.Country.CountryName,
                    OrderItemsCount = listData.OrderDetails.Count,
                    PaymentMethod = listData.PaymentMethod
                });
            }
            return Ok(new
            {
                dataCount = countList,
                orderList = convertDataToViewModel
            });

        }

        // getting single order detail and its order-details
        [HttpGet("OrderDetails/{Id}")]
        public async Task<IActionResult> OrderDetails(int Id)
        {
            var gettingOrdersData = await _dataContext.Orders
              .Include(a => a.CustomIdentity)
              .ThenInclude(a => a.Address)
              .ThenInclude(a => a.City)
              .ThenInclude(a => a.Country)
              .Include(a => a.OrderDetails)
              .ThenInclude(a => a.Product)
              .ThenInclude(a=>a.ProductImages)
              .FirstOrDefaultAsync(a => a.OrderID == Id);

            if(gettingOrdersData == null)
            {
                return BadRequest("No data found");
            }

            var convertDataToViewModel = new
            {
            OrderId = gettingOrdersData.OrderID,
            FullName = gettingOrdersData.CustomIdentity.FullName,
            Email = gettingOrdersData.CustomIdentity.Email,
            CompleteAddress = gettingOrdersData.CustomIdentity.Address.CompleteAddress,
            CountryName = gettingOrdersData.CustomIdentity.Address.City.CityName,
            CityName = gettingOrdersData.CustomIdentity.Address.City.Country.CountryName,
            PhoneNumber = gettingOrdersData.CustomIdentity.Address.PhoneNumber,
            OrderStatus = gettingOrdersData.OrderStatus,
            OrderDate = gettingOrdersData.OrderDate.ToString(),
            ShippedDate = gettingOrdersData.ShippedDate.ToString(),
            OrderDetail = new List<GetOrderDetail>(),
            PaymentMethod = gettingOrdersData.PaymentMethod,
            ShipperId = gettingOrdersData.ShipperId
            };

            // Adding Its OrderDetails
            foreach (var item in gettingOrdersData.OrderDetails)
            {
                
                convertDataToViewModel.OrderDetail.Add(new GetOrderDetail
                {
                    ProductId = item.ProductId,
                    ProductImageUrl = item.Product.ProductImages[0].URL,
                    ProductName = item.Product.ProductName + " " + item.Product.Color,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    QuantityAvailability = item.Product.Quantity > item.Quantity ? true : false,
                });
            }
            return Ok(convertDataToViewModel);
        }





        // ------------------------- Shipper section -------------------------

        [HttpPost("ShippmentOrderDone")]

        public async Task<IActionResult> ShippmentOrderDone(ShippmentOrderDoneViewModel viewModel)
        {
            var getOrderData = await _dataContext.Orders
                .FirstOrDefaultAsync(a => a.OrderID == viewModel.OrderId);

            var getShipperIdFind = await _dataContext.Shippers.FirstOrDefaultAsync(a => a.UserId == viewModel.ShipperUserId);
            getOrderData.ShipperId = getShipperIdFind.ShipperID;
            getOrderData.OrderStatus = "Shipped";
            getOrderData.ShippedDate = DateTime.Now;


            // Add the Account Balance Or Modifing balance
            var getAccountData = await _dataContext.AdminAccounts
                .OrderByDescending(a => a.AdminAccountID)
                .FirstOrDefaultAsync();

            var convertAccountData = new AdminAccount();
            convertAccountData.BeforeBalance = getAccountData.CurrentBalance;
            convertAccountData.CurrentBalance = getAccountData.CurrentBalance + viewModel.OrderTotalPrice;
            convertAccountData.UserId = getAccountData.UserId;
            convertAccountData.TransactionDateTime = DateTime.Now;
            convertAccountData.TransactionPurpose = "Order Payment";
            convertAccountData.BalanceSituation = "Add";
            await _dataContext.AdminAccounts.AddAsync(convertAccountData);


            // Sending email to the user that whose order is and tell to give us the reviews about the all products that you have purchased
            StringBuilder orderDetailsProduct = new StringBuilder();
            var url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("Client_URL");

            orderDetailsProduct.Append($@"

                <div>
                  <h1 style='color: #0f146d;'
                >Your package has been delivered!</h1>
                  <h3>Hi {viewModel.FullName},</h3>
                  <h3>We are pleased to inform that your order #{getOrderData.OrderID} has been delivered.</h3>
                  <h3>We hope you are enjoying your recent purchase! Once you have a chance, we would love to hear your shopping
                    experience
                    to keep us constantly improving.</h3>
                  <a href='{url + "/Client/Reviews"}' style='    text-decoration: none;
                background: #ff6b00;
                color: #ffffff;
                font-weight: bold;
                font-size: 16px;
                width: 2080px;
                padding: 12px;'>
                    WRITE A REVIEW</a>
                </div>
                <br>
                <hr>
                <div style='font-size:1rem'>

                  <span style='font-weight:bold; color: #0f146d;'>Name: </span>
                  <span>{viewModel.FullName}</span><br>
                  <span style='font-weight:bold; color: #0f146d;'>Address: </span>
                  <span>{viewModel.CompleteAddress}</span><br>
                  <span style='font-weight:bold; color: #0f146d;'>Phone: </span>
                  <span>{viewModel.PhoneNumber}</span><br>
                  <span style='font-weight:bold; color: #0f146d;'>Email: </span>
                  <span>{viewModel.Email}</span>

                </div>

                <hr>

                <div>
                  <h1 style='color: #0f146d;'>Order Details</h1>

                  <p>Estimated delivery between {getOrderData.OrderDate.Value.ToString("D")} to {getOrderData.ShippedDate.Value.ToString("D")} </p>

                            <table style='border-collapse:collapse;'>
                            <thead>
                            <tr style ='   border-bottom: thick solid #03b1ca; '>   
                            <th style='padding-right: 200px;' >Product Name</th>
                            <th style='padding-right: 200px;' >Quantity</th>
                            <th style='padding-right: 200px;' > Price</th>
                            <th style='padding-right: 200px;' >TotalPriceWithQuantity</th>
                            </tr>
                            </thead>
                            <tbody>
                                ");

            int totalPrice = 0;
            var addingProductReviews = new List<OrderProductReview>();
            foreach (var OrderData in viewModel.OrderDetails)
            {

                orderDetailsProduct.Append($@"
                <tr style='   border-bottom: 1pt solid black; '> 
                <td style='padding-right: 200px;' > {OrderData.ProductName} </td>
                <td style='padding-right: 200px;' > {OrderData.Quantity} </td>
                <td style='padding-right: 200px;' >Rs {OrderData.Price}</td>
                <td style='padding-right: 200px;' >Rs {OrderData.Price * OrderData.Quantity}</td>
                </tr>
                ");

                totalPrice = totalPrice + (OrderData.Price * OrderData.Quantity);

                // Adding Reviews for pending

                addingProductReviews.Add(new OrderProductReview()
                {
                    ProductId = OrderData.ProductId,
                    UserId = getOrderData.CustomIdentityId,
                    ReviewStatus = "Pending"
                });

            }

            orderDetailsProduct.Append($@"
            </tbody>
            </table>
            <h2 style='text-align: right; padding-right: 270px;'>TOTAL = Rs {totalPrice} </h2>
            <hr>
            </div>");


            await _dataContext.OrderProductReviews.AddRangeAsync(addingProductReviews);
            await _dataContext.SaveChangesAsync();

            var getEmailSendEmailData = await _dataContext.SendingEmails.FirstOrDefaultAsync();
            MailMessage msgObj = new MailMessage(getEmailSendEmailData.OwnerEmail, viewModel.Email);
            msgObj.Subject = "Paen mart order";
            msgObj.IsBodyHtml = true;
            msgObj.Body = orderDetailsProduct.ToString();

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential() { UserName = getEmailSendEmailData.OwnerEmail, Password = getEmailSendEmailData.AppPassword };
            client.Send(msgObj);

            return Ok();
        }

        [HttpGet("ShipperDetail/{Id}")]
        public async Task<IActionResult> ShipperDetail(int Id)
        {
           var gettingShipperDetailById = await _dataContext.Shippers.FirstOrDefaultAsync(a=>a.ShipperID == Id);
            return Ok(new
            {
                ShipperId = gettingShipperDetailById.ShipperID,
                FullName = gettingShipperDetailById.FirstName + " " + gettingShipperDetailById.LastName,
                PhoneNumber = gettingShipperDetailById.PhoneNumber
            });
        }

        [HttpPost("ShipperShipmentsDone")]
        public async Task<IActionResult> ShipperShipmentsDone(GetShipperShipmentsDone viewModel)
        {
            var gettingShipperDetailByUserId = await _dataContext.Shippers
                .FirstOrDefaultAsync(a => a.UserId == viewModel.shipperUserId);

            var countList = await _dataContext.Orders
             .Where(a => a.ShipperId == gettingShipperDetailByUserId.ShipperID)
                .CountAsync();

            var findingShipperShipmentsDone = new List<Order>();

            if (viewModel.pageNo == 1)
            {

                findingShipperShipmentsDone = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.ShipperId == gettingShipperDetailByUserId.ShipperID)
                .Take(12).ToListAsync();
            }
            else
            {
                int skipPageSize = (viewModel.pageNo - 1) * 12;
                findingShipperShipmentsDone = await _dataContext.Orders
                .Include(a => a.CustomIdentity)
                .ThenInclude(a => a.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(a => a.Country)
                .Include(a => a.OrderDetails)
                .Where(a => a.ShipperId == gettingShipperDetailByUserId.ShipperID)
                .Skip(skipPageSize).Take(12).ToListAsync();
            }

            var convertDataToViewModel = new List<OrderListViewModel>();

            foreach (var listData in findingShipperShipmentsDone)
            {
                convertDataToViewModel.Add(new OrderListViewModel
                {
                    OrderId = listData.OrderID,
                    FullName = listData.CustomIdentity.FullName,
                    OrderStatus = listData.OrderStatus,
                    OrderDate = listData.OrderDate.ToString(),
                    CountryName = listData.CustomIdentity.Address.City.Country.CountryName,
                    OrderItemsCount = listData.OrderDetails.Count,
                    PaymentMethod = listData.PaymentMethod,
                    ShipperId = listData.ShipperId
                });
            }
            return Ok(new
            {
                dataCount = countList,
                orderList = convertDataToViewModel
            });
        }

     
       // ------------------------- user order section -------------------------

        // Getting sinle user all his order 
        [HttpGet("GetSingleUserOrders/{userId}")]
        public async Task<IActionResult> GetSingleUserOrders(string userId)
        {
            var getSingleUserOrders = await _dataContext.Orders
              .Include(a => a.CustomIdentity)
               .ThenInclude(a => a.Address)
               .ThenInclude(a => a.City)
               .ThenInclude(a => a.Country)
               .Include(a => a.OrderDetails)
              .Where(a => a.CustomIdentityId == userId)
              .ToListAsync();

            var convertDataToViewModel = new List<GetUserOrderViewModel>();

            foreach (var listData in getSingleUserOrders)
            {
                convertDataToViewModel.Add(new GetUserOrderViewModel
                {
                    OrderId = listData.OrderID,
                    UserName = listData.CustomIdentity.FullName,
                    OrderStatus = listData.OrderStatus,
                    OrderDate = listData.OrderDate.ToString(),
                    CountryName = listData.CustomIdentity.Address.City.Country.CountryName,
                    OrderItemsCount = listData.OrderDetails.Count
                });
            }

            return Ok(convertDataToViewModel);
        }

        // Delete order means to cancel the order there like if user didnt want to order then can cancel it and also
        [HttpDelete("DeleteOrderByUser/{Id}")]
        public async Task<IActionResult> DeleteOrderByUser(int Id)
        {
            var findingOrderById = await _dataContext.Orders.FirstOrDefaultAsync(a => a.OrderID == Id);
            findingOrderById.OrderStatus = "Canceled";
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        // Posting user orders
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddUserOrder(string userId, List<AddUserOrderViewModel> userOrders)
        {
            // checking quantity

            foreach (var order in userOrders)
            {
                var productFinding = await _dataContext.Products.Where(a => a.ProductID == order.ProductId).FirstOrDefaultAsync();
                if (productFinding.Quantity < order.Quantity)
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
