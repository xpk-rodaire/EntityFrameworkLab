using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SCO.IRS.ACA.Utils
{
    public class PropertyAccessor
    {
        private Dictionary<string, IPropertyAccessor> _properties = new Dictionary<string, IPropertyAccessor>();

        public PropertyAccessor(Type type)
        {
            this.ObjectType = type;
        }

        public Type ObjectType {get; private set; }

        public List<IPropertyAccessor> GetCreateAccessors(object obj, string property)
        {
            if (!obj.GetType().Equals(this.ObjectType))
            {
                throw new ArgumentException(
                    String.Format(
                        "PropertyAccessor: Invalid object type - expected '{0}', received '{1}'.",
                        this.ObjectType.FullName,
                        obj.GetType().FullName
                    )
                );
            }

            var accessors = new List<IPropertyAccessor>();

            Type parentType = obj.GetType();

            foreach (string propertyPart in property.Split('.'))
            {
                IPropertyAccessor propertyPartAccessor = null;
                PropertyInfo propertyInfo = parentType.GetProperty(propertyPart);

                if (propertyInfo == null)
                {
                    throw new ArgumentException(
                        String.Format("PropertyAccessor.GetCreateAccessors(): invalid property - object '{0}' does not have property '{1}'.",
                            parentType.FullName,
                            propertyPart
                        )
                    );
                }

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

        public object GetValue(object obj, string property)
        {
            List<IPropertyAccessor> accessor = GetCreateAccessors(obj, property);
            return _GetValue(obj, accessor);
        }

        public void SetValue(object obj, string property, object value)
        {
            List<IPropertyAccessor> accessor = GetCreateAccessors(obj, property);
            _SetValue(obj, value, accessor);
        }

        private object _GetValue(object obj, List<IPropertyAccessor> accessors)
        {
            object result = obj;
            foreach (IPropertyAccessor accessor in accessors)
            {
                result = accessor.GetValue(result);
            }
            return result;
        }

        private void _SetValue(object obj, object value, List<IPropertyAccessor> accessors)
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
    public static bool GetValue<FieldEnum>(
        this SCO.IRS.ACA.Utils.PropertyAccessor accessor,
        object obj,
        string property,
        Dictionary<FieldEnum, string> values,
        FieldEnum field
    )
        where FieldEnum : struct, IComparable, IConvertible, IFormattable
    {
        object value = accessor.GetValue(obj, property);
        if (value != null)
        {
            values.Add(field, accessor.GetValue(obj, property).ToString());
            return true;
        }
        return false;
    }

    public static bool SetValue<FieldEnum>(
        this SCO.IRS.ACA.Utils.PropertyAccessor accessor,
        object obj,
        string property,
        Dictionary<FieldEnum, string> values,
        FieldEnum field
    )
        where FieldEnum : struct, IComparable, IConvertible, IFormattable
    {
        string value = null;
        if (!values.TryGetValue(field, out value))
        {
            return false;
        }

        List<SCO.IRS.ACA.Utils.IPropertyAccessor> accessors = accessor.GetCreateAccessors(obj, property);
        Type propertyType = accessors.Last().PropertyInfo.PropertyType;

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