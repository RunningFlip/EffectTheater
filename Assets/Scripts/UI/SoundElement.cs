using UnityEngine;
using Theater.Sounds;
using UnityEngine.UI;
using TMPro;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public abstract class SoundElement : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] protected TextMeshProUGUI title;
        [SerializeField] protected Button playButton;

        private SoundHandlerBase soundHandler;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public void Initialize(SoundHandlerBase soundHandler) {

            this.soundHandler = soundHandler;
            this.title.text = this.soundHandler.Title;

            this.OnInitialize(soundHandler);

            playButton.onClick.AddListener(this.soundHandler.Play);
        }

        //--------------------------------------------------------------------------------

        protected abstract void OnInitialize(SoundHandlerBase soundHandler);

        //--------------------------------------------------------------------------------
    }
}