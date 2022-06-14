using Business_Core.Entities.Carousel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Core.IRepositories
{
    public interface ICarouselRepository : IRepository<int, Carousel>
    {
        void DeleteCarouselImage(Carousel carousel);
        Task<Carousel> AddingCarousel(Carousel carousel, IFormFile File);
    }
}
