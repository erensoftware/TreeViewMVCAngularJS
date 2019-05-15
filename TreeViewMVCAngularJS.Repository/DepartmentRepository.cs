using System.Collections.Generic;
using System.Linq;
using TreeViewMVCAngularJS.Entity;
using TreeViewMVCAngularJS.Repository.DbManager;

namespace TreeViewMVCAngularJS.Repository
{
    public class DepartmentRepository
    {
        private static MyTreeViewDBEntities _db;
        public DepartmentRepository()
        {
            _db = DbConnection.GetSQLConnection();
        }
        public List<Department> GetAll()
        {
            return _db.Department.ToList();
        }
        public List<Department> GetAllByMasterId(int firmId)
        {
            return _db.Department.Where(t=>t.FirmId == firmId).ToList();
        }
    }
}
