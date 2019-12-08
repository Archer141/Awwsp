using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Awwsp.Data
{
    interface IAcademyRepository:IDisposable
    {

        IList<Child> GetChildren(string search, string sort, string sortDir, int skip, int pageSize, out int totalRecord);
        IList<AgeGroup> GetAgeGroups();
        IList<Child> GetChildrenAll();
        IList<News> GetNews();
        IList<Photo> GetPhotos();
        IList<Trophy> GetTrophies();

        Task <Child> GetChildById(int? id);
        News GetNewsByID(int? id);
        Photo GetPhotoById(int? id);
        Trophy GetTrophyById(int? id);
        AgeGroup GetAgeGropuById(int? id);

        void AddChild(Child child);
        void AddNews(News news);
        void AddPhoto(Photo photo, HttpPostedFileBase image);
        void AddTrophy(Trophy trophy);
        void AddAgeGroup(AgeGroup ageGroup);

        void DeleteChild(int? id);
        void DeleteNews(int? id);
        void DeletePhoto(int? id);
        void DeleteTrophy(int? id);
        void DeleteAgeGroup(int? id);

        void UpdateChild(Child child);
        void UpdateNews(News news);
        void UpdatePhoto(Photo photo, HttpPostedFileBase image);
        void UpdateTrophy(Trophy trophy);
        void UpdateAgeGroup(AgeGroup ageGroup);

        string PasswordHash(string value);
    }
}
