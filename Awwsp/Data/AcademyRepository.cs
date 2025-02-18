﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
                // dbContext.AgeGroups.Add(new AgeGroup { MaxAge = ageGroup.MaxAge, MinAge = ageGroup.MinAge, Name = ageGroup.Name });
                dbContext.AgeGroups.Add(ageGroup);
                dbContext.SaveChanges();
            }
        }

        public void AddChild(Child child)
        {
            dbContext.Children.Add(child);
            dbContext.SaveChanges();
        }

        public void AddNews(News news)
        {
            dbContext.News.Add(news);
            dbContext.SaveChanges();
        }

        public void AddPhoto(Photo photo, HttpPostedFileBase image)
        {
            photo.Image = new byte[image.ContentLength];
            image.InputStream.Read(photo.Image, 0, image.ContentLength);

            dbContext.Photos.Add(photo);
            dbContext.SaveChanges();
        }

        public void AddTrophy(Trophy trophy)
        {
            dbContext.Trophies.Add(trophy);
            dbContext.SaveChanges();
        }




        public AgeGroup GetAgeGropuById(int? id)
        {
            return dbContext.AgeGroups.Find(id);
        }

        public Task<Child> GetChildById(int? id)
        {
            return dbContext.Children.FindAsync(id);
        }

        public News GetNewsByID(int? id)
        {
            return dbContext.News.Include(n=>n.Photo).Where(x=>x.NewsID==id).FirstOrDefault();
        }

        public Photo GetPhotoById(int? id)
        {
            return dbContext.Photos.FindAsync(id).Result;
        }

        public Trophy GetTrophyById(int? id)
        {
            return dbContext.Trophies.FindAsync(id).Result;
        }







        public IList<AgeGroup> GetAgeGroups()
        {
            var list = dbContext.AgeGroups.ToList();
            return list;
        }

        public IList<Child> GetChildrenAll()
        {
            return dbContext.Children.ToListAsync().Result;
        }
        public IList<Child> GetChildrenAll(string id)
        {
            var list = dbContext.Children.Where(x => x.UserID == id).ToList();

            return list;
        }
        public IList<Child> GetChildren(string search,string sort, string sortDir,int skip,int pageSize, out int totalRecord)
        {
          var list =  (from a in dbContext.Children where
            a.ChildFirstName.Contains(search) ||
            a.ChildLastName.Contains(search)
             select a);
            totalRecord = list.Count();
            list.OrderBy(sort + " " + sortDir);
            if (pageSize>0)
            {
                list = list.Skip(skip).Take(pageSize);
            }
            return list.ToList();
        }

        public IList<News> GetNews()
        {
            return dbContext.News.ToListAsync().Result;
        }

        public IList<Photo> GetPhotos()
        {
            return dbContext.Photos.ToListAsync().Result;
        }

        public IList<Trophy> GetTrophies()
        {
            return dbContext.Trophies.ToListAsync().Result;
        }







        public void DeleteAgeGroup(int? id)
        {
            var children = dbContext.Children.Where(x => x.AgeGroupID == id);
            foreach (var item in children)
            {
                try
                {
                    item.AgeGroupID = null;
                    dbContext.SaveChanges();
                }
                catch (Exception )
                {
                    throw;
                }
            }

            dbContext.AgeGroups.Remove(GetAgeGropuById(id));
            dbContext.SaveChanges();
        }

        public void DeleteChild(int? id)
        {
            dbContext.Children.Remove(GetChildById(id).Result);
            dbContext.SaveChangesAsync();
        }

        public void DeleteNews(int? id)
        {
            dbContext.News.Remove(GetNewsByID(id));
            dbContext.SaveChanges();
        }

        public void DeletePhoto(int? id)
        {
            dbContext.Photos.Remove(GetPhotoById(id));
            dbContext.SaveChanges();
        }

        public void DeleteTrophy(int? id)
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


        public string PasswordHash(string value)
        {
            using (MD5CryptoServiceProvider mD = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uTF8 = new UTF8Encoding();
                byte[] data = mD.ComputeHash(uTF8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
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