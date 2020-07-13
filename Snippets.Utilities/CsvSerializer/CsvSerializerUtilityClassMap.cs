using CsvHelper.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace Snippets.Utilities.CsvSerializer
{
    public class CsvSerializerUtilityClassMap<TModel> : ClassMap<TModel>
    {
        private void MapHeaderName(PropertyInfo property, string name)
        {
            var memberMap = Map(typeof(TModel), property);
            memberMap.GetType().GetMethod("Name")?.Invoke(memberMap, new object[] { new[] { name } });
        }

        private void MapColumnNumber(PropertyInfo property, int columnNumber)
        {
            var memberMap = Map(typeof(TModel), property);
            memberMap.GetType().GetMethod("Index")?.Invoke(memberMap, new object[] { columnNumber - 1, -1 });
        }

        public CsvSerializerUtilityClassMap()
        {
            foreach(var property in typeof(TModel).GetProperties())
            {
                var attr = property.GetCustomAttribute<CsvSerializerHeaderAttribute>();
                
                if(attr == null)
                {
                    MapHeaderName(property, property.Name);
                }
                
                else if(attr.ColumnNumber > 0)
                {
                    MapColumnNumber(property, attr.ColumnNumber);
                }

                else if(!String.IsNullOrEmpty(attr.HeaderName))
                {
                    MapHeaderName(property, attr.HeaderName);
                }

                if(property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    var memberMap = Map(typeof(TModel), property);
                    memberMap.GetType()
                        .GetMethods()
                        .FirstOrDefault(x => x.Name == "TypeConverter" && x.IsGenericMethod)?
                        .MakeGenericMethod(typeof(CsvSerializerUtilityDateTimeConverter))
                        .Invoke(memberMap, null);
                }
            }
        }
    }
}
