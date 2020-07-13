using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Snippets.Utilities.CsvSerializer
{
    public class CsvSerializerUtility
    {
        public IEnumerable<TModel> Deserialize<TModel>(string filepath, bool hasHeader = true)
        {
            var models = new List<TModel>();

            using (var fileReader = File.OpenText(filepath))
            {
                using (var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Configuration.RegisterClassMap(new CsvSerializerUtilityClassMap<TModel>());
                    csvReader.Configuration.HasHeaderRecord = hasHeader;

                    foreach (var x in csvReader.GetRecords<TModel>())
                    {
                        models.Add(x);
                    }
                }
            }

            return models;
        }

        public void Serialize<TModel>(string filepath, IEnumerable<TModel> models, bool hasHeader = true)
        {
            var dirpath = Path.GetDirectoryName(filepath);
            if(!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }

            using (var writer = new StreamWriter(filepath))
            {
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.Configuration.RegisterClassMap(new CsvSerializerUtilityClassMap<TModel>());
                    csvWriter.Configuration.HasHeaderRecord = hasHeader;
                    csvWriter.WriteRecords(models);
                }
            }
        }
    }
}
