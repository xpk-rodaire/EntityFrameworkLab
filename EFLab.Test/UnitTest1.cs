using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using EFLab.DAL;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

using EFLab.DAL.BizObjects;
using EFLab.DAL.BizObjects.TypeA;
using EFLab.DAL.BizObjects.TypeB;
using EFLab.DAL.BizObjects.TypeC;
using System.Reflection;


namespace EFLab.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (DbEntities context = new DbEntities())
            {
                TopLevelObject top = new TopLevelObject();
                top.PopulateTest();
                context.TopLevelObject.Add(top);
                context.SaveChanges();

                IEnumerable<SecondLevelObjectBase> objects =
                    (from t in context.TopLevelObject
                     join s in context.SecondLevelObject
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     where t.TopLevelObjectId == top.TopLevelObjectId
                     select s).ToList();

                Assert.AreEqual(objects.Count(), 3);

                Assert.AreEqual(objects.OfType<TypeASecondLevelObject>().Count(), 1);
                Assert.AreEqual(objects.OfType<TypeBSecondLevelObject>().Count(), 1);
                Assert.AreEqual(objects.OfType<TypeCSecondLevelObject>().Count(), 1);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            EFLab.DAL.DAL dal = new EFLab.DAL.DAL();

            IList<TypeASecondLevelObject> typeASecondObjs = dal.GetSecondLevelObject<TypeASecondLevelObject>(1, new string[] { "TypeAObject1s" });
            Assert.AreEqual(1, typeASecondObjs.Count());
            Assert.AreEqual(1, typeASecondObjs[0].TypeAObject1s.Count());

            IList<TypeBSecondLevelObject> typeBSecondObjs = dal.GetSecondLevelObject<TypeBSecondLevelObject>(1, new string[] { "TypeBObject1s" });
            Assert.AreEqual(1, typeBSecondObjs.Count());
            Assert.AreEqual(1, typeBSecondObjs[0].TypeBObject1s.Count());

            IList<SecondLevelObjectBase> secondObjs = dal.GetSecondLevelObject(1);
            Assert.AreEqual(3, secondObjs.Count());

            //Assert.AreEqual(1, dal.GetSecondLevelObject(1, CustomType.TypeA).Count());
            //Assert.AreEqual(1, dal.GetSecondLevelObject(1, CustomType.TypeB).Count());
            //Assert.AreEqual(1, dal.GetSecondLevelObject(1, CustomType.TypeC).Count());

            Assert.AreEqual(3, dal.GetSecondLevelObject().Count());
        }

        [TestMethod]
        public void TestCustomAttributes()
        {
            Assembly clientAssembly = Assembly.Load("DAL");

            var typesWithMyAttribute =
                (from t in clientAssembly.GetTypes()
                 let attribute = t.GetCustomAttributes(typeof(SecondLevelObjectAttribute), true).FirstOrDefault()
                 where attribute != null
                 select new {
                     Type = t,
                     Attribute = (SecondLevelObjectAttribute)attribute,
                     Value = ((SecondLevelObjectAttribute)attribute).Value
                 }
                 ).ToList();

            foreach(var att in typesWithMyAttribute)
            {
                Debug.WriteLine(att);
            }
        }
    }
}
