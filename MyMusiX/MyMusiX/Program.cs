namespace MyMusiX
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using System;
    using System.CommandLine;
    using System.CommandLine.Builder;
    using System.CommandLine.Invocation;
    using System.CommandLine.Parsing;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;

    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand();

            var importCsvFile = new Option<FileInfo?>("--input-csv", "CSV file to import from");
            var sqlitdb = new Option<FileInfo?>("--sqlitedb", "SQLite db to import to");
            var importCsvCommand = new Command("import", "Import CSV file")
            {
                importCsvFile, sqlitdb
            };

            rootCommand.Add(importCsvCommand);

            importCsvCommand.SetHandler((file) =>
            {
                ReadFile(file!);
            }, importCsvFile);

            return await rootCommand.InvokeAsync(args);
        }

        static void ReadFile(FileInfo file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            using (var stream = file.OpenText())
            using (var csv = new CsvReader(stream, config))
            {
                var records = csv.GetRecords<Record>();

                var types = new HashSet<string>();

                foreach(var record  in records)
                {
                    types.Add(record.Type);
                }

                foreach (var type in types)
                {
                    Console.WriteLine(type);
                }
            }
        }
    }
}