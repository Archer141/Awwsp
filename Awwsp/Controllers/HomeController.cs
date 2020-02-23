using Awwsp.Data;
using Awwsp.Models;
using Awwsp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Awwsp.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            List<Notification> yourNotifications = new List<Notification>();
            
            if (User.Identity.IsAuthenticated)
            {
                if (repository.GetChildrenAll().Where(x=>x.UserID==User.Identity.GetUserId())!=null)
                {
                    //Znajdź powiadomiena dla każdego dziecka
                    if (GetUserID() != "")
                    {
                        //Dzieci zalogowanego użytkownika
                     var yourChildren = repository.GetChildrenAll().Where(x => x.UserID == GetUserID()).ToList();
                        if (yourChildren.Count != 0)
                        {
                            foreach (var item in yourChildren)
                            {
                                var notifi = repository.GetNotifications().Where(x => x.AgeGroupId == item.AgeGroupID&&x.ChildId==null).LastOrDefault();
                                var notifi2 = repository.GetNotifications().Where(x => x.ChildId == item.ChildID).LastOrDefault();
                                if (notifi != null)
                                {
                                    yourNotifications.Add(notifi);
                                }
                                if (notifi2!=null)
                                {
                                    yourNotifications.Add(notifi2);
                                }
                            }
                        }

                    }
                    else//lub jeśli zalogowane jest dziecko znajdujemy tylko jedno powiadomienie dla zawodnika
                    {
                        //Zalogowane dziecko
                        var childLoged = repository.GetChildrenAll().Where(x => x.FullName.ToLower() == User.Identity.Name.ToLower()).FirstOrDefault();
                        var notifiChild = repository.GetNotifications().Where(x => x.AgeGroupId == childLoged.AgeGroupID&&x.ChildId==null).LastOrDefault();
                        var notifiChild2 = repository.GetNotifications().Where(x => x.ChildId == childLoged.ChildID).LastOrDefault();
                        if (notifiChild != null)
                        {
                            yourNotifications.Add(notifiChild);
                        }
                        if (notifiChild2 != null)
                        {
                            yourNotifications.Add(notifiChild2);
                        }
                    }
                }
            }

            var news = repository.GetNews().Reverse().Take(3).ToList();

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

            var a = repository.SetPercived(yourNotifications);

            HomeVM homeVm = new HomeVM { Notifications = yourNotifications, NewsTop3 = news };

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