namespace MyMusiX
{
    using CsvHelper.Configuration;
    using Microsoft.EntityFrameworkCore;

    [PrimaryKey("Id")]
    public class Record
    {
        public Guid Id { get; set; } = Guid.Empty;

        public string TrackName { get; set; } = string.Empty;

        public string ArtistName { get; set; } = string.Empty;

        public string AlbumName { get; set; } = string.Empty;

        public string PlaylistName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string ISRC { get; set; } = string.Empty;
    }

    public class RecordMap : ClassMap<Record>
    {
        public RecordMap()
        {
            Map(m => m.TrackName).Index(0);
            Map(m => m.ArtistName).Index(1);
            Map(m => m.AlbumName).Index(2);
            Map(m => m.PlaylistName).Index(3);
            Map(m => m.Type).Index(4);
            Map(m => m.ISRC).Index(5);
        }
    }
}
