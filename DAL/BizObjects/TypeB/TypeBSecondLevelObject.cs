using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFLab.DAL;

namespace EFLab.DAL.BizObjects.TypeB
{
    [SecondLevelObject(Value = SecondLevelObjectType.TypeB)]
    [Table("t_TypeBSecondLevel", Schema = "TypeB")]
    public class TypeBSecondLevelObject : EFLab.DAL.BizObjects.SecondLevelObjectBase
    {
        public TypeBSecondLevelObject()
        {
            this.TypeBObject1s = new List<TypeBObject1>();
            this.Identifier = "TypeB";
        }

        public string TypeBSecond_Property4 { get; set; }
        public string TypeBSecond_Property5 { get; set; }
        public string TypeBSecond_Property6 { get; set; }

        public virtual IList<TypeBObject1> TypeBObject1s { get; set; }

        public override void PopulateTest()
        {
            base.PopulateTest();

            var obj1 = new TypeBObject1();
            obj1.PopulateTest();
            this.TypeBObject1s.Add(obj1);

            this.TypeBSecond_Property4 = "TypeBSecond_Property4";
            this.TypeBSecond_Property5 = "TypeBSecond_Property5";
            this.TypeBSecond_Property6 = "TypeBSecond_Property6";
        }
    }
}
