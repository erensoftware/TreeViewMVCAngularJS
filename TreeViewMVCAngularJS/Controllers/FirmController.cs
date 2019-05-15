using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeViewMVCAngularJS.Repository;
using TreeViewMVCAngularJS.Entity;

namespace TreeViewMVCAngularJS.Controllers
{
    public class FirmController : Controller
    {
        private static FirmRepository _firmRep;
        public FirmController()
        {
            _firmRep = new FirmRepository();
        }
        // GET: Firm
        public ActionResult List()
        {
            var list = _firmRep.GetAll();
            return View(list.AsEnumerable());
        }
        public ActionResult Add()
        {
            return View();
        }

    }
}