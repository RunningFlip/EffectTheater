using System.IO;
using Theater.Coloring;
using UnityEditor;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public class MusicSoundCollection : SoundCollectionBase<SoundLoopHandler, LerpColorizer> {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public static string ResourcePath => Path.Combine(MusicSoundCollection.ResourcesPath, "MusicSoundCollection.asset");

        //--------------------------------------------------------------------------------
        // Music
        //--------------------------------------------------------------------------------

        [MenuItem("Sound Collections/Create Music")]
        public static void CreateMusicCollection() {
            MusicSoundCollection collection = CreateAsset<MusicSoundCollection>(Path.Combine("Assets", ResourcePath));
            collection.soundType = SoundType.Music;
        }

        //--------------------------------------------------------------------------------
    }
}