using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using TALExamMVC.Models;

namespace TALExamMVC.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        [HttpGet]
        public ActionResult Index()
        {
            Member member = new Member();
            return View(member);
        }

        //POST
        [HttpPost]
        public ActionResult Index(Member member)
        {
            var occupation = member.OccupationList.FirstOrDefault(x => x.Title == member.Occupation);
            var rate = occupation.Rating;
            member.MonthlyPremium = (member.SumInsured * rate * member.Age);
            return View(member);
        }

        

        
    }
}