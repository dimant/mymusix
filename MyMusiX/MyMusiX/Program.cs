namespace MyMusiX
{
    using Microsoft.EntityFrameworkCore;
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
            var sampleSize = new Option<int>("--s", getDefaultValue: () => 100, description: "number of items to return at random");
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
            var recommendCommand = new Command("recommend", "Recommend artists")
            {
                nRecommendations, sampleSize
            };

            recommendCommand.SetHandler((nRecommendations, sampleSize) =>
            {
                RecommendArtistsAsync(nRecommendations, sampleSize).Wait();
            }, nRecommendations, sampleSize);

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
        static async Task RecommendArtistsAsync(int nRecommendations, int sampleSize)
        {
            Random random = new Random();
            var db = new ApplicationDbContext();
            var chatClient = new ChatClient();

            var artists = db.Records
                .Select(r => r.ArtistName)
                .Distinct()
                .Take(sampleSize);

            var artistsJoined = string.Join(',', artists);

            var response = await chatClient.AskQuestionAsync($"recommend {nRecommendations} artists similar to {artistsJoined}");

            Console.WriteLine(response);
        }

        static void QueryArtists(int sampleSize)
        {
            Random random = new Random();
            var db = new ApplicationDbContext();

            var artists = db.Records
                .Select(r => r.ArtistName)
                .Distinct()
                .Take(sampleSize);

            Console.WriteLine(string.Join(',', artists));
        }
    }
}