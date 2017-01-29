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
using System.Data.Entity.Validation;
using Tools.Reflection;


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
                context.Set<EFLab.DAL.BizObjects.TopLevelObject>().Add(top);

                IEnumerable<DbEntityValidationResult> errors = context.GetValidationErrors();

                context.SaveChanges();

                IEnumerable<SecondLevelObjectBase> objects =
                    (from t in context.Set<EFLab.DAL.BizObjects.TopLevelObject>()
                     join s in context.Set<EFLab.DAL.BizObjects.SecondLevelObjectBase>()
                     on t.TopLevelObjectId equals s.Parent.TopLevelObjectId
                     where t.TopLevelObjectId == top.TopLevelObjectId
                     select s).ToList();

                Assert.AreEqual(objects.Count(), 3);

                EFLab.DAL.BizObjects.TypeA.TypeAObject1 obj =
                    (from t in context.Set<EFLab.DAL.BizObjects.TypeA.TypeAObject1>()
                     select t).FirstOrDefault();
                Assert.IsNotNull(obj);

                EFLab.DAL.BizObjects.TypeA.TypeAObject1 obj2 =
                    (from t in context.Set(Type.GetType("EFLab.DAL.BizObjects.TypeA.TypeAObject1,DAL")).Cast<EFLab.DAL.BizObjects.TypeA.TypeAObject1>()
                     select t).FirstOrDefault();

                Assert.IsNotNull(obj2);

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
            Assembly clientAssembly = Assembly.Load("DAL"); // NOT EFLab.DAL

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

        [TestMethod]
        public void TestDelete()
        {
            EFLab.DAL.DAL dal = new EFLab.DAL.DAL();
            dal.DeleteSecondLevelObject(1);
        }

        [TestMethod]
        public void TestGetTypeAObject1()
        {
            EFLab.DAL.DAL dal = new EFLab.DAL.DAL();
            dal.GetTypeAObject1();
        }

        [TestMethod]
        public void TestGetSecondLevelObject2()
        {
            EFLab.DAL.DAL dal = new EFLab.DAL.DAL();
            IList<EFLab.DAL.BizObjects.SecondLevelObjectBase> objs = dal.GetSecondLevelObject2(1);
        }

        [TestMethod]
        public void TestGetSecondLevelObject3()
        {
            EFLab.DAL.DAL dal = new EFLab.DAL.DAL();
            IList<TypeASecondLevelObject> objs = dal.GetSecondLevelObject3<TypeASecondLevelObject>(1);
        }

        [TestMethod]
        public void OutputDbSets()
        {
            EFLab.DAL.DAL dal = new EFLab.DAL.DAL();
            List<PropertyInfo> info = dal.GetDbSetProperties();

            foreach (PropertyInfo pi in info)
            {
                Debug.WriteLine(pi);
            }
        }

        //// Column B = Full-Time Employee Count for ALE Member
        //if (Utils.Utils.GetIntValue(values, Mark4FileProcesser.Mark4FileRecord1094CField.ALEMonthlyInformation12MonthsB, out intValue)
        //    && intValue.Value > 0)
        //{
        //    f1094C.ALEMemberInformationGrp.YearlyALEMemberDetail.TotalEmployeeCnt = intValue.Value.ToString();
        //}
        //else
        //{
        //    f1094C.ALEMemberInformationGrp.YearlyALEMemberDetail.TotalEmployeeCnt = null;
        //}

        [TestMethod]
        public void TestPropertyInfoHelper()
        {
            TestClass2 e = new TestClass2();
            IPropertyAccessor[] Accessors = e.GetType().GetProperties()
                .Select(pi => PropertyInfoHelper.CreateAccessor(pi)).ToArray();

            foreach (var accessor in Accessors)
            {
                Type pt = accessor.PropertyInfo.PropertyType;
                if (pt == typeof(string))
                {
                    accessor.SetValue(e, Guid.NewGuid().ToString("n").Substring(0, 9));
                }
                else if (pt == typeof(int))
                {
                    accessor.SetValue(e, new Random().Next(0, int.MaxValue));
                }
                Console.WriteLine(string.Format("{0}:{1}", accessor.PropertyInfo.Name, accessor.GetValue(e)));
            }
        }

        public class TestClass1
        {
            public TestClass1()
            {
                Field1  = 1234;
                Field2  = "Field2";
                Field3  = "Field3";
                Field4 = 5678;
                Field5 = DateTime.Now;
                Class2 = new TestClass2();
            }

            public int Field1 { get; set; }
            public string Field2 { get; set; }
            public string Field3 { get; set; }
            public int Field4 { get; set; }
            public DateTime Field5 { get; set; }
            public TestClass2 Class2 { get; set; }
        }

        public class TestClass2
        {
            public TestClass2()
            {
                this.Id2 = 7895;
                this.FirstName2 = "FirstName2";
                this.LastName2 = "LastName2";
                this.Age2 = 456;
                Class3 = new TestClass3();
            }

            public int Id2 { get; set; }
            public string FirstName2 { get; set; }
            public string LastName2 { get; set; }
            public int Age2 { get; set; }
            public TestClass3 Class3 { get; set; }
        }

        public class TestClass3
        {
            public TestClass3()
            {
                this.Id3 = 7895;
                this.FirstName3 = "FirstName3";
                this.LastName3 = "LastName3";
                this.Age3 = 456;
            }

            public int Id3 { get; set; }
            public string FirstName3 { get; set; }
            public string LastName3 { get; set; }
            public int Age3 { get; set; }
        }

        public enum Field
        {
            EnumField1,
            EnumField2,
            EnumField3,
            EnumField4,
            EnumField5,
            EnumField6
        }

        [TestMethod]
        public void TestPropertyInfoHelper2()
        {
            //var values = (from field in Enum.GetValues(typeof(Field)).Cast<Field>()
            //              select new {Key = field, Value = field.ToString()})
            //              .ToDictionary(g => g.Key, g => g.Value);

            //PropertyInfoEnumHelper<Field> helper = new PropertyInfoEnumHelper<Field>();

            //Type t = Type.GetType("EFLab.Test.UnitTest1+TestClass1");

            //helper.Add(Field.EnumField1, t, "Field1");
            //helper.Add(Field.EnumField3, t, "Field3");
            //helper.Add(Field.EnumField5, t, "Class1.FirstName");

            //var obj = new TestClass1();

            //foreach (KeyValuePair<Field, string> entry in values)
            //{
            //    IPropertyAccessor accessor = properties[entry.Key];
            //    if (accessor != null)
            //    {
            //        Type pt = accessor.PropertyInfo.PropertyType;
            //        if (pt == typeof(string))
            //        {
            //            accessor.SetValue(obj, entry.Value);
            //        }
            //        else if (pt == typeof(int))
            //        {
            //            int parsedValue;
            //            bool parsed = int.TryParse(entry.Value, out parsedValue);

            //            if (parsed)
            //            {
            //                accessor.SetValue(obj, parsedValue);
            //            }
            //            else
            //            {
            //                throw new Exception();
            //            }
            //        }
            //        else if (pt == typeof(bool))
            //        {
            //            bool parsedValue = (entry.Value.Equals("YES") || entry.Value.Equals("X"));
            //            accessor.SetValue(obj, parsedValue);
            //        }
            //        else if (pt == typeof(DateTime))
            //        {
            //            DateTime parsedValue;
            //            bool parsed = DateTime.TryParse(entry.Value, out parsedValue);
            //            accessor.SetValue(obj, parsedValue);
            //        }
            //        else if (pt == typeof(Decimal))
            //        {
            //            Decimal parsedValue;
            //            bool parsed = Decimal.TryParse(entry.Value, out parsedValue);
            //            accessor.SetValue(obj, parsedValue);
            //        }
            //        else
            //        {
            //            throw new ApplicationException("Invalid type: " + pt.ToString());
            //        }
            //    }
            //}
        }

        [TestMethod]
        public void TestPropertyInfoHelper4()
        {
            PropertyInfoEnumHelper<Field> helper = new PropertyInfoEnumHelper<Field>();

            Type t = Type.GetType("EFLab.Test.UnitTest1+TestClass1");

            helper.Add(Field.EnumField1, t, "Field1");
            helper.Add(Field.EnumField2, t, "Class2.Class3.FirstName3");
            helper.Add(Field.EnumField3, t, "Class2.Class3.Age3");
        
            TestClass1 tc1 = new TestClass1();

            //TestGetSet(helper, tc1, 1234, 12345, Field.EnumField1);
            //TestGetSet(helper, tc1, "FirstName3", "NewFirstName", Field.EnumField2);
            //TestGetSet(helper, tc1, 456, 4568, Field.EnumField3);

            var values = new Dictionary<Field, string>();
            values.Add(Field.EnumField1, "472598");
            values.Add(Field.EnumField2, "Steve was here");
            values.Add(Field.EnumField3, "99837");

            TestGetSet2(helper, tc1, values, Field.EnumField1);
            TestGetSet2(helper, tc1, values, Field.EnumField2);
            TestGetSet2(helper, tc1, values, Field.EnumField3);
        }

        private void TestGetSet2(
            PropertyInfoEnumHelper<Field> helper,
            object obj,
            Dictionary<Field, string> values,
            Field field)
        {
            // Copy value from values into obj
            helper.SetValue(obj, field, values);
            // Verify value in obj matches
            Assert.AreEqual(values[field], helper.GetValue(obj, field).ToString());
        }

        private void TestGetSet(
            PropertyInfoEnumHelper<Field> helper,
            object obj,
            object value,
            object newValue,
            Field field)
        {
            object result = helper.GetValue(obj, field);
            Assert.AreEqual(result, value);

            helper.SetValue(obj, newValue, field);

            result = helper.GetValue(obj, field);
            Assert.AreEqual(result, newValue);
        }
    }
}
