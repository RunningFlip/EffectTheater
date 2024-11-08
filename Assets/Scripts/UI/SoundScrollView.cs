﻿using Theater.Coloring;
using Theater.Sounds;
using TMPro;
using UnityEngine;
using System.Linq;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public class SoundScrollView : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Constants
        //--------------------------------------------------------------------------------

        private const string SFX_TITLE = "SFX";
        private const string AMBIENT_TITLE = "Ambient";
        private const string MUSIC_TITLE = "Music";

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [Header("Scroll View")]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private CustomSlider volumeSlider;
        [SerializeField] private Transform contentParent;
        [Space]
        [SerializeField] private GameObject soundElementSFXPrefab;
        [SerializeField] private GameObject soundElementAmbientPrefab;
        [SerializeField] private GameObject soundElementMusicPrefab;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public void InitScrollView<T, R>(SoundCollectionBase<T, R> soundCollection) 
            where T : SoundHandlerBase<R> 
            where R : ColorizerBase {

            this.volumeSlider.Value = soundCollection.masterVolume;
            this.volumeSlider.OnValueChanged += (volume) => { soundCollection.masterVolume = volume; };

            GameObject prefab = null;

            switch (soundCollection.soundType) {

                case SoundType.SFX:
                    this.title.text = SFX_TITLE;
                    prefab = this.soundElementSFXPrefab;
                    break;

                case SoundType.Ambient:
                    this.title.text = AMBIENT_TITLE;
                    prefab = this.soundElementAmbientPrefab;
                    break;

                case SoundType.Music:

                    this.title.text = MUSIC_TITLE;
                    prefab = this.soundElementMusicPrefab;
                    break;
            }

            if (prefab != null) {

                foreach (T handler in soundCollection.soundHandlers.OrderBy(x => x.Title).ToList()) {

                    SoundElementBase<T, R> element = GameObject.Instantiate(prefab, this.contentParent).GetComponent<SoundElementBase<T, R>>();
                    element.Initialize(soundCollection.soundType, handler);
                    element.MasterVolume = this.volumeSlider.Value;

                    this.volumeSlider.OnValueChanged += (volume) => element.MasterVolume = volume;
                }
            }
        }

        //--------------------------------------------------------------------------------
    }
}