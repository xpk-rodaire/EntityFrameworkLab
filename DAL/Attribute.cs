using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLab.DAL
{
    public enum SecondLevelObjectType
    {
        TypeA,
        TypeB,
        TypeC
    } 

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SecondLevelObjectAttribute : Attribute
    {
        public SecondLevelObjectType Value { get; set; }
    }
}
