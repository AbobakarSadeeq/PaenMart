using Business_Core.Entities.Identity;
using Bussiness_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Core.IServices
{
   public interface IUserPhotoService
    {
        Task<UserImage> GetPhotoUser(int Id);

        Task<CustomIdentity> getSingleUserAllPhotos(string Id);
        Task<UserImage> DeletePhoto(int Id);

        // Changing or Removing the Old IsMain Photo to False

        Task<UserImage> GetMainPhotoForUser(string userId);

        Task<int> CommitAsync();

    }
}
