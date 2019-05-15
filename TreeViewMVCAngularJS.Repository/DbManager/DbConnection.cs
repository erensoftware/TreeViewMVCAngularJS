using System;
using TreeViewMVCAngularJS.Entity;

namespace TreeViewMVCAngularJS.Repository.DbManager
{
    /// <summary>
    /// Static Singleton Db Connection Manager class.
    /// For every Db connection, add another static get method.
    /// Since the connection providers cannot be abstracted, this cannot become a factory pattern.
    /// </summary>
    public static class DbConnection
    {
        //lazy is by definition thread-safe: implicitly uses LazyThreadSafetyMode.ExecutionAndPublication /*.Net >= 4*/
        private static readonly Lazy<MyTreeViewDBEntities> _db
            = new Lazy<MyTreeViewDBEntities>(() => new MyTreeViewDBEntities());

        public static MyTreeViewDBEntities GetSQLConnection()
        {
            return _db.Value;
        }
    }
}
