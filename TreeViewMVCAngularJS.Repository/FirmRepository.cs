using System.Collections.Generic;
using System.Linq;
using TreeViewMVCAngularJS.Entity;
using TreeViewMVCAngularJS.Repository.DbManager;

namespace TreeViewMVCAngularJS.Repository
{
    public class FirmRepository
    {
        private static MyTreeViewDBEntities _db;
        public FirmRepository()
        {
            _db = DbConnection.GetSQLConnection();
        }
        public List<Firm> GetAll()
        {
            return _db.Firm.ToList();
        }
    }
}
