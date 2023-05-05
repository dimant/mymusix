namespace MyMusiX
{
    using CsvHelper.Configuration.Attributes;

    internal class Record
    {
        [Index(0)]
        public string TrackName { get; set; } = string.Empty;

        [Index(1)]
        public string ArtistName { get; set; } = string.Empty;

        [Index(2)]
        public string AlbumName { get; set; } = string.Empty;

        [Index(3)]
        public string PlaylistName { get; set; } = string.Empty;

        [Index(4)]
        public string Type { get; set; } = string.Empty;

        [Index(5)]
        public string ISRC { get; set; } = string.Empty;
    }
}
