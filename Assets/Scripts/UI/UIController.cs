using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CanvasGroup soundGroup = null;
    [Space]
    [SerializeField] private SoundScrollView sfxScrollView = null;
    [SerializeField] private SoundScrollView ambientScrollView = null;
    [SerializeField] private SoundScrollView musicScrollView = null;

    [Header("Hue")]
    [SerializeField] private Button hueOpenButton = null;
    [SerializeField] private Button hueCloseButton = null;
    [SerializeField] private Button hueApplyButton = null;
    [SerializeField] private CanvasGroup huePanelGroup = null;
    
    [Header("Searching")]
    [SerializeField] private TMP_InputField searchInputField = null;
    [SerializeField] private Button searchCancelButton = null;


    //Flag
    private bool panelActive;


    private void Awake()
    {
        //Listener
        hueOpenButton.onClick.AddListener(() => TogglePanel(huePanelGroup));
        hueCloseButton.onClick.AddListener(() => TogglePanel(huePanelGroup));
        hueApplyButton.onClick.AddListener(() => TogglePanel(huePanelGroup));

        searchInputField.onValueChanged.AddListener((input) => OnSearchInputFieldValueChanged(input));
        searchCancelButton.onClick.AddListener(OnSearchCancelButtonClicked);
    }


    private void TogglePanel(CanvasGroup _group)
    {
        panelActive = !panelActive;

        //Alpha
        _group.alpha = (panelActive) ? 1.0f : 0.0f;

        //Interactable
        soundGroup.interactable = !panelActive;
        _group.interactable = panelActive;

        //Raycasts
        _group.blocksRaycasts = panelActive;
    }


    private void OnSearchCancelButtonClicked()
    {
        searchInputField.text = "";
    }


    private void OnSearchInputFieldValueChanged(string _input)
    {
        sfxScrollView.Filter(_input);
        ambientScrollView.Filter(_input);
        musicScrollView.Filter(_input);
    }
}