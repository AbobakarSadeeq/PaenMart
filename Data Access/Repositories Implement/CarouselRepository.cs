using Business_Core.Entities.Carousel;
using Business_Core.IRepositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Presentation.AppSettingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories_Implement
{
    public class CarouselRepository : Repository<int, Carousel>, ICarouselRepository
    {
        private readonly DataContext _DataContext;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public CarouselRepository(DataContext DataContext,
            IOptions<CloudinarySettings> cloudinaryConfig) : base(DataContext)
        {
            _DataContext = DataContext;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
               _cloudinaryConfig.Value.CloudName,
               _cloudinaryConfig.Value.ApiKey,
               _cloudinaryConfig.Value.ApiSecret
               );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<Carousel> AddingCarousel(Carousel carousel, IFormFile File)
        {
            var uploadResult = new ImageUploadResult();
            if (File.Length > 0)
            {
                using (var stream = File.OpenReadStream())
                {
                    var uploadparams = new ImageUploadParams
                    {
                        File = new FileDescription(File.Name, stream)

                    };
                    uploadResult = _cloudinary.Upload(uploadparams);
                }
            }
            await _DataContext.Carousels.AddAsync(new Carousel
            {
                PublicId = uploadResult.PublicId,
                URL = uploadResult.Url.ToString(),
                ImageDescription = carousel.ImageDescription,
                ImagePriority = carousel.ImagePriority,
                ImageTitle = carousel.ImageTitle
            });
            return carousel;
        }

        public void DeleteCarouselImage(Carousel  carousel)
        {
            var deletePrams = new DeletionParams(carousel.PublicId);
            var cloudinaryDeletePhoto = _cloudinary.Destroy(deletePrams);
            if (cloudinaryDeletePhoto.Result == "ok")
            {
                _DataContext.Carousels.Remove(carousel);
            }
        }
    }
}
