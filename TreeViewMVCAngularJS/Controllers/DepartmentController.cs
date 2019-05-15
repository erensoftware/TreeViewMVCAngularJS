using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeViewMVCAngularJS.Repository;
using TreeViewMVCAngularJS.Entity;

namespace TreeViewMVCAngularJS.Controllers
{
    public class DepartmentController : Controller
    {
        private static DepartmentRepository _deptRep;
        public DepartmentController()
        {
            _deptRep = new DepartmentRepository();
        }
        // GET: Department
        public ActionResult List()
        {
            var list = _deptRep.GetAll();
            return View(list.AsEnumerable());
        }
    }
}