using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects.TypeB
{
    [Table("t_Object1", Schema = "TypeB")]
    public class TypeBObject1
    {
        public int TypeBObject1Id { get; set; }

        public string TypeBObject1_Property1 { get; set; }
        public string TypeBObject1_Property2 { get; set; }
        public string TypeBObject1_Property3 { get; set; }
    }
}
