using Awwsp.Data;
using Awwsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awwsp.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        protected static AcademyRepository repository;

        public BaseController()
        {
            repository = new AcademyRepository(db);
        }
    }
}