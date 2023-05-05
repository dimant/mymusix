namespace MyMusiX
{
    using CsvHelper.Configuration;
    using CsvHelper;
    using System.Globalization;

    internal class RecordCsvReader
    {
        public IEnumerable<Record> ReadRecords(FileInfo csvFile)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            using (var stream = csvFile.OpenText())
            using (var csv = new CsvReader(stream, config))
            {
                csv.Context.RegisterClassMap<RecordMap>();

                foreach (var record in csv.GetRecords<Record>())
                {
                    record.Id = Guid.NewGuid();
                    yield return record;
                }
            }
        }
    }
}
