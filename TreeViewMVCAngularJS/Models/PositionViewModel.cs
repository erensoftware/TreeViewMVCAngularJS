using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeViewMVCAngularJS.Entity;

namespace TreeViewMVCAngularJS.Models
{
    public class PositionViewModel
    {
        private Position _position; 
        public PositionViewModel()
        {
            _position = new Position();
        }
        public PositionViewModel(Position position)
        {
            _position = position;
        }
        public int PositionId { get { return _position.PositionId; } set { _position.PositionId = value; } }
        [Display(Name = "Position")]
        public string PositionName { get { return _position.PositionName; } set { _position.PositionName = value; } }
        public int? DepartmentId { get { return _position.DepartmentId; } set { _position.DepartmentId = value; } }
        public bool IsActive { get { return _position.IsActive; } set { _position.IsActive = value; } }
        [Display(Name = "Superior")]
        public int? ParentId { get { return _position.SuperiorId; } set { _position.SuperiorId = value; } }
        public string ParentName { get { return _position.Position2?.PositionName; } }
        public Department Department { get { return _position.Department; } }
        public Position Position { get { return _position; } }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> PositionList { get; set; }
    }
}