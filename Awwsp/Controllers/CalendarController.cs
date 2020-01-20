using Awwsp.Data;
using Awwsp.Models;
using Awwsp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    public class CalendarController : Controller
    {

        AcademyRepository repository;
        ApplicationDbContext context = new ApplicationDbContext();
        public CalendarController()
        {
            repository = new AcademyRepository(context);
        }

        // GET: Callendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.AgeGroupID = new SelectList(context.AgeGroups, "AgeGroupID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventCreateVM eventT)
        {
           
            if (ModelState.IsValid)
            {
                Event @event;

                @event = new Event()
                {
                    Id = eventT.Id,
                    Title = eventT.Title,
                    Start = eventT.Start,
                    End = eventT.End,
                    AllDay = eventT.AllDay,
                    AgeGroupID = eventT.AgeGroupID,
                };

                if (eventT.RepetedThroughtWeeks != null)
                {
                   

                    for (int i = 0; i <= eventT.RepetedThroughtWeeks; i++)
                    {
                        @event = new Event()
                        {
                            Id = eventT.Id,
                            Title = eventT.Title,
                            Start = eventT.Start.AddDays(i*7),
                            End = eventT.End.AddDays(i*7),
                            AllDay = eventT.AllDay,
                            AgeGroupID = eventT.AgeGroupID,
                        };
                        repository.AddEvent(@event);
                      
                    }
                }
                else
                {
                    repository.AddEvent(@event);
                }


                return RedirectToAction("EventList");
            }
            else
            {
                ViewBag.AgeGroupID = new SelectList(context.AgeGroups, "AgeGroupID", "Name");
                return View(eventT);
            }

        }


        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
              var eventT = repository.GetEventById(id);
                if (eventT!=null)
                {

             
              EventCreateVM  @event = new EventCreateVM()
                {
                    Id = eventT.Id,
                    Title = eventT.Title,
                    Start = eventT.Start,
                    End = eventT.End,
                    AllDay = eventT.AllDay,
                    AgeGroupID = eventT.AgeGroupID,
                };
                ViewBag.AgeGroupID = new SelectList(context.AgeGroups, "AgeGroupID", "Name");
                return View(@event);
                }
                return View();

            }
            return View();

        }
        [HttpPost]
        public ActionResult Edit(Event eventT)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateEvent(eventT);
            }
            else
            {
                ViewBag.AgeGroupID = new SelectList(context.AgeGroups, "AgeGroupID", "Name");
                return View(eventT);
            }
            return View("Index");
        }

        public ActionResult EventList()
        {
            return View(repository.GetEvents());
        }

        public ActionResult GetEvents()
        {
            try
            {
                List<Event> events = new List<Event>();
                var userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    var children = context.Users.Where(x => x.Id == userId).Select(c => c.Children).FirstOrDefault();

                    foreach (var item in children)
                    {
                        events.AddRange(repository.GetEventFor(item.AgeGroupID));
                    }

                }
                else
                {
                    var name = User.Identity.Name;
                    var ageGroupID = repository.GetAgeGroups().Where(x => x.Children.Where(a => a.FullName == name).FirstOrDefault().FullName == name).Select(c => c.AgeGroupId).FirstOrDefault();
                    events = repository.GetEventFor(ageGroupID);

                }

                var listEventVm = events.Select(x => new EventVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Start = x.Start.ToString("yyyy-MM-dd HH:mm:ss"),
                    End = x.End.ToString("yyyy-MM-dd HH:mm:ss"),
                    AllDay = x.AllDay,
                    Color = x.Color,
                    TextColor = x.TextColor,
                }).ToList();

                return Json(new
                {
                    Data = listEventVm,
                    IsSuccesful = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseVM
                {
                    IsSuccesfull = false,
                    Errors = new List<string> { ex.Message }
                }, JsonRequestBehavior.AllowGet);

            }

        }
    }
}