using System;
using Theater.Coloring;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public abstract class SoundHandlerBase<T> where T : ColorizerBase {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public string Title => this.title;
        public SearchTag SearchTag => this.searchTags;
        public T Colorizer => this.colorizer;
        public SoundTuple[] SoundTuples => this.soundTuples;

        public GameObject UIElement { get; set; }

        //--------------------------------------------------------------------------------

        public abstract float MasterVolume { get; set; }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] protected string title;
        [SerializeField] protected SearchTag searchTags;
        [Space]
        [SerializeField] protected T colorizer;
        [Space]
        [SerializeField] protected SoundTuple[] soundTuples;

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SoundHandlerBase(string title, SearchTag searchTag, SoundTuple[] soundTuples) {

            this.title = title;
            this.searchTags = searchTag;
            this.soundTuples = soundTuples;
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public abstract void Play();

        //--------------------------------------------------------------------------------
    }
}