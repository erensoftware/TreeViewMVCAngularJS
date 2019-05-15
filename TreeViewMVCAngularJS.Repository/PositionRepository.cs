using System;
using System.Collections.Generic;
using System.Linq;
using TreeViewMVCAngularJS.Entity;
using TreeViewMVCAngularJS.Repository.DbManager;

namespace TreeViewMVCAngularJS.Repository
{
    public class PositionRepository
    {
        private static MyTreeViewDBEntities _db;
        public PositionRepository()
        {
            _db = DbConnection.GetSQLConnection();
        }
        public List<Position> GetAll()
        {
            return _db.Position.ToList();
        }
        public List<Position> GetAllByMasterId(int firmId)
        {
            return _db.Position.Where(t=>t.Department.FirmId == firmId).ToList();
        }
        public List<Position> GetAllTopPositions()
        {
            return _db.Position.Where(t => t.SuperiorId == null).ToList();
        }
        public Position GetSingle(int id)
        {
            var entity = _db.Position.SingleOrDefault(t => t.PositionId == id);
            return entity;
        }
        public Position Insert(Position model)
        {
            try
            {
                var added = _db.Position.Add(model);
                _db.SaveChanges();
                return added;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int Update(Position model)
        {
            var entity = _db.Position.SingleOrDefault(t => t.PositionId == model.PositionId);
            entity.DepartmentId = model.DepartmentId;
            entity.IsActive = model.IsActive;
            entity.PositionName = model.PositionName;
            entity.SuperiorId = model.SuperiorId;
            return _db.SaveChanges();
        }
        public int Delete(int id)
        {
            var entity = _db.Position.SingleOrDefault(t => t.PositionId == id);
            _db.Position.Remove(entity);
            return _db.SaveChanges();
        }
    }
}
