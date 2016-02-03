using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects.TypeB
{
    public class TypeBSecondLevelObject : DAL.BizObjects.SecondLevelObjectBase
    {
        public int SecondLevelObjectId { get; set; }

        public string Property4 { get; set; }
        public string Property5 { get; set; }
        public string Property6 { get; set; }

        public virtual IList<TypeBObject1> TypeBObject1s { get; set; }
    }
}
