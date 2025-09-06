using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.Presentation.Extensions
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Source object cannot be null.");
            }

            var dictionary = new Dictionary<string, object>();

            // Get all public instance properties of the object's type
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                // Get the property name and value
                string propertyName = property.Name;
                object propertyValue = property.GetValue(obj, null);

                // Add to the dictionary
                dictionary[propertyName] = propertyValue;
            }

            return dictionary;
        }
    }
}
