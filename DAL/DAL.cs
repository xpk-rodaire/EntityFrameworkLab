using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Reflection;
using System.Diagnostics;

//using System.Linq.Dynamic;

// System.Data.Objects.SQLClient.SqlFunctions

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
                    (from t in context.Set<EFLab.DAL.BizObjects.TopLevelObject>()
                     join s in context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>().OfType<T>()
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     where t.TopLevelObjectId == topLevelId
                     //&& SqlFunctions.IsNumeric()
                     select s)
                     .ToList();

                return objects;
            }
        }

        public IList<EFLab.DAL.BizObjects.SecondLevelObjectBase> GetSecondLevelObject2(int topLevelId)
        {
            using (DbEntities context = new DbEntities())
            {
                var objects =
                    context.Set<EFLab.DAL.BizObjects.TopLevelObject>()
                    .Join(
                        context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>(),
                        t => t.TopLevelObjectId,
                        s => s.Parent.TopLevelObjectId,
                        (t, s) => s
                    )
                    .Where(s => s.Parent.TopLevelObjectId == topLevelId)
                    .ToList();

                return objects;
            }
        }

        public IList<T> GetSecondLevelObject3<T>(int topLevelId)
            where T : EFLab.DAL.BizObjects.SecondLevelObjectBase
        {
            using (DbEntities context = new DbEntities())
            {
                IList<T> objects =
                    context.Set<EFLab.DAL.BizObjects.TopLevelObject>()
                    .Join(
                        context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>().OfType<T>(),
                        t => t.TopLevelObjectId,
                        s => s.Parent.TopLevelObjectId,
                        (t, s) => s
                    )
                    .Where(s => s.Parent.TopLevelObjectId == topLevelId)
                    .ToList<T>();

                return objects;
            }
        }

        public IList<T> GetSecondLevelObject<T>(int topLevelId, string[] includes)
            where T : EFLab.DAL.BizObjects.SecondLevelObjectBase
        {
            using (DbEntities context = new DbEntities())
            {
                EFLab.DAL.BizObjects.TopLevelObject tlo = new BizObjects.TopLevelObject();

                // http://stackoverflow.com/questions/26506619/linq-on-dbcontext-set

                var query =
                    (from t in context.Set<EFLab.DAL.BizObjects.TopLevelObject>()
                     join s in context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>().OfType<T>()
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
                    (from t in context.Set<EFLab.DAL.BizObjects.TopLevelObject>()
                     join s in context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>()
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     select s)
                     .AsQueryable();

                return query
                    .Where(t => t.Parent.TopLevelObjectId.Equals(topLevelId))
                    .ToList();
            }
        }

        public void DeleteSecondLevelObject(int id)
        {
            using (DbEntities context = new DbEntities())
            {
                var obj = context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>().Find(id);
                context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>().Remove(obj);
                context.SaveChanges();
            }
        }

        public IList<EFLab.DAL.BizObjects.SecondLevelObjectBase> GetSecondLevelObject(int topLevelId, SecondLevelObjectType type)
        {
            using (DbEntities context = new DbEntities())
            {
                return
                    (from t in context.Set<EFLab.DAL.BizObjects.TopLevelObject>()
                     join s in context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>().OfType<EFLab.DAL.BizObjects.SecondLevelObjectBase>()
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId

                     where t.TopLevelObjectId.Equals(topLevelId)
                     
                     //&& s.GetType().GetCustomAttributes(typeof(SecondLevelObjectAttribute), true) != null

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

        public IList<EFLab.DAL.BizObjects.TypeA.TypeAObject1> GetTypeAObject1()
        {
            using (DbEntities context = new DbEntities())
            {
                var objs = context.Set<EFLab.DAL.BizObjects.TypeA.TypeAObject1>().Find(1);
                return null;
            }
        }

        public List<PropertyInfo> GetDbSetProperties()
        {
            using (DbEntities context = new DbEntities())
            {
                var dbSetProperties = new List<PropertyInfo>();
                var properties = context.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var setType = property.PropertyType;

                    bool isDbSet = false;

                    //#if EF5 || EF6
                    isDbSet = setType.IsGenericType && (typeof(IDbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition()) || setType.GetInterface(typeof(IDbSet<>).FullName) != null);
                    //#elif EF7
                    //    isDbSet = setType.IsGenericType && (typeof (DbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition()));
                    //#endif

                    if (isDbSet)
                    {
                        dbSetProperties.Add(property);
                    }
                }
                return dbSetProperties;
            }
        }
    }
}
