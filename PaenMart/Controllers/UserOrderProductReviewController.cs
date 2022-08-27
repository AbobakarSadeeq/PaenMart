using Business_Core.Entities.OrderProductReviews;
using Business_Core.Entities.OrderProductReviews.OrderProductReviewsPhoto;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presentation.AppSettingClasses;
using Presentation.ViewModel;
using Presentation.ViewModel.OrderProductReview;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrderProductReviewController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public UserOrderProductReviewController(DataContext dataContext,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _dataContext = dataContext;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
             _cloudinaryConfig.Value.CloudName,
             _cloudinaryConfig.Value.ApiKey,
             _cloudinaryConfig.Value.ApiSecret
             );
            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPendingReviewByUser(string userId)
        {
            var getUserPendingOrderReviews = await _dataContext.OrderProductReviews
                .Include(a=>a.Product)
                .ThenInclude(a=>a.ProductImages)
                .Where(x => x.UserId == userId && x.ReviewStatus == "Pending")
                .ToListAsync();


            var convertToViewModel = new List<OrderProductReviewViewModel>();

            foreach (var item in getUserPendingOrderReviews)
            {
                convertToViewModel.Add(new OrderProductReviewViewModel
                {
                    ProductId = item.ProductId,
                    ProductColor = item.Product.Color,
                    ProductSingleImageUrl = item.Product.ProductImages[0].URL,
                    ProductName = item.Product.ProductName,
                    OrderProductReviewId = item.OrderProductReviewID
                });
            }
            return Ok(convertToViewModel);
        }

        [HttpGet("GetSingleProduct/{SingleProductId}")]
        public async Task<IActionResult> GetSingleProduct(int SingleProductId)
        {
            var getProductById = await _dataContext.Products.Include(a=>a.ProductImages)
                .FirstOrDefaultAsync(a => a.ProductID == SingleProductId);

            return Ok(new
            {
                ProductId = getProductById.ProductID,
                ProductName = getProductById.ProductName,
                ProductColor = getProductById.Color,
                ProductImage = getProductById.ProductImages[0].URL
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddUserProductReview([FromForm] AddUserOrderProductReview addReviewViewModel)
        {
            var uploadResult = new ImageUploadResult();
            var addingOrderReviewImages = new List<OrderProductReviewsPhoto>();
            if (addReviewViewModel.File.Count > 0)
            {
                foreach (var singleImg in addReviewViewModel.File)
                {
                    using (var stream = singleImg.OpenReadStream())
                    {
                        var uploadparams = new ImageUploadParams
                        {
                            File = new FileDescription(singleImg.Name, stream)
                        };
                        uploadResult = _cloudinary.Upload(uploadparams);
                    }
                    addingOrderReviewImages.Add(new OrderProductReviewsPhoto
                    {
                        PublicId = uploadResult.PublicId,
                        URL = uploadResult.Url.ToString(),
                        OrderProductReviewId = addReviewViewModel.OrderProductReviewId,
                    });
                }
            }

            var findingSelectedProductReviewId = await _dataContext.OrderProductReviews
                .FirstOrDefaultAsync(a => a.OrderProductReviewID == addReviewViewModel.OrderProductReviewId);
            findingSelectedProductReviewId.Add_Review_Date = DateTime.Now;
            findingSelectedProductReviewId.RaitingStars = addReviewViewModel.RatingStars;
            findingSelectedProductReviewId.ProductComment = addReviewViewModel.ProductComment;
            findingSelectedProductReviewId.ReviewStatus = addReviewViewModel.ReviewStatus;

           await _dataContext.OrderProductReviewsPhotos.AddRangeAsync(addingOrderReviewImages);
            _dataContext.OrderProductReviews.Update(findingSelectedProductReviewId);

          var findingProduct =  await _dataContext.Products
                .FirstOrDefaultAsync(a => a.ProductID == addReviewViewModel.ProductId);
            findingProduct.ProductTotalStars = findingProduct.ProductTotalStars + addReviewViewModel.RatingStars;
            findingProduct.Raitings = findingProduct.Raitings + 1;

            await _dataContext.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("GetReviewedReviewByUser/{userId}")]
        public async Task<IActionResult> GetReviewedReviewByUser(string userId)
        {
            var getUserPendingOrderReviews = await _dataContext.OrderProductReviews
                .Include(a => a.Photos)
                .Include(a => a.Product)
                .ThenInclude(a => a.ProductImages)
                .Where(x => x.UserId == userId && x.ReviewStatus == "Reviewed")
                .ToListAsync();

            var convertToViewModel = new List<OrderProductReviewViewModel>();
            foreach (var item in getUserPendingOrderReviews)
            {
                convertToViewModel.Add(new OrderProductReviewViewModel
                {
                    ProductId = item.ProductId,
                    ProductColor = item.Product.Color,
                    ProductSingleImageUrl = item.Product.ProductImages[0].URL,
                    ProductName = item.Product.ProductName,
                    OrderProductReviewId = item.OrderProductReviewID,
                    ProductComment = item.ProductComment,
                    RatingStars = item.RaitingStars,
                    ProductReviewedPhoto = item.Photos

                });
            }
            return Ok(convertToViewModel);
        }


        [HttpPost("GetAllUserReviewsOfSingleProduct")]
        public async Task<IActionResult> GetAllUserReviewsOfSingleProduct(GetSingleProductReviewByProduct productReviews)
        {
            var findingReviewsByItsProductId = new List<OrderProductReview>();

            var countList = await _dataContext.OrderProductReviews
            .Where(a => a.ProductId == productReviews.ProductId && a.ReviewStatus == "Reviewed")
            .CountAsync();

            if (productReviews.PageNo == 1)
            {
                findingReviewsByItsProductId = await _dataContext.OrderProductReviews
                .Include(a => a.CustomIdentity)
                .Include(a => a.Photos)
                .Where(a => a.ProductId == productReviews.ProductId && a.ReviewStatus == "Reviewed")
                .Take(7)
                .ToListAsync();
            }else
            {
                int skipPageSize = (productReviews.PageNo - 1) * 7;
                findingReviewsByItsProductId = await _dataContext.OrderProductReviews
                .Include(a => a.CustomIdentity)
                .Include(a => a.Photos)
                .Where(a => a.ProductId == productReviews.ProductId && a.ReviewStatus == "Reviewed")
                .Skip(skipPageSize)
                .Take(7)
                .ToListAsync();
            }

            var convertDataToViewModel = new List<GetSingleProductReviews>();

            foreach (var listData in findingReviewsByItsProductId)
            {
                convertDataToViewModel.Add(new GetSingleProductReviews
                {
                    RatingStars = listData.RaitingStars,
                    UserFullName = listData.CustomIdentity.FullName,
                    ProductId = listData.ProductId,
                    ProductComment = listData.ProductComment,
                    ProductReviewedPhoto =  listData.Photos.Select(a=> new GetSingleProductReviewPhotos
                    {
                        OrderProductReviewsPhotoID = a.OrderProductReviewsPhotoID,
                        URL = a.URL,
                    }).ToList()
                });
            }
            return Ok(new
            {
                dataCount = countList,
                reviewList = convertDataToViewModel
            });
        }



    }
}
