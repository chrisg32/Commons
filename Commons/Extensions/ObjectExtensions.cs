using System;
using System.Collections.Generic;
using System.Linq;

namespace CG.Commons.Extensions
{
    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object obj, string property)
        {
            try
            {
                var propertyTree = new Queue<string>(property.Split('.'));
                while (propertyTree.Any())
                {
                    if (obj == null) return null;
                    var propName = propertyTree.Dequeue();
                    obj = obj.GetType().GetProperty(propName)?.GetValue(obj);
                }
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
