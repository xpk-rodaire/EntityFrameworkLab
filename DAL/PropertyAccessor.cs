using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SCO.IRS.ACA.Utils
{
    public class PropertyAccessor<BaseType, FieldEnum>
        where BaseType : class
        where FieldEnum : struct, IComparable, IConvertible, IFormattable
    {
        private Dictionary<string, IPropertyAccessor> _properties = new Dictionary<string, IPropertyAccessor>();

        public List<IPropertyAccessor> GetCreateAccessors(BaseType obj, string property)
        {
            var accessors = new List<IPropertyAccessor>();

            Type parentType = obj.GetType();

            foreach (string propertyPart in property.Split('.'))
            {
                IPropertyAccessor propertyPartAccessor = null;
                PropertyInfo propertyInfo = parentType.GetProperty(propertyPart);

                if (this._properties.TryGetValue(parentType.FullName + "." + propertyPart, out propertyPartAccessor))
                {
                    accessors.Add(propertyPartAccessor);
                }
                else
                {
                    IPropertyAccessor accessor = PropertyInfoHelper.CreateAccessor(propertyInfo);
                    accessors.Add(accessor);
                    this._properties.Add(parentType.FullName + "." + propertyPart, accessor);
                }

                parentType = propertyInfo.PropertyType;
            }

            return accessors;
        }

        public object GetValue(BaseType obj, string property)
        {
            List<IPropertyAccessor> accessor = GetCreateAccessors(obj, property);
            return _GetValue(obj, accessor);
        }

        public void SetValue(BaseType obj, string property, object value)
        {
            List<IPropertyAccessor> accessor = GetCreateAccessors(obj, property);
            _SetValue(obj, value, accessor);
        }

        private object _GetValue(BaseType obj, List<IPropertyAccessor> accessors)
        {
            object result = obj;
            foreach (IPropertyAccessor accessor in accessors)
            {
                result = accessor.GetValue(result);
            }
            return result;
        }

        private void _SetValue(BaseType obj, object value, List<IPropertyAccessor> accessors)
        {
            object _obj = obj;

            // Need second-to-last object and last accessor
            foreach (IPropertyAccessor accessor in accessors.Take(accessors.Count - 1))
            {
                _obj = accessor.GetValue(_obj);
            }

            accessors[accessors.Count - 1].SetValue(_obj, value);
        }
    }
}

public static class PropertyAccessorStringExtension
{
    public static object GetValue<BaseType, FieldEnum>(
        this SCO.IRS.ACA.Utils.PropertyAccessor<BaseType, FieldEnum> accessor,
        BaseType obj,
        string property,
        Dictionary<FieldEnum, string> values,
        FieldEnum field
    )
        where BaseType : class
        where FieldEnum : struct, IComparable, IConvertible, IFormattable
    {
        return accessor.GetValue(obj, property, values, field);
    }

    public static bool SetValue<BaseType, FieldEnum>(
        this SCO.IRS.ACA.Utils.PropertyAccessor<BaseType, FieldEnum> accessor,
        BaseType obj,
        string property,
        Dictionary<FieldEnum, string> values,
        FieldEnum field
    )
        where BaseType : class
        where FieldEnum : struct, IComparable, IConvertible, IFormattable
    {
        if (values[field] == null)
        {
            return false;
        }

        List<SCO.IRS.ACA.Utils.IPropertyAccessor> accessors = accessor.GetCreateAccessors(obj, property);
        Type propertyType = accessors.Last().PropertyInfo.PropertyType;
        string value = values[field];

        if (propertyType == typeof(string))
        {
            if (value.Trim().Length > 0)
            {
                accessor.SetValue(obj, property, value);
                return true;
            }
        }
        else if (propertyType == typeof(int))
        {
            int parsedValue;
            bool parsed = int.TryParse(value, out parsedValue);

            if (parsed)
            {
                accessor.SetValue(obj, property, parsedValue);
                return true;
            }
        }
        else if (propertyType == typeof(bool))
        {
            bool parsedValue = (value.Equals("YES") || value.Equals("X"));
            accessor.SetValue(obj, property, parsedValue);
            return true;
        }
        else if (propertyType == typeof(DateTime))
        {
            DateTime parsedValue;
            bool parsed = DateTime.TryParse(value, out parsedValue);
            if (parsed)
            {
                accessor.SetValue(obj, property, parsedValue);
                return true;
            }
        }
        else if (propertyType == typeof(Decimal))
        {
            Decimal parsedValue;
            bool parsed = Decimal.TryParse(value, out parsedValue);
            if (parsed)
            {
                accessor.SetValue(obj, property, parsedValue);
                return true;
            }
        }
        else
        {
            throw new ApplicationException("SetValue() - invalid type: " + propertyType.ToString());
        }
        return false;
    }
}