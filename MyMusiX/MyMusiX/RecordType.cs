namespace MyMusiX
{
    public enum RecordType
    {
        Favorite,
        Album,
        Artist,
        PlaylistTrack
    }

    public static class RecordTypeExtensions
    {
        public static RecordType ToRecordType(this string token)
        {
            switch (token)
            {
                case "Favorite":
                    return RecordType.Favorite;
                case "Album":
                    return RecordType.Album;
                case "Artist":
                    return RecordType.Artist;
                case "Plalist track":
                    return RecordType.PlaylistTrack;
                default:
                    throw new ArgumentException($"unknown type: {token}");
            }
        }
    }
}
