using UnityEngine;
using Theater.Sounds;
using UnityEngine.UI;
using TMPro;
using Theater.Coloring;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    [RequireComponent(typeof(Animator))]
    public abstract class SoundElementBase<T, R> : MonoBehaviour 
        where T : SoundHandlerBase<R> 
        where R : ColorizerBase {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public float MasterVolume {

            get => this.masterVolume;

            set {
                this.masterVolume = value;
                this.soundHandler.MasterVolume = this.masterVolume;
            }
        }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [Header("Base")]
        [SerializeField] protected TextMeshProUGUI title;
        [SerializeField] protected Image outline;
        [SerializeField] protected Button playButton;
        [SerializeField] protected Button expandButton;
        
        [Header("Expand Button Image")]
        [SerializeField] private Sprite expandSprite;
        [SerializeField] private Sprite collapseSprite;

        private bool expanded;
        private float masterVolume;
        protected SoundType soundType;

        private Animator animator;
        private Image expandButtonImage;
        protected T soundHandler;

        protected static Color defaultColor = Color.white;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public void Initialize(SoundType soundType, T soundHandler) {

            this.soundType = soundType;
            this.soundHandler = soundHandler;
            this.soundHandler.UIElement = this.gameObject;

            this.title.text = this.soundHandler.Title;
            this.animator = this.GetComponent<Animator>();
            this.expandButtonImage = this.expandButton.image;

            this.ApplyColor(defaultColor);
            this.OnInitialize(soundHandler);
            this.SetupColorPickerButton(soundHandler.Colorizer);

            this.soundHandler.MasterVolume = this.MasterVolume;
            this.playButton.onClick.AddListener(() => this.soundHandler.Play());
            this.expandButton.onClick.AddListener(this.ToggleExpandButton);
        }

        //--------------------------------------------------------------------------------

        private void ToggleExpandButton() {

            this.expanded = !this.expanded;
            this.expandButtonImage.sprite = !this.expanded
                ? this.expandSprite
                : this.collapseSprite; 

            this.animator.SetTrigger("Toggle");
        }

        //--------------------------------------------------------------------------------

        protected void ApplyColor(Color color) {

            this.outline.color = color;
            this.playButton.image.color = color;

            this.OnApplyColor(color);
        }

        //--------------------------------------------------------------------------------

        protected abstract void OnInitialize(T soundHandler);
        protected abstract void OnApplyColor(Color color);
        protected abstract void SetupColorPickerButton(R colorizer);

        //--------------------------------------------------------------------------------
    }
}