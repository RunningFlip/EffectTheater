﻿using Theater.Sounds;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public class SoundUIDelegate : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private SoundScrollView sfxScrollView;
        [SerializeField] private SoundScrollView ambientScrollView;
        [SerializeField] private SoundScrollView musicScrollView;
        [Space]
        [SerializeField] private SearchFilter searchFilter;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {

            SoundCollectionContainer container = new SoundCollectionContainer();

            this.searchFilter.SoundCollectionContainer = container;

            if (container.SFXCollection != null) {
                this.sfxScrollView.InitScrollView(container.SFXCollection);
            }

            if (container.AmbientCollection != null) {
                this.ambientScrollView.InitScrollView(container.AmbientCollection);
            }

            if (container.MusicCollection != null) {
                this.musicScrollView.InitScrollView(container.MusicCollection);
            }
        }

        //--------------------------------------------------------------------------------
    }
}