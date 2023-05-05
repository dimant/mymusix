# mymusix

Uses [OpenAI](https://openai.com/) to recommend music. You need a [https://www.tunemymusic.com/](https://www.tunemymusic.com/) account an [OpenAI API Key](https://platform.openai.com/overview).

Download your Spotify playlists as CSV files from [https://www.tunemymusic.com/](https://www.tunemymusic.com/)

In the same folder as MyMusiX.exe create a file named "api.key" and paste your OpenAI API Key in it.

Import your track list into MyMusiX
```
    MyMusiX import --input-csv "C:\path\to\MyPlaylist.csv" 
```

By running import multiple times, you can import several CSV files, one from each of the streaming services you use.

Then, ask MyMusiX to recommend music:

```
PS C:\...\> .\MyMusiX.exe recommend
1. Royal Blood - similar in their hard rock sound and powerful vocals
2. Hozier - similar in their soulful, emotive vocals and lyricism
3. The 1975 - similar in their blend of alternative and pop sounds, and introspective lyrics.
PS C:\...\>
```