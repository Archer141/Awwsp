using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Awwsp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Awwsp.Data
{
    public class AcademyRepository : IAcademyRepository
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        public void AddAgeGroup(AgeGroup ageGroup)
        {
            if (ageGroup!=null)
            {
                dbContext.AgeGroups.Add(new AgeGroup { MaxAge = ageGroup.MaxAge, MinAge = ageGroup.MinAge, Name = ageGroup.Name });
                dbContext.SaveChangesAsync();
            }
            
        }
      
        public void AddChild(Child child)
        {
            throw new NotImplementedException();
        }

        public void AddNews(News news)
        {
            throw new NotImplementedException();
        }

        public void AddPhoto(Photo photo)
        {
            throw new NotImplementedException();
        }

        public void AddTrophy(Trophy trophy)
        {
            throw new NotImplementedException();
        }

        public void DeleteAgeGroup(int id)
        {
            dbContext.AgeGroups.Remove(GetAgeGroup(id));
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public AgeGroup GetAgeGroup(int id)
        {
           return dbContext.AgeGroups.Find(id);

        }

        public IList<AgeGroup> GetAgeGroups()
        {
            return dbContext.AgeGroups.ToList();
        }

        public Child GetChildById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Child> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IList<News> GetNews()
        {
            throw new NotImplementedException();
        }

        public News GetNewsByID(int id)
        {
            throw new NotImplementedException();
        }

        public Photo GetPhotoById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Photo> GetPhotos()
        {
            throw new NotImplementedException();
        }


        public IList<Trophy> GetTrophies()
        {
            throw new NotImplementedException();
        }

        public Trophy GetTrophyById(int id)
        {
            throw new NotImplementedException();
        }

        

        public IList<ApplicationUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void RemoveAgeGroup(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveChild(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveNews(int id)
        {
            throw new NotImplementedException();
        }

        public void RemovePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveTrophy(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAgeGroup(AgeGroup ageGroup)
        {
            dbContext.Entry(ageGroup).State = EntityState.Modified;
            dbContext.SaveChanges();
            //var a = dbContext.AgeGroups.Find(ageGroup.AgeGroupID);
            //a.MaxAge = ageGroup.MaxAge;
            //a.MinAge = ageGroup.MinAge;
            //a.Name = ageGroup.Name;
           
        }

        public void UpdateChild(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateNews(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePhoto(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrophy(int id)
        {
            throw new NotImplementedException();
        }
    }
}