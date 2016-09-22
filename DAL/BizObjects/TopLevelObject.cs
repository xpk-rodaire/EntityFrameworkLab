using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects
{
    [Table("t_TopLevelObject", Schema = "Core")]
    public class TopLevelObject
    {
        public TopLevelObject()
        {
            this.SecondLevelObjects = new List<SecondLevelObjectBase>();
        }

        public int TopLevelObjectId { get; set; }

        public string TopLevel_Property1 { get; set; }
        public string TopLevel_Property2 { get; set; }
        public string TopLevel_Property3 { get; set; }

        public virtual IList<SecondLevelObjectBase> SecondLevelObjects { get; set; }

        public void PopulateTest()
        {
            this.TopLevel_Property1 = "TopLevel_Property1";
            this.TopLevel_Property2 = "TopLevel_Property2";
            this.TopLevel_Property3 = "TopLevel_Property3";

            this.AddSecondLevelObject<EFLab.DAL.BizObjects.TypeA.TypeASecondLevelObject>();
            this.AddSecondLevelObject<EFLab.DAL.BizObjects.TypeB.TypeBSecondLevelObject>();
            this.AddSecondLevelObject<EFLab.DAL.BizObjects.TypeC.TypeCSecondLevelObject>();
        }

        public void AddSecondLevelObject<T>()
            where T: SecondLevelObjectBase, new()
        {
            T newObject = new T();
            newObject.PopulateTest();
            this.SecondLevelObjects.Add(newObject);
        }
    }
}
