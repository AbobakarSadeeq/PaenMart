using Business_Core.Entities.Carousel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IServices
{
    public interface ICarouselService
    {
        Task<IEnumerable<Carousel>> GetCarousel();
        Task<Carousel> GetCarouselById(int Id);
        Task<Carousel> InsertCarousel(Carousel carousel, IFormFile File);
        Task<Carousel> DeleteCarousel(Carousel carousel);
        Task<Carousel> UpdateCarousel(Carousel OldData, Carousel NewData);
    }
}
