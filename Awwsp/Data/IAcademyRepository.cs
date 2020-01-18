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

       
        IList<AgeGroup> GetAgeGroups();
        IList<Child> GetChildrenAll();
        IList<News> GetNews();
        IList<Photo> GetPhotos();
        IList<Trophy> GetTrophies();
        IList<Notification>  GetNotifications();
        IList<Event>  GetEvents();

        Child GetChildById(int? id);
        News GetNewsByID(int? id);
        Photo GetPhotoById(int? id);
        Trophy GetTrophyById(int? id);
        AgeGroup GetAgeGropuById(int? id);
        Notification GetNotificationById(int? id);
        Event GetEventById(int? id);

        void AddChild(Child child);
        void AddNews(News news);
        void AddPhoto(Photo photo, HttpPostedFileBase image);
        void AddTrophy(Trophy trophy);
        void AddAgeGroup(AgeGroup ageGroup);
        void AddNotification(Notification notification);
        void AddEvent(Event @event);

        void DeleteChild(int? id);
        void DeleteNews(int? id);
        void DeletePhoto(int? id);
        void DeleteTrophy(int? id);
        void DeleteAgeGroup(int? id);
        void DeleteNotification(int? id);
        void DeleteEvent(int? id);

        void UpdateChild(Child child);
        void UpdateNews(News news);
        void UpdatePhoto(Photo photo, HttpPostedFileBase image);
        void UpdateTrophy(Trophy trophy);
        void UpdateAgeGroup(AgeGroup ageGroup);
        void UpdateNotification(Notification notification);
        void UpdateEvent(Event @event);

        string PasswordHash(string value);
    }
}
