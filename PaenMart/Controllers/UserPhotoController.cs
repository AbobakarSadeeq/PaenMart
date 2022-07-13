using AutoMapper;
using Business_Core.Entities.Identity;
using Bussiness_Core.Entities;
using Bussiness_Core.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.AppSettingClasses;
using Presentation.ViewModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Project_Levi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPhotoController : ControllerBase
    {
        private readonly IUserPhotoService userPhotoService;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly UserManager<CustomIdentity> _userManager;
        public UserPhotoController(IUserPhotoService userPhotoService, IMapper mapper, UserManager<CustomIdentity> userManager, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this.userPhotoService = userPhotoService;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _userManager = userManager;

            // Give the written keys that are in appsetting.json
            Account acc = new Account(
            _cloudinaryConfig.Value.CloudName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost("{userId}")]
        // Inserting Student Photo

        public async Task<IActionResult> AddingSingleUserPhoto(string userId, [FromForm] PhotoForCreationViewModel photoForCreationView)
        {
            // Who is adding the images to check is he/she is authorized or not in our application If User is not Authorized then give unAuthorized error.
            var User = await _userManager.FindByIdAsync(userId);
            var getPhotoFromUser = await userPhotoService.getSingleUserAllPhotos(userId);
            if (User == null)
            {
                return Unauthorized("Please You need to authorize yourself in Web");
            }

            // Storing file in a variable that are send from client
            var file = photoForCreationView.File;
            // Result that we back from Cloudinary
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            { // Reading the FIle Data or context that we are uploading
                using (var stream = file.OpenReadStream())
                {
                    var uploadparams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream),
                        // we can also crop the image if we want here means when user could upload his large size or big shape of image then crop it all its around thing just focus it on the face only
                        // it will crop the image automatically for us. 
                        Transformation = new Transformation()
                        .Width(824).Height(536)

                    };
                    // Uploading the image on clodinary server and could take a while
                    uploadResult = _cloudinary.Upload(uploadparams);
                }

            }
            // When the Image is uploaded or inserted in Cloudinary through API then we store that API URL in the Database as well public Id 

            // In video uploadResult.uri but i correct it to URL if anything happen wrong then change this back to uri
            photoForCreationView.URL = uploadResult.Url.ToString();
            photoForCreationView.PublicId = uploadResult.PublicId;
            photoForCreationView.CustomIdentityId = userId;

            // Converting that ViewModel Data now to Entity
            var convertViewModelToEntityData = _mapper.Map<UserImage>(photoForCreationView);
            // If the user upload first photo then we will consider it first and main photo of it of single user or singleItem
            if (getPhotoFromUser.UserImages.Count == 0)
            {
                convertViewModelToEntityData.IsMainPhoto = true;
            }

            // Store the Photos in Dbset of photo property
            User.UserImages.Add(convertViewModelToEntityData);

            // Save changes to the database and store photo URL's and more 
            if (Convert.ToBoolean(await userPhotoService.CommitAsync()))
            {
                // When photo is saved in the database than send response to the user of photo and converting Request ViewModel to Response ViewModel 
                var photoToReturn = _mapper.Map<PhotoForReturnViewModel>(convertViewModelToEntityData);
                // 
                //  return CreatedAtRoute("GetPhoto", new { Id = convertViewModelToEntityData.Id }, photoToReturn);
                // Return Response When Photo has been Added
                return Ok(photoToReturn);

            }

            return BadRequest("Sorry Error Found!");
        }

        // used for to get Single User All Its Photos
        [HttpGet("GetSingleAllUserPhoto/{Id}")]
        public async Task<IActionResult> GetSingleAllUserPhoto(string Id)
        {
            var gettingData = await userPhotoService.getSingleUserAllPhotos(Id);
            return Ok(gettingData);
        }

        // Used for to Delete the Photo From Database as well as in the cloudinary server
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePhotoUser(int Id)
        {
            // Get the Photo Id from database
            var photoFrom = await userPhotoService.GetPhotoUser(Id);
            // Deleting the Photo From Cloudinary using PublicId
            var deletePrams = new DeletionParams(photoFrom.PublicId);
            var cloudinaryDeletePhoto = _cloudinary.Destroy(deletePrams);
            if (cloudinaryDeletePhoto.Result == "ok")
            {
                // Delete that Id from photo table as well.
                await userPhotoService.DeletePhoto(Id);
                return Ok();
            }
            return BadRequest();
        }

        // IsMainPhoto FUnctionllity changing Calling like when user want to change the its main profile Photos

        [HttpPut("SetMainPhoto/{userId}/{PhotoId}")]
        public async Task<IActionResult> SetMainPhoto(string userId, int PhotoId)
        {
            // First Finding that Photo in the database
            var findingPhoto = await userPhotoService.GetPhotoUser(PhotoId);
            if (findingPhoto != null)
            {
                if (findingPhoto.IsMainPhoto == true)
                {
                    return BadRequest("This is already Main Photo");
                }
                // find the user first in photo table and than find its mainPhoto value which is true.
                var currentMianPhoto = await userPhotoService.GetMainPhotoForUser(userId);
                currentMianPhoto.IsMainPhoto = false;

                // When new Photo Id is found than change it false to true.
                findingPhoto.IsMainPhoto = true;
                if (Convert.ToBoolean(await userPhotoService.CommitAsync()))
                {
                    return Ok();
                }

            }
            return BadRequest("Not Found Photo");
        }


    }
}
