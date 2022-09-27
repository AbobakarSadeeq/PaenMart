using Business_Core.Entities.Identity;
using Business_Core.Entities.SponsoredAd;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presentation.AppSettingClasses;
using Presentation.ViewModel.SponsoredAdsViewModel;

namespace PaenMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsoreAdsController : ControllerBase
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly DataContext _dataContext;
        public SponsoreAdsController(IOptions<CloudinarySettings> cloudinaryConfig, DataContext dataContext)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _dataContext = dataContext;
            // Give the written keys that are in appsetting.json
            Account acc = new Account(
            _cloudinaryConfig.Value.CloudName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }


        [HttpPost]
        public async Task<IActionResult> AddSponsoreAds([FromForm] AddUpdateSponsoredAdsViewModel viewModel)
        {
            // finding is ads is greater then five or not
            var findingAdsCurrentLive = _dataContext.SponsorsAds.Where(a => a.AdStatus == "Live").Count();
            if (findingAdsCurrentLive + 1 > 5)
            {
                return BadRequest("Sorry you cannot live the ads more then 5");
            }
            // if lower then add to database

            var convertingViewModel = new SponsorsAds
            {
                AdPrice = viewModel.AdPrice,
                SponsoredByName = viewModel.SponsoredByName,
                ShowAdOnPage = viewModel.ShowAdOn,
                Created_At = DateTime.Now,
                AdStatus = "Live",
                AdUrlDestination = viewModel.AdUrlDestination,
                Expire_At = viewModel.Expire_At
            };

            // uploading image to cloudinary
            var uploadResult = new ImageUploadResult();
            using (var stream = viewModel.File.OpenReadStream())
            {
                var uploadparams = new ImageUploadParams
                {
                    File = new FileDescription(viewModel.File.Name, stream),
                };
                uploadResult = _cloudinary.Upload(uploadparams);
            }
            convertingViewModel.AdPictureUrl = uploadResult.Url.ToString();
            convertingViewModel.PublicId = uploadResult.PublicId;

            await _dataContext.SponsorsAds.AddAsync(convertingViewModel);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSponsoreAds([FromForm] AddUpdateSponsoredAdsViewModel viewModel)
        {
            var findingUpdateObj = await _dataContext.SponsorsAds
                .FirstOrDefaultAsync(a => a.AdID == viewModel.AdID);

            findingUpdateObj.AdPrice = viewModel.AdPrice;
            findingUpdateObj.SponsoredByName = viewModel.SponsoredByName;
            findingUpdateObj.ShowAdOnPage = viewModel.ShowAdOn;
            findingUpdateObj.Update_At = DateTime.Now;
            findingUpdateObj.AdStatus = "Live";
            findingUpdateObj.AdUrlDestination = viewModel.AdUrlDestination;
            findingUpdateObj.Expire_At = viewModel.Expire_At;
            if (viewModel.File != null)
            {
                // first delete the image from cloud
                var deletePrams = new DeletionParams(findingUpdateObj.PublicId);
                var cloudinaryDeletePhoto = _cloudinary.Destroy(deletePrams);

                // uploading the new image to cloudinary
                var uploadResult = new ImageUploadResult();
                using (var stream = viewModel.File.OpenReadStream())
                {
                    var uploadparams = new ImageUploadParams
                    {
                        File = new FileDescription(viewModel.File.Name, stream),
                    };
                    uploadResult = _cloudinary.Upload(uploadparams);
                }
                findingUpdateObj.AdPictureUrl = uploadResult.Url.ToString();
                findingUpdateObj.PublicId = uploadResult.PublicId;

            }
            _dataContext.SponsorsAds.Update(findingUpdateObj);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("{adId}")]
        public async Task<IActionResult> GetingleSponsoredAdDetail(int adId)
        {
            var findingAdById = await _dataContext.SponsorsAds.FirstOrDefaultAsync(a => a.AdID == adId);
            return Ok(findingAdById);
        }

        [HttpGet("GetLiveAdsList")]
        public async Task<IActionResult> GetLiveAdsList()
        {
            var liveAdsList = await _dataContext.SponsorsAds.Where(a => a.AdStatus == "Live")
                .ToListAsync();
            return Ok(liveAdsList);
        }

        [HttpGet("SponsoredAdsHistoryList")]
        public async Task<IActionResult> SponsoredAdsHistoryList()
        {
            var adsHistoryList = await _dataContext.SponsorsAds.Where(a => a.AdStatus != "Live")
                .ToListAsync();
            return Ok(adsHistoryList);
        }

        [HttpDelete("{adId}")]
        public async Task<IActionResult> DeleteOrExpireLiveSponser(int adId)
        {
            var findingSponserId = await _dataContext.SponsorsAds.Where(a => a.AdID == adId).FirstOrDefaultAsync();
            findingSponserId.AdStatus = "Expire";
            _dataContext.SponsorsAds.Update(findingSponserId);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("SearchingAdsForPageAvailable/{pageName}")]
        public async Task<IActionResult> SearchingAdsForPageAvailable(string pageName)
        {
            var findingAdsByPageName = await _dataContext.SponsorsAds
                .Where(a => a.AdStatus == "Live").ToListAsync();
            if (pageName == "Home")
            {
                List<SearchingSponsoreAdsViewModel> list = new List<SearchingSponsoreAdsViewModel>();
                foreach (var sponser in findingAdsByPageName)
                {
                    if (sponser.ShowAdOnPage == "HomePage")
                    {
                        list.Add(new SearchingSponsoreAdsViewModel
                        {
                            liveOnPageName = "HomePage",
                            SponsoreImageUrl = sponser.AdPictureUrl,
                            SponsoreWebsiteUrl = sponser.AdUrlDestination,
                        });
                    }
                    else if (sponser.ShowAdOnPage == "HomePopUpPage")
                    {
                        list.Add(new SearchingSponsoreAdsViewModel
                        {
                            liveOnPageName = "HomePopUpPage",
                            SponsoreImageUrl = sponser.AdPictureUrl,
                            SponsoreWebsiteUrl = sponser.AdUrlDestination,
                        });
                    }
                }

                return Ok(list);

            }
            else
            {
                var findingSelectedPage = findingAdsByPageName.FirstOrDefault(a => a.ShowAdOnPage == pageName);
                if(findingSelectedPage != null)
                {
                    return Ok(new SearchingSponsoreAdsViewModel
                    {
                        liveOnPageName = findingSelectedPage.ShowAdOnPage,
                        SponsoreImageUrl = findingSelectedPage.AdPictureUrl,
                        SponsoreWebsiteUrl = findingSelectedPage.AdUrlDestination
                    });
                }

                return BadRequest();
            }

        }





    }
}
