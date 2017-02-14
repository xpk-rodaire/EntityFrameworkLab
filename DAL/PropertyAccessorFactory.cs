using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCO.IRS.ACA.Utils
{
    public static class PropertyAccessorFactory
    {
        private static Dictionary<Type, PropertyAccessor> _accessors = new Dictionary<Type, PropertyAccessor>();

        public static PropertyAccessor GetPropertyAccessor(Type type)
        {
            PropertyAccessor accessor = null;
            if (!_accessors.TryGetValue(type, out accessor))
            {
                accessor = new PropertyAccessor(type);
                _accessors.Add(type, accessor);
            }
            return accessor;
        }
    }
}
