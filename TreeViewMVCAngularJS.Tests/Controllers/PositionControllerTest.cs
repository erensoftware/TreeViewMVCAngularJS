using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TreeViewMVCAngularJS.Controllers;
using System.Runtime.CompilerServices;
using System.Dynamic;
using TreeViewMVCAngularJS.Models;
using TreeViewMVCAngularJS.Entity;
using TreeViewMVCAngularJS.Repository;
//this attribute decoration is supposed to be used for anonymous controller object returned as Json to be able to cast to dynamic type and access its properties. But it does not work... 
[assembly: InternalsVisibleTo("TreeViewMVCAngularJS.Controllers")]
namespace TreeViewMVCAngularJS.Tests.Controllers
{
    [TestClass]
    public class PositionControllerTest
    {
        [TestMethod]
        public void PositionHome()
        {
            // Arrange
            PositionController controller = new PositionController();

            // Act
            ViewResult result = controller.PositionHome() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetFirm()
        {
            // Arrange
            PositionController controller = new PositionController();

            // Act
            JsonResult result = controller.GetFirm();
            JsonGetResultModel resData = null;
            string errMsg = "";
            try
            {
                resData = (JsonGetResultModel)result.Data;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                resData = null;
            }
            // Assert
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("", errMsg);
            Assert.IsNotNull(resData);
            Assert.AreEqual(true, resData.result);
            Assert.AreEqual(1, resData.data.Count);
        }
        [TestMethod]
        public void PrepareTreeViewModel()
        {
            // Arrange
            PositionController controller = new PositionController();
            PositionRepository posRep = new PositionRepository();
            List<Position> posList = posRep.GetAllTopPositions(); //get root positions from db
            var treeView = new List<TreeViewModel>();

            // Act
            controller.PrepareTreeViewModel(treeView, posList[0]);

            // Assert
            Assert.IsNotNull(treeView);
            Assert.AreNotEqual(0, treeView.Count);
            Assert.IsNotNull(treeView[0].Children);
            Assert.AreNotEqual(0, treeView[0].Children.Count);
        }
        [TestMethod]
        public void GetPositions()
        {
            // Arrange
            PositionController controller = new PositionController();

            // Act
            JsonResult result = controller.GetPositions(1);
            JsonGetPositionResultModel resData = null;
            string errMsg = "";
            try
            {
                resData = (JsonGetPositionResultModel)result.Data;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                resData = null;
            }
            // Assert
            Assert.IsNotNull(result.Data);
            Assert.AreEqual("", errMsg);
            Assert.IsNotNull(resData);
            Assert.AreEqual(true, resData.result);
            Assert.AreNotEqual(0, resData.data.Count);
        }
    }
}
