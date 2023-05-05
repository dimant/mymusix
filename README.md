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

### Example Session

```
D:\mymusix\MyMusiX\MyMusiX\bin\Debug\net7.0>notepad api.key

D:\mymusix\MyMusiX\MyMusiX\bin\Debug\net7.0>MyMusiX.exe import --input-csv "playlists\My Spotify Playlist.csv"

D:\mymusix\MyMusiX\MyMusiX\bin\Debug\net7.0>MyMusiX.exe import --input-csv "playlists\My Tidal Playlist.csv"

D:\mymusix\MyMusiX\MyMusiX\bin\Debug\net7.0>MyMusiX.exe recommend
sampling 25 artists out of 3192
1. The Doobie Brothers - If you enjoy the funk and soul infused sound of Shalamar and Nu Shooz, then you'll definitely appreciate The Doobie Brothers' classic hit "What a Fool Believes". They have a similar upbeat rhythm and catchy melodies that will have you grooving along in no time.

2. Dua Lipa - With her powerful voice and dynamic pop sound, Dua Lipa is a force to be reckoned with in the music industry. If you enjoy the electronic beats and modern production of Wiener Philharmoniker and Yuksek, then you'll appreciate Dua Lipa's contemporary sound and ability to blend different genres seamlessly.

3. Sade - If you're a fan of Portishead's moody, atmospheric sound and Annie Lennox's emotive vocals, then you'll definitely appreciate Sade's unique blend of jazz, soul, and R&B. Her smooth, sultry voice and understated instrumentals create a dreamy mood that will transport you to another world.

D:\mymusix\MyMusiX\MyMusiX\bin\Debug\net7.0>
```