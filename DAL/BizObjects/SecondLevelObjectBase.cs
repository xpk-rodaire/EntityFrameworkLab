using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects
{
    public abstract class SecondLevelObjectBase
    {
        public int SecondLevelObjectBaseId { get; set; }

        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }

        public TopLevelObject Parent { get; set; }
    }
}
