using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects.TypeA
{
    [Table("t_Object1", Schema = "TypeA")]
    public class TypeAObject1
    {
        public TypeAObject1()
        {
        }

        public int TypeAObject1Id { get; set; }

        [Required]
        public TypeASecondLevelObject TypeASecondLevelObject { get; set; }

        [MaxLength(200)]
        public string TypeAObject1_Property1 { get; set; }
        public string TypeAObject1_Property2 { get; set; }
        public string TypeAObject1_Property3 { get; set; }

        public void PopulateTest()
        {
            this.TypeAObject1_Property1 = "TypeAObject1_Property1";
            this.TypeAObject1_Property2 = "TypeAObject1_Property2";
            this.TypeAObject1_Property3 = "TypeAObject1_Property3";
        }
    }
}
