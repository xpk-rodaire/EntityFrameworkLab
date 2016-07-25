using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

using EFLab.DAL.BizObjects;

namespace EFLab.DAL
{
    public class DAL
    {
        public IList<T> GetSecondLevelObject<T>(int topLevelId)
            where T : SecondLevelObjectBase
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
            where T : SecondLevelObjectBase
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

        public IList<EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject> GetTypeBSecondLevelObject(int topLevelId)
        {
            return GetSecondLevelObject<EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject>(topLevelId);
        }   
    }
}
