using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awwsp.Data
{
    interface IAcademyRepository:IDisposable
    {
        IList<ApplicationUser> GetUsers();
        IList<AgeGroup> GetAgeGroups();
        IList<Child> GetChildren();
        IList<News> GetNews();
        IList<Photo> GetPhotos();
        IList<Trophy> GetTrophies();
        Child GetChildById(int id);
        News GetNewsByID(int id);
        Photo GetPhotoById(int id);
        Trophy GetTrophyById(int id);
        AgeGroup GetAgeGroup(int id);
        void AddChild(Child child);
        void AddNews(News news);
        void AddPhoto(Photo photo);
        void AddTrophy(Trophy trophy);
        void AddAgeGroup(AgeGroup ageGroup);

        void RemoveChild(int id);
        void RemoveNews(int id);
        void RemovePhoto(int id);
        void RemoveTrophy(int id);
        void RemoveAgeGroup(int id);

        void UpdateChild(int id);
        void UpdateNews(int id);
        void UpdatePhoto(int id);
        void UpdateTrophy(int id);
        void UpdateAgeGroup(AgeGroup ageGroup);

        void DeleteAgeGroup(int id);

    }
}
