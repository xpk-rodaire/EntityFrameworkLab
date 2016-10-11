using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects
{
    [Table("t_SecondLevelObjectBase", Schema = "Core")]
    public abstract class SecondLevelObjectBase
    {
        public SecondLevelObjectBase()
        {
        }

        public int SecondLevelObjectBaseId { get; set; }

        [MaxLength(10)]
        public string Identifier { get; set; } 

        [MaxLength(200)]
        public string SecondBase_Property1 { get; set; }
        [MaxLength(200)]
        public string SecondBase_Property2 { get; set; }
        [MaxLength(200)]
        public string SecondBase_Property3 { get; set; }

        [Required]
        public TopLevelObject Parent { get; set; }

        public virtual void PopulateTest()
        {
            this.SecondBase_Property1 = "SecondLevelObjectBase_Property1";
            this.SecondBase_Property2 = "SecondLevelObjectBase_Property2";
            this.SecondBase_Property3 = "SecondLevelObjectBase_Property3";
        }
    }
}
