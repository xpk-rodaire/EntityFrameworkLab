using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects.TypeB
{
    [Table("t_Object1", Schema = "TypeB")]
    public class TypeBObject1
    {
        public TypeBObject1()
        {
        }

        public int TypeBObject1Id { get; set; }

        [Required]
        public TypeBSecondLevelObject TypeBSecondLevelObject { get; set; }

        public string TypeBObject1_Property1 { get; set; }
        public string TypeBObject1_Property2 { get; set; }
        public string TypeBObject1_Property3 { get; set; }

        public void PopulateTest()
        {
            this.TypeBObject1_Property1 = "TypeBObject1_Property1";
            this.TypeBObject1_Property2 = "TypeBObject1_Property2";
            this.TypeBObject1_Property3 = "TypeBObject1_Property3";
        }
    }
}
