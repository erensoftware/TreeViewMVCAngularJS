using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeViewMVCAngularJS.Models
{
    public class TreeViewModel
    {
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public string ParentName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<TreeViewModel> Children { get; set; }
    }
}