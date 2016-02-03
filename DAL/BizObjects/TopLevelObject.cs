﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL.BizObjects
{
    public class TopLevelObject
    {
        public TopLevelObject()
        {
            this.SecondLevelObjects = new List<SecondLevelObjectBase>();
        }

        public int TopLevelObjectId { get; set; }

        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }

        public virtual IList<SecondLevelObjectBase> SecondLevelObjects { get; set; }
    }
}
