using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace EFLab.DAL
{
    public class DAL
    {
        public IList<T> GetSecondLevelObject<T>(int topLevelId)
            where T : EFLab.DAL.BizObjects.SecondLevelObjectBase
        {
            using (DbEntities context = new DbEntities())
            {
                IList<T> objects =
                    (from t in context.TopLevelObject
                     join s in context.SecondLevelObject.OfType<T>()
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     where t.TopLevelObjectId == topLevelId
                     select s)
                     .ToList();

                return objects;
            }
        }

        public IList<T> GetSecondLevelObject<T>(int topLevelId, string[] includes)
            where T : EFLab.DAL.BizObjects.SecondLevelObjectBase
        {
            using (DbEntities context = new DbEntities())
            {
                var query =
                    (from t in context.TopLevelObject
                     join s in context.SecondLevelObject.OfType<T>()
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     select s)
                     .AsQueryable();

                foreach (string include in includes)
                {
                    query = query.Include(include);
                }

                return query
                    .Where(t => t.Parent.TopLevelObjectId.Equals(topLevelId))
                    .ToList();
            }
        }

        public IList<EFLab.DAL.BizObjects.SecondLevelObjectBase> GetSecondLevelObject(int topLevelId)
        {
            using (DbEntities context = new DbEntities())
            {
                var query =
                    (from t in context.TopLevelObject
                     join s in context.SecondLevelObject
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     select s)
                     .AsQueryable();

                return query
                    .Where(t => t.Parent.TopLevelObjectId.Equals(topLevelId))
                    .ToList();
            }
        }

        public IList<EFLab.DAL.BizObjects.SecondLevelObjectBase> GetSecondLevelObject(int topLevelId, SecondLevelObjectType type)
        {
            using (DbEntities context = new DbEntities())
            {
                return
                    (from t in context.TopLevelObject
                     join s in context.SecondLevelObject
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId



                     where t.TopLevelObjectId.Equals(topLevelId)
                     && s.GetType().GetCustomAttributes(typeof(SecondLevelObjectAttribute), true) != null

                     select s)
                     .ToList();
            }
        }

        public IList<EFLab.DAL.BizObjects.SecondLevelObjectBase> GetSecondLevelObject()
        {
            using (DbEntities context = new DbEntities())
            {
                DbSet<EFLab.DAL.BizObjects.SecondLevelObjectBase> set = context.GetSecondLevelObject();

                return
                    (from t in set
                     select t)
                    .ToList();
            }
        }   
    }
}
