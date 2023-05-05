namespace MyMusiX
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Parsing;
    using System.IO;
    using System.Threading.Tasks;

    public class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand();

            var importCsvFile = new Option<FileInfo?>("--input-csv", "CSV file to import from");
            var importCsvCommand = new Command("import", "Import CSV file")
            {
                importCsvFile
            };

            importCsvCommand.SetHandler((file) =>
            {
                ImportFileAsync(file!).Wait();
            }, importCsvFile);


            var isArtist = new Option<bool>("--artists", "Query artists");
            var sampleSize = new Option<int>("--s", getDefaultValue: () => 25, description: "number of items to return at random");
            var queryCommand = new Command("query", "Query music records")
            {
                isArtist, sampleSize
            };

            queryCommand.SetHandler((isArtist, sampleSize) =>
            {
                if (isArtist)
                {
                    QueryArtists(sampleSize);
                }
            }, isArtist, sampleSize);

            var nRecommendations = new Option<int>("--n", getDefaultValue: () => 3, description: "Number of artists to recommend");
            var qualifier = new Option<string>("--qualifier", getDefaultValue: () => "", description: "A string to append to the prompt.");
            var recommendCommand = new Command("recommend", "Recommend artists")
            {
                nRecommendations, sampleSize, qualifier
            };

            recommendCommand.SetHandler((nRecommendations, sampleSize, qualifier) =>
            {
                RecommendArtistsAsync(nRecommendations, sampleSize, qualifier).Wait();
            }, nRecommendations, sampleSize, qualifier);

            rootCommand.Add(importCsvCommand);
            rootCommand.Add(queryCommand);
            rootCommand.Add(recommendCommand);

            return await rootCommand.InvokeAsync(args);
        }

        static async Task ImportFileAsync(FileInfo file)
        {
            var reader = new RecordCsvReader();
            var db = new ApplicationDbContext();

            await db.ImportRecordsAsync(reader.ReadRecords(file));
        }

        static IEnumerable<string> SelectArtists(IEnumerable<Record> records, int sampleSize)
        {
            var random = new Random(DateTime.Now.Millisecond);

            var artists = records.Select(r => r.ArtistName).Distinct().ToArray();

            var histogram = records
                    .Where(r => !string.IsNullOrEmpty(r.ArtistName))
                    .GroupBy(r => r.ArtistName)
                    .Select(g => new { Value = g.Key, Count = g.Count() })
                    .OrderBy(x => x.Count)
                    .ToList();

            Console.WriteLine($"sampling {sampleSize} artists out of {artists.Count()}");

            return histogram
                .OrderBy(r => random.Next() * r.Count)
                .Take(sampleSize)
                .Select(x => x.Value);
        }

        static async Task RecommendArtistsAsync(int nRecommendations, int sampleSize, string qualifier)
        {
            var db = new ApplicationDbContext();
            var chatClient = new ChatClient();

            var artists = SelectArtists(db.Records, sampleSize);

            var artistsJoined = string.Join(',', artists);

            var response = await chatClient
                .AskQuestionAsync(
                $"recommend {nRecommendations} artists which i would also like if i like {artistsJoined} and justify your recommendation. "
                + qualifier);

            Console.WriteLine(response);
        }

        static void QueryArtists(int sampleSize)
        {
            var db = new ApplicationDbContext();

            var artists = SelectArtists(db.Records, sampleSize);

            Console.WriteLine(string.Join(',', artists));
        }
    }
}