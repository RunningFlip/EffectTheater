using UnityEngine;
using System.IO;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    public class SoundCollectionContainer {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public SFXSoundCollection SFXCollection { get; }
        public AmbientSoundCollection AmbientCollection { get; }
        public MusicSoundCollection MusicCollection { get; }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SoundCollectionContainer() {

            this.SFXCollection = SFXSoundCollection.LoadSoundCollection<SFXSoundCollection>(SFXSoundCollection.ResourcePath);
            this.AmbientCollection = AmbientSoundCollection.LoadSoundCollection<AmbientSoundCollection>(AmbientSoundCollection.ResourcePath);
            this.MusicCollection = MusicSoundCollection.LoadSoundCollection<MusicSoundCollection>(MusicSoundCollection.ResourcePath);
        }

        //--------------------------------------------------------------------------------
    }
}