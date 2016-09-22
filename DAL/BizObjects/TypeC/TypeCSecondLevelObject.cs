using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects.TypeC
{
    [Table("t_TypeCSecondLevel2", Schema = "TypeC2")]
    public class TypeCSecondLevelObject : EFLab.DAL.BizObjects.SecondLevelObjectBase
    {
        public TypeCSecondLevelObject()
        {
            this.TypeCObject1s = new List<TypeCObject1>();
            this.Identifier = "TypeC";
        }

        public string TypeCSecond_Property4 { get; set; }
        public string TypeCSecond_Property5 { get; set; }
        public string TypeCSecond_Property6 { get; set; }

        public virtual IList<TypeCObject1> TypeCObject1s { get; set; }

        public override void PopulateTest()
        {
            base.PopulateTest();

            var obj1 = new TypeCObject1();
            obj1.PopulateTest();
            this.TypeCObject1s.Add(obj1);

            this.TypeCSecond_Property4 = "TypeCSecond_Property4";
            this.TypeCSecond_Property5 = "TypeCSecond_Property5";
            this.TypeCSecond_Property6 = "TypeCSecond_Property6";
        }
    }
}