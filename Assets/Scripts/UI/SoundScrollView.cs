using Theater.Sounds;
using TMPro;
using UnityEngine;

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

        public void InitScrollView(SoundCollection soundCollection) {

            this.volumeSlider.Value = soundCollection.masterVolume;

            GameObject prefab = null;

            switch (soundCollection.soundType) {

                case SoundCollection.SoundType.SFX:
                    this.title.text = SoundScrollView.SFX_TITLE;
                    prefab = this.soundElementSFXPrefab;
                    break;

                case SoundCollection.SoundType.Ambient:
                    this.title.text = SoundScrollView.AMBIENT_TITLE;
                    prefab = this.soundElementAmbientPrefab;
                    break;

                case SoundCollection.SoundType.Music:

                    this.title.text = SoundScrollView.MUSIC_TITLE;
                    prefab = this.soundElementMusicPrefab;
                    break;
            }

            if (prefab != null) {

                for (int i = 0; i < soundCollection.soundHandlers.Length; i++) {

                    SoundElement element = GameObject.Instantiate(prefab, this.contentParent).GetComponent<SoundElement>();
                    element.Initialize(soundCollection.soundHandlers[i]);
                }
            }
        }

        //--------------------------------------------------------------------------------
    }
}