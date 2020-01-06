using Awwsp.Data;
using Awwsp.Models;
using Awwsp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    public class HomeController : DefaultController
    {

        private AcademyRepository academyRepository;
        private ApplicationDbContext dbContext;

        public HomeController()
        {
            dbContext = new ApplicationDbContext();
            academyRepository = new AcademyRepository(dbContext);
        }
        public ActionResult Index()
        {
            List<Notification> yourNotifications = new List<Notification>();
            
            if (User.Identity.IsAuthenticated)
            {
            
                //Zalogowane dziecko
                var childLoged = academyRepository.GetChildrenAll().Where(x => x.FullName.ToLower() == User.Identity.Name.ToLower()).FirstOrDefault();

                //Dzieci zalogowanego użytkownika
                var yourChildren = academyRepository.GetChildrenAll().Where(x => x.UserID == GetUserID()).ToList();

                //Znajdź powiadomiena dla każdego dziecka
                if (GetUserID()!="")
                {
                    if (yourChildren.Count!=0)
                    {
                        foreach (var item in yourChildren)
                        {
                            var notifi = academyRepository.GetNotifications().Where(x => x.AgeGroupId == item.AgeGroupID).LastOrDefault();
                            if (notifi != null)
                            {
                                yourNotifications.Add(notifi);
                            }
                        }
                    }
                  
                }
                else//lub jeśli zalogowane jest dziecko znajdujemy tylko jedno powiadomienie dla zawodnika
                {
                    var notifiChild = academyRepository.GetNotifications().Where(x => x.AgeGroupId == childLoged.AgeGroupID).LastOrDefault();
                    yourNotifications.Add(notifiChild);
                }
            }
            var news = academyRepository.GetNews().Reverse().Take(3).ToList();

            foreach (var item in news)
            {
                if (item.Text.Length > 300)
                {
                    item.Text = item.Text.Substring(0, 300);
                    item.Text = item.Text + "...";
                }

                if (item.Title.Length > 20)
                {
                    item.Title = item.Title.Substring(0, 20);
                    item.Title = item.Title + "...";
                }
            }
            //usunięcie powtórzeń z listy powiadomień
            yourNotifications = yourNotifications.Distinct().ToList();
            HomeVM homeVm = new HomeVM { NotificationsTop3 = yourNotifications, NewsTop3 = news };

            return View(homeVm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public string GetUserID()
        {
            string userIdValue = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    userIdValue = userIdClaim.Value;
                }
            }
            return userIdValue;
        }

    }
}