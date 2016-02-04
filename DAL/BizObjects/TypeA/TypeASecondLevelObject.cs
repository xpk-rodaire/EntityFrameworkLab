using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects.TypeA
{
    public class TypeASecondLevelObject : EFLab.DAL.BizObjects.SecondLevelObjectBase
    {
        public TypeASecondLevelObject()
        {
            this.TypeAObject1s = new List<TypeAObject1>();
        }

        public int SecondLevelObjectId { get; set; }

        public string TypeASecond_Property4 { get; set; }
        public string TypeASecond_Property5 { get; set; }
        public string TypeASecond_Property6 { get; set; }

        public virtual IList<TypeAObject1> TypeAObject1s { get; set; }

        public override void PopulateTest()
        {
            base.PopulateTest();

            var obj1 = new TypeAObject1();
            obj1.PopulateTest();
            this.TypeAObject1s.Add(obj1);

            this.TypeASecond_Property4 = "TypeASecond_Property4";
            this.TypeASecond_Property5 = "TypeASecond_Property5";
            this.TypeASecond_Property6 = "TypeASecond_Property6";
        }
    }
}
