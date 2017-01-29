using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Tools.Reflection
{
    public class PropertyInfoEnumHelper<T>
         where T : struct, IComparable, IConvertible, IFormattable
    {
        private Dictionary<T, List<IPropertyAccessor>> _fields = new Dictionary<T, List<IPropertyAccessor>>();

        public void Add(T field, Type objectType, string propertyName)
        {
            _fields.Add(field, CreatePropertyAccessors(objectType, propertyName));
        }

        private List<IPropertyAccessor> CreatePropertyAccessors(Type objectType, string propertyName)
        {
            var accessors = new List<IPropertyAccessor>();

            Type parentType = objectType;

            foreach (string property in propertyName.Split('.'))
            {
                PropertyInfo propertyInfo = parentType.GetProperty(property);
                accessors.Add(PropertyInfoHelper.CreateAccessor(propertyInfo));
                parentType = propertyInfo.PropertyType;
            }

            return accessors;
        }

        public object GetValue(object obj, T field)
        {
            return _GetValue(obj, _fields[field]);
        }

        //
        // Form1095CUpstreamDetailType record = this.GetF1095CRecord(lineNumber);
        // OtherCompletePersonNameType name = record.EmployeeInfoGrp.OtherCompletePersonName;
        // values.Add(Mark4FileProcesser.Mark4FileRecord1095CField.EEFirstName, name.PersonFirstNm);
        //
        // GetValue(record, Mark4FileProcesser.Mark4FileRecord1095CField.EEFirstName, values)
        public object GetValue(object obj, T field, Dictionary<T, string> values)
        {
            object result = GetValue(obj, field);
            values.Add(field, result.ToString());
            return result;
        }

        public void SetValue(object obj, object value, T field)
        {
            _SetValue(obj, value, _fields[field]);
        }

        //
        // f1095C.EmployeeInfoGrp.OtherCompletePersonName.PersonFirstNm = values[Mark4FileProcesser.Mark4FileRecord1095CField.EEFirstName];
        //
        // SetValue(f1095C, Mark4FileProcesser.Mark4FileRecord1095CField.EEFirstName, values)
        //
        public bool SetValue(object obj, T field, Dictionary<T, string> values)
        {
            List<IPropertyAccessor> accessor = this._fields[field];
            Type propertyType = accessor.Last().PropertyInfo.PropertyType;
            string value = values[field];

            if (propertyType == typeof(string))
            {
                SetValue(obj, value, field);
                return true;
            }
            else if (propertyType == typeof(int))
            {
                int parsedValue;
                bool parsed = int.TryParse(value, out parsedValue);

                if (parsed)
                {
                    SetValue(obj, parsedValue, field);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (propertyType == typeof(bool))
            {
                bool parsedValue = (value.Equals("YES") || value.Equals("X"));
                SetValue(obj, parsedValue, field);
                return true;
            }
            else if (propertyType == typeof(DateTime))
            {
                DateTime parsedValue;
                bool parsed = DateTime.TryParse(value, out parsedValue);
                if (parsed)
                {
                    SetValue(obj, parsedValue, field);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (propertyType == typeof(Decimal))
            {
                Decimal parsedValue;
                bool parsed = Decimal.TryParse(value, out parsedValue);
                if (parsed)
                {
                    SetValue(obj, parsedValue, field);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ApplicationException("Invalid type: " + propertyType.ToString());
            }
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
