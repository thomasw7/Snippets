using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Reflection;
using MemberTypes = System.Reflection.MemberTypes;

namespace Snippets.Utilities.CsvSerializer
{
    public class CsvSerializerUtilityDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            try
            {
                return base.ConvertFromString(text, row, memberMapData);
            }
            catch(Exception)
            {
                if (memberMapData.Member.MemberType == MemberTypes.Property)
                {
                    var property = memberMapData.Member as PropertyInfo;

                    if (property?.PropertyType == typeof(DateTime?))
                    {
                        return default(DateTime?);
                    }

                    if (property?.PropertyType == typeof(DateTime))
                    {
                        return default(DateTime);
                    }
                }

                return null;
            }
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var attr = memberMapData.Member
                            .DeclaringType
                            .GetProperty(memberMapData.Member.Name)
                            .GetCustomAttribute<CsvSerializerHeaderAttribute>();

            if(attr != null && !String.IsNullOrEmpty(attr.DateTimeFormat))
            {
                var dt = value as DateTime?;
                return dt.HasValue ? dt.Value.ToString(attr.DateTimeFormat) : null;
            }

            return base.ConvertToString(value, row, memberMapData);
        }
    }
}
