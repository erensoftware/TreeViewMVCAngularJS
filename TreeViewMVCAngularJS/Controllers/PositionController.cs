using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeViewMVCAngularJS.Repository;
using TreeViewMVCAngularJS.Entity;
using TreeViewMVCAngularJS.Models;

namespace TreeViewMVCAngularJS.Controllers
{
    public class PositionController : Controller
    {
        private static PositionRepository _posRep;
        private static DepartmentRepository _deptRep;
        private static FirmRepository _firmRep;

        public PositionController()
        {
            _posRep = new PositionRepository();
            _deptRep = new DepartmentRepository();
            _firmRep = new FirmRepository();
        }
        public PositionController(PositionRepository posRep, DepartmentRepository deptRep, FirmRepository firmRep)
        {
            _posRep = posRep;
            _deptRep = deptRep;
            _firmRep = firmRep;
        }
        // GET: Position
        public ActionResult PositionHome()
        {
            return View();
        }
        private void loadLookUp(PositionViewModel pvm, int firmId)
        {
            var deptList = new List<SelectListItem>();
            var res = _deptRep.GetAllByMasterId(firmId);
            if (res != null && res.Count > 0)
            {
                foreach (var item in res)
                {
                    deptList.Add(new SelectListItem
                    {
                        Text = item.DepartmentName,
                        Value = item.DepartmentId.ToString()
                    });
                }
            }
            var posList = new List<SelectListItem>();
            var resp = _posRep.GetAllByMasterId(firmId);
            if (resp != null && resp.Count > 0)
            {
                foreach (var item in resp)
                {
                    if (item.PositionId != pvm.PositionId)
                        posList.Add(new SelectListItem
                        {
                            Text = item.PositionName,
                            Value = item.PositionId.ToString()
                        });
                }
            }
            pvm.DepartmentList = deptList;
            pvm.PositionList = posList;
        }
        public JsonResult GetFirm()
        {
            var res = _firmRep.GetAll();
            if (res != null && res.Count > 0)
            {
                List<object> retList = new List<object>();
                foreach (var item in res)
                {
                    retList.Add(new { FirmId = item.FirmId, FirmName = item.FirmName });
                }
                return Json(new JsonGetResultModel { result = true, data = retList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<object> msgList = new List<object>();
                msgList.Add("Error Fetching Firms from DB");
                return Json(new JsonGetResultModel { result = false, data = msgList }, JsonRequestBehavior.AllowGet);
            }
        }
        public void PrepareTreeViewModel(ICollection<TreeViewModel> pTree, Position item)
        {
            if (item != null)
            {
                TreeViewModel treeItem = new TreeViewModel
                {
                    IsActive = item.IsActive,
                    ParentName = item.Position2?.PositionName,
                    NodeId = item.PositionId,
                    NodeName = item.PositionName
                };
                pTree.Add(treeItem);
                if (item.Position1 != null && item.Position1.Count > 0)
                {
                    treeItem.Children = new List<TreeViewModel>();
                    foreach (var child in item.Position1)
                    {
                        PrepareTreeViewModel(treeItem.Children, child);
                    }
                }
            }
        }
        public JsonResult GetPositions(int? firmId)
        {
            if (firmId != null)
            {
                var res = _posRep.GetAllByMasterId((int)firmId);
                if (res != null && res.Count > 0)
                {
                    var treeView = new List<TreeViewModel>();
                    foreach (var item in res)
                    {
                        if (item.Position2 == null)
                            PrepareTreeViewModel(treeView, item);
                    }
                    return Json(new JsonGetPositionResultModel { result = true, data = treeView }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<object> msgList = new List<object>();
                    msgList.Add("Error Fetching Positions from DB");
                    return Json(new JsonGetResultModel { result = false, data = msgList }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<object> msgList = new List<object>();
                msgList.Add("Bad request firmId is null");
                return Json(new JsonGetResultModel { result = false, data = msgList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Add(int firmId, int? supPosId)
        {
            PositionViewModel pvm = new PositionViewModel();
            loadLookUp(pvm, firmId);
            pvm.ParentId = supPosId;
            return View(pvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Add(PositionViewModel pvm)
        {
            var res = _posRep.Insert(pvm.Position);
            if (res != null)
                return Json(new { result = true, data = res.PositionId });
            else
                return Json(new { result = false, data = -1 });
        }
        public ActionResult Edit(int firmId, int positionId)
        {
            var res = _posRep.GetSingle(positionId);
            PositionViewModel pvm = null;
            if (res == null)
            {
                pvm = new PositionViewModel();
            }
            else
            {
                pvm = new PositionViewModel(res);
            }
            loadLookUp(pvm, firmId);
            return View(pvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(PositionViewModel pvm)
        {
            var res = _posRep.Update(pvm.Position);
            if (res >  0)
                return Json(new
                {
                    result = true,
                    data = new
                    {
                        PositionId = pvm.PositionId,
                        PositionName = pvm.PositionName,
                        ParentName = pvm.ParentName,
                        IsActive = pvm.IsActive
                    }
                });
            else
                return Json(new { result = false, data = -1 });
        }
        [HttpPost]
        public JsonResult Delete(int positionId)
        {
            var res = _posRep.Delete(positionId);
            if (res > 0)
                return Json(new { result = true });
            else
                return Json(new { result = false });
        }
    }
}