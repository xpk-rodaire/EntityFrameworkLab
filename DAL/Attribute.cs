using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL
{
    public enum CustomType
    {
        TypeA,
        TypeB,
        TypeC
    }

    public class SecondLevelObjectAttribute : Attribute
    {
    }

    public class CustomTypeAttribute : Attribute
    {
        public CustomType Value { get; set; }
    }
}
