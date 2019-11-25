using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Awwsp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Awwsp.Data
{
    public class AcademyRepository : IAcademyRepository
    {
        private ApplicationDbContext dbContext;
        public AcademyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddAgeGroup(AgeGroup ageGroup)
        {
            if (ageGroup != null)
            {
                dbContext.AgeGroups.Add(new AgeGroup { MaxAge = ageGroup.MaxAge, MinAge = ageGroup.MinAge, Name = ageGroup.Name });
                dbContext.SaveChangesAsync();
            }
        }

        public void AddChild(Child child)
        {
            dbContext.Children.Add(child);
            dbContext.SaveChangesAsync();
        }

        public void AddNews(News news)
        {
            dbContext.News.Add(news);
            dbContext.SaveChangesAsync();
        }

        public void AddPhoto(Photo photo,HttpPostedFileBase image)
        {
            photo.Image = new byte[image.ContentLength];
            image.InputStream.Read(photo.Image, 0, image.ContentLength);

            dbContext.Photos.Add(photo);
            dbContext.SaveChangesAsync();
        }

        public void AddTrophy(Trophy trophy)
        {
            dbContext.Trophies.Add(trophy);
            dbContext.SaveChangesAsync();
        }






        public AgeGroup GetAgeGropuById(int id)
        {
            return dbContext.AgeGroups.Find(id);
        }

        public IList<AgeGroup> GetAgeGroups()
        {
            return dbContext.AgeGroups.ToListAsync().Result;
        }

        public Child GetChildById(int id)
        {
            return dbContext.Children.FindAsync(id).Result;
        }

        public IList<Child> GetChildren()
        {
            return dbContext.Children.ToListAsync().Result;
        }

        public IList<News> GetNews()
        {
            return dbContext.News.ToListAsync().Result;
        }

        public News GetNewsByID(int id)
        {
            return dbContext.News.FindAsync(id).Result;
        }

        public Photo GetPhotoById(int id)
        {
            return dbContext.Photos.FindAsync(id).Result;
        }

        public IList<Photo> GetPhotos()
        {
            return dbContext.Photos.ToListAsync().Result;
        }


        public IList<Trophy> GetTrophies()
        {
            return dbContext.Trophies.ToListAsync().Result;
        }

        public Trophy GetTrophyById(int id)
        {
            return dbContext.Trophies.FindAsync(id).Result;
        }




        public void DeleteAgeGroup(int id)
        {
            dbContext.AgeGroups.Remove(GetAgeGropuById(id));
            dbContext.SaveChanges();
        }

        public void DeleteChild(int id)
        {
            dbContext.Children.Remove(GetChildById(id));
            dbContext.SaveChanges();
        }

        public void DeleteNews(int id)
        {
            dbContext.News.Remove(GetNewsByID(id));
            dbContext.SaveChanges();
        }

        public void DeletePhoto(int id)
        {
            dbContext.Photos.Remove(GetPhotoById(id));
            dbContext.SaveChanges();
        }

        public void DeleteTrophy(int id)
        {
            dbContext.Trophies.Remove(GetTrophyById(id));
            dbContext.SaveChanges();
        }

        public void UpdateAgeGroup(AgeGroup ageGroup)
        {
            var aG = GetAgeGropuById(ageGroup.AgeGroupID);
            aG.MaxAge = ageGroup.MaxAge;
            aG.MinAge = ageGroup.MinAge;
            aG.Name = ageGroup.Name;
            dbContext.SaveChanges();
        }

        public void UpdateChild(Child child)
        {
            dbContext.Entry(child).State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public void UpdateNews(News news)
        {
            dbContext.Entry(news).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void UpdatePhoto(Photo photo, HttpPostedFileBase image)
        {
            var photoModify = GetPhotoById(photo.PhotoID);
            photoModify.Name = photo.Name;

            dbContext.SaveChanges();
        }

        public void UpdateTrophy(Trophy trophy)
        {
            dbContext.Entry(trophy).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AcademyRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}