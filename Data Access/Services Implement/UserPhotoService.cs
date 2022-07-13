using Business_Core.Entities.Identity;
using Bussiness_Core.Entities;
using Bussiness_Core.IServices;
using Data_Access.DataContext_Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Services_Implementation
{
   public class UserPhotoService : IUserPhotoService
    {
        private readonly DataContext dataContext;

        public UserPhotoService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<UserImage> DeletePhoto(int Id)
        {
            var findingData = await dataContext.UserImages.FindAsync(Id);
            dataContext.UserImages.Remove(findingData);
            await dataContext.SaveChangesAsync();
            return findingData;
        }

        // Used for to change the main photo to another
        public async Task<UserImage> GetMainPhotoForUser(string userId)
        {
            // When User Id is found than in the photo table check that same user Id which photo is true
            return await dataContext.UserImages.Where(a => a.CustomIdentityId == userId).FirstOrDefaultAsync(a => a.IsMainPhoto == true);
        }

        public async Task<UserImage> GetPhotoUser(int Id)
        {
            var photo = await dataContext.UserImages.FirstOrDefaultAsync(a => a.Id == Id);
            return photo;
        }

        public async Task<CustomIdentity> getSingleUserAllPhotos(string Id)
        {
            var gettingAllPhotos = await dataContext.Users.Include(a => a.UserImages).SingleOrDefaultAsync(a => a.Id == Id);
            return gettingAllPhotos;
        }

        public async Task<int> CommitAsync()
        {
            return await dataContext.SaveChangesAsync();
        }
    }
}
