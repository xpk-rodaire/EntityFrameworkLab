using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects.TypeC
{
    [Table("t_Object1", Schema = "TypeC")]
    public class TypeCObject1
    {
        public TypeCObject1()
        {
        }

        public int TypeCObject1Id { get; set; }

        [Required]
        public TypeCSecondLevelObject TypeCSecondLevelObject { get; set; }

        public string TypeCObject1_Property1 { get; set; }
        public string TypeCObject1_Property2 { get; set; }
        public string TypeCObject1_Property3 { get; set; }

        public void PopulateTest()
        {
            this.TypeCObject1_Property1 = "TypeCObject1_Property1";
            this.TypeCObject1_Property2 = "TypeCObject1_Property2";
            this.TypeCObject1_Property3 = "TypeCObject1_Property3";
        }
    }
}
