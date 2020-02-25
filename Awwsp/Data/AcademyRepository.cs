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
        public void AddEvent(Event @event)
        {
            dbContext.Events.Add(@event);
            dbContext.SaveChanges();
        }



        public AgeGroup GetAgeGropuById(int? id)
        {
            return dbContext.AgeGroups.Include("Children").Where(x=>x.AgeGroupId==id).FirstOrDefault();
        }

        public Child GetChildById(int? id)
        {
            return dbContext.Children.FindAsync(id).Result;
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
            return dbContext.Trophies.Include(x=>x.Photo).Include(x=>x.Children).Where(z=>z.TrophyID==id).FirstOrDefault();
        }

        public Event GetEventById(int? id)
        {
            return dbContext.Events.Include("AgeGRoup").Where(x=>x.Id==id).FirstOrDefaultAsync().Result;
        } 
        public List<Event> GetEventFor(int? id)
        {
            if (id!=null)
            {
                return dbContext.Events.Include("AgeGroup").Where(x => x.AgeGroupID == id).ToListAsync().Result;

            }
            else
            {
                return null;
            }
        }

        public IList<AgeGroup> GetAgeGroups()
        {
            var list = dbContext.AgeGroups.Include("Children").ToListAsync();
            return list.Result;
        }

        public IList<Child> GetChildrenAll()
        {
            return dbContext.Children.Include("AgeGroup").Include(x=>x.Trophies).ToListAsync().Result;
        }
        /// <summary>
        /// Get parent children
        /// </summary>
        /// <param name="id">Parent id</param>
        /// <returns></returns>
        public IList<Child> GetChildrenAll(string id)
        {
            var list = dbContext.Children.Include("AgeGroup").Where(x => x.UserID == id).ToListAsync().Result;
            return list;
        }

        public IList<News> GetNews()
        {
            return dbContext.News.Include("Photo").ToListAsync().Result;
        }

        public IList<Photo> GetPhotos()
        {
            return dbContext.Photos.ToListAsync().Result;
        }

        public IList<Trophy> GetTrophies()
        {
            return dbContext.Trophies.Include("Photo").Include("Children").ToList();
        }

        public List<Event> GetEvents()
        {
            return dbContext.Events.Include("AgeGroup").ToList();
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

        public void DeleteChild(Child child)
        {
            try
            {
                var notifi = dbContext.Notifications.Where(x => x.ChildId == child.ChildID).ToList();
                foreach (var item in notifi)
                {
                    dbContext.Notifications.Remove(item);
                }

                dbContext.Children.Remove(child);
                dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public void DeleteNews(int? id)
        {
            dbContext.News.Remove(GetNewsByID(id));
            dbContext.SaveChanges();
        }

        public void DeletePhoto(int? id)
        {
            var newsList= dbContext.News.Where(x => x.PhotoID == id).ToList();
            var trophiesList= dbContext.Trophies.Where(x => x.PhotoID == id).ToList();
            foreach (var item in newsList)
            {
                item.PhotoID = null;
            }
            foreach (var item in trophiesList)
            {
                item.PhotoID = null;
            }
            dbContext.Photos.Remove(GetPhotoById(id));
            dbContext.SaveChanges();
        }

        public void DeleteTrophy(int? id)
        {
            dbContext.Trophies.Remove(GetTrophyById(id));
            dbContext.SaveChanges();
        }
         public void DeleteEvent(int? id)
        {
            dbContext.Events.Remove(GetEventById(id));
            dbContext.SaveChanges();
        }
        public void SignOutChild(Child child)
        {
            var user = GetChildById(child.ChildID);
            user.IsSignOut = true;
            user.IsActive = false;
            dbContext.SaveChanges();
        }
        public void SignInChild(Child child)
        {
            var user = GetChildById(child.ChildID);
            user.IsSignOut = false;
            dbContext.SaveChanges();
        }






        public void UpdateAgeGroup(AgeGroup ageGroup)
        {
            var aG = GetAgeGropuById(ageGroup.AgeGroupId);
            aG.MaxAge = ageGroup.MaxAge;
            aG.MinAge = ageGroup.MinAge;
            aG.Name = ageGroup.Name;
            dbContext.SaveChanges();
        }

        public void UpdateChild(Child child)
        {
           var player= GetChildById(child.ChildID);
            player.AgeGroupID = child.AgeGroupID;
            player.ChildFirstName = child.ChildFirstName;
            player.ChildLastName = child.ChildLastName;
            player.FullName = child.FullName;
            player.UserID = child.UserID;
            player.PasswordHash = child.PasswordHash;
            player.IsSignOut = child.IsSignOut;
            player.IsActive = child.IsActive;
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
            photoModify.Date = DateTime.Now;
            photoModify.Image = new byte[image.ContentLength];
            image.InputStream.Read(photoModify.Image, 0, image.ContentLength);
            dbContext.SaveChanges();
        }
        public void UpdatePhoto(Photo photo)
        {
            var photoModify = GetPhotoById(photo.PhotoID);
            photoModify.Date = DateTime.Now;
            photoModify.IsTrophy = photo.IsTrophy;
            photoModify.Name = photo.Name;
            dbContext.SaveChanges();
        }
        public void UpdateTrophy(Trophy trophy)
        {
            var trophyModify = GetTrophyById(trophy.TrophyID);
            trophyModify.Date = DateTime.Now;
            trophyModify.PhotoID = trophy.PhotoID;
            trophyModify.Name = trophy.Name;
            dbContext.SaveChanges();
        }
        public void UpdateEvent(Event @event)
        {
            var eventToModify = GetEventById(@event.Id);
            eventToModify = @event;
            dbContext.SaveChanges();
        }


        public IList<Notification> GetNotifications()
        {
            return dbContext.Notifications.Include("AgeGroup").Include("Child").ToList();
        }

        public Notification GetNotificationById(int? id)
        {
            return GetNotifications().Where(x => x.Id == id).FirstOrDefault();
        }

        public void AddNotification(Notification notification)
        {
            dbContext.Notifications.Add(notification);
            dbContext.SaveChanges();
        }

        public void DeleteNotification(int? id)
        {
            var notifiTodelete = GetNotificationById(id);
            dbContext.Notifications.Remove(notifiTodelete);
            dbContext.SaveChanges();
        }

        public void UpdateNotification(Notification notification)
        {
            var notifyForUpdate = GetNotificationById(notification.Id);
            notifyForUpdate.AgeGroupId = notification.AgeGroupId;
            notifyForUpdate.Title = notification.Title;
            notifyForUpdate.Text = notification.Text;
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

        public bool SetPercived(List<Notification> notifications)
        {
            try
            {
                foreach (var item in notifications)
                {
                    item.Perceived = true;
                    UpdateNotification(item);
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
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