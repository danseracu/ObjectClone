using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;

namespace ObjectClone
{
    public static class ObjectCloner
    {
        /// <summary>
        /// Deep clones an object recursing into all it's properties
        /// </summary>
        /// <typeparam name="T">The type of the object to clone</typeparam>
        /// <param name="source">The object to clone</param>
        /// <returns>The cloned object</returns>
        public static T DeepClone<T>(this T source)
        {
            var sourceType = typeof(T);

            //Some validations
            if(sourceType.IsPrimitive || sourceType == typeof(string))
            {
                return source;
            }

            if(typeof(IEnumerable).IsAssignableFrom(sourceType))
            { 
                throw new InvalidOperationException("Attempting to clone an enumerable. Please use DeepCloneList method");
            }

            return Clone(source);
        }

        /// <summary>
        /// Deep clones a collection of objects and all of its elements
        /// </summary>
        /// <typeparam name="T">The type of the objects the collection holds</typeparam>
        /// <typeparam name="U">The type of the collection</typeparam>
        /// <param name="source">The collection to clone</param>
        /// <returns>The cloned collection</returns>
        public static U DeepCloneList<T, U>(this IEnumerable<T> source) where U : ICollection<T>, new()
        {
            var result = new U();

            foreach (var item in source as IEnumerable)
            {
                result.Add(Clone((T)item));
            }

            return result;
        }

        private static T Clone<T>(T source)
        {
            if (source == null)
                return default(T);

            var type = source.GetType();

            //Only get the properties we can assign to
            var properties = type.GetProperties().Where(p => p.CanWrite);

            //Initialise the cloned object
            var result = (T)FormatterServices.GetUninitializedObject(type);

            //Iterate the properties of the original object
            foreach (var prop in properties)
            {
                //Get the value of the property from the original object
                var value = prop.GetValue(source);

                //If it's derived from an IEnumerable (e.g. list or array) clone it using the clone list method and set it to the result object
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    prop.SetValue(result, InvokeCloneList(value, prop));
                }
                //If it's a primitive set it to the result object
                else if (!prop.PropertyType.IsClass || prop.PropertyType == typeof(string))
                {
                    prop.SetValue(result, value);
                }
                //If it's a complex object recurse into it the set it to the result object
                else
                {
                    prop.SetValue(result, InvokeClone(value));
                }
            }

            return result;
        }

        //Get the type of the objects in an IEnumerable
        private static List<Type> GetGenericIEnumerables(object o)
        {
            return o.GetType()
                    .GetInterfaces()
                    .Where(t => t.IsGenericType == true
                        && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    .Select(t => t.GetGenericArguments()[0]).ToList();
        }

        //Invokes the Clone<T> method using the type of the value object as the type parameter
        private static object InvokeClone(object value)
        {
            return typeof(ObjectCloner).GetMethod("Clone", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance).MakeGenericMethod(value.GetType()).Invoke(null, new object[] { value });
        }

        //Invokes the DeepCloneList<T, U> method using the type of collection that value is, and the type of the objects contained in the value collection
        private static object InvokeCloneList(object value, PropertyInfo prop)
        {
            var function = typeof(ObjectCloner).GetMethod("DeepCloneList", BindingFlags.Public | BindingFlags.Static).MakeGenericMethod(new Type[] { GetGenericIEnumerables(value)[0], prop.PropertyType });
            return function.Invoke(null, new object[] { value });
        }
    }
}
