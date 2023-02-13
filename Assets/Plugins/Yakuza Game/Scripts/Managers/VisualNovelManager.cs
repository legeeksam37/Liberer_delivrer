using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;



public enum CharacterVN
{
    Null,
    Suzui,
    Mitsuri,
    Kadojima,
    Yakuza,
    Momo,
}

public class VisualNovelManager : MonoBehaviour
{
    public static VisualNovelManager Instance;
    //private AudioManager _audioManager;
    [SerializeField] private bool firstDay = true;
    
    [Header("UI Attribution VN")]
    [SerializeField] private GameObject _panelVN;
    [SerializeField] private bool _canGoNext;
    private Image _panelVNImage;
    
    [SerializeField] private GameObject _panelDialogue;
    private Image _panelDialogueImage;
    [SerializeField] private GameObject _panelName;
    private Image _panelNameImage;
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textDialogue;

    private float _timerCanContinue;
    [SerializeField] private float _timerCanContinueMax;
    [SerializeField] private GameObject _imageCanContinueNextDialogue;

    [Header("Character VN")] 
    [SerializeField] private Sprite _transparentSprite;
    
    //[SerializeField] private Image _leftCharacterImage;
    [SerializeField] private Image _rightCharacterImage;
    [SerializeField] private CharacterVN _rightCharacterEnum;


    [SerializeField] private Transform _leftCharaTransformParent;
    [SerializeField] private GameObject _leftCharacterImagePrefab;
    [SerializeField] private List<GameObject> _leftCharacterImageList = new List<GameObject>();

    [Header("Sequence VN")]
    [SerializeField] public List<VNSequence> _sequencesList = new List<VNSequence>();
    public int indexSequence;
    [SerializeField] private Dictionary<VNSequence, int> _dictionarySequence = new Dictionary<VNSequence, int>();
    
    [Header("Dialogue VN")]
    [SerializeField] private bool _dialogueIsFinished;
    
    private string _dialogueToWrite; 
    private int _indexCharacterDialogue; 
    private float _timerPerCharacterDialogue; 
    [SerializeField] private float _timerPerCharacterDialogueMax;
    
    [SerializeField] private float _speedUpText;

    [Header("Choice VN")] 
    [SerializeField] private GameObject _panelChoice;
    [SerializeField] private GameObject _buttonChoice;
    [SerializeField] private bool _choiceWillHappen;
    [SerializeField] private VNSequence _sequencePostChoice;
    [SerializeField] private string _choiceString;
    [SerializeField] private bool sequenceChoiceIsFinished;
    
    //Will be usefull if there is two characters speaking
    private CharacterVN _lastCharacterThatSpoke;

    private VNSequence _sequenceCurrentlyPlaying;
    private int _indexSequenceCurrentlyPlaying;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //_audioManager = AudioManager.Instance;
        
        //Reset text and setup components/timers
        
        _panelVNImage = _panelVN.GetComponent<Image>();
        _panelDialogueImage = _panelDialogue.GetComponent<Image>();
        _panelNameImage = _panelName.GetComponent<Image>();
        _textDialogue.text = null;
        _textName.text = null;

        _timerPerCharacterDialogue = _timerPerCharacterDialogueMax;
        _timerCanContinue = _timerCanContinueMax;
        
        //Reset all index from scriptables 
        ResetIndexAllSequences();

        //if (!MainMenu.resuming)
        //{
            TriggerVNScene();
        //}
    }

    private void Update()
    {
        if (_panelVN.activeSelf && _canGoNext /* && !GameplayManager.Instance.isPaused*/)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (_dialogueIsFinished)
                {
                    //Display Button Choice Answer
                    if (_choiceWillHappen)
                    {
                        ChoiceDisplay(true);
                    }
                    else
                    {
                        if (_choiceString.Length > 0 && sequenceChoiceIsFinished) 
                        {
                            _textName.text = "";
                            _indexCharacterDialogue = 0;
                            _dialogueIsFinished = false;
                            _dialogueToWrite = _choiceString;
                                    
                            sequenceChoiceIsFinished = false;
                            _choiceString = "";
                        }
                        else
                        {
                            NextDialogue();
                        }
                    }
                }
            }
            //Display All the text 
            if (Input.GetMouseButtonDown(1))
            {
                _textDialogue.text = _dialogueToWrite;
                _dialogueIsFinished = true;
            }
            
            DisplayLetterPerLetter(_dialogueToWrite);
            MakeCanContinueTextWink();
        }
    }

    //Display the buttons of the choice the player will have.
    //It's created from VNDialogueChoice scriptable object values
    
    //Create button and attribute them OnClick listener from VNDialogue choice scriptable Unity Event list 
    //Then display with some fading the choice's panel
    private void ChoiceDisplay(bool value)
    {
        if (value)
        {
            //Display Tuto Choice
            //TutoManager.Instance.CreateTuto(TutoName.ChoiceVN);
            
            //Get the previous Sequence because otherwise it would have been out of index
            VNDialogueChoice dialogueChoice = GetActualDialogue(-1) as VNDialogueChoice;

            if (_panelChoice.transform.childCount == 0)
            {
                for (int i = 0; i < dialogueChoice.choiceVNList.Count; i++)
                {
                    GameObject buttonChoice = Instantiate(_buttonChoice, _panelChoice.transform);
                    buttonChoice.GetComponentInChildren<TextMeshProUGUI>().text = GetTranslatedStringFromKey(dialogueChoice.choiceVNList[i].textChoiceKey);
                    Button.ButtonClickedEvent buttonClickedEvent = buttonChoice.GetComponent<Button>().onClick;
                
                    foreach (var functionList in dialogueChoice.choiceVNList[i].changeList)
                    {
                        buttonClickedEvent .AddListener(functionList.Invoke);
                    }
                
                    var i2 = i;
                    buttonClickedEvent.AddListener(delegate
                    {
                        StartChoiceSequence(dialogueChoice.choiceVNList[i2].postChoiceSequence);
                    });
                }
            }
            DisplayChoicePanel(true);
        }
        else
        {
            DisplayChoicePanel(false);
        }
    }

    //Fonction that will add a text from choice's button OnClick
    public void AddStringToEndText(string text)
    {
        _choiceString += " " + text;
    }

    //Display Choice's panel elements using Dotween
    private void DisplayChoicePanel(bool value)
    {
        if (value)
        {
            //_panelChoice.SetActive(true);
            _panelChoice.GetComponent<Image>()
                .DOFade(0.4f, 0.5f)
                .SetUpdate(true);

            for (int i = 0; i < _panelChoice.transform.childCount; i++)
            {
                GameObject child = _panelChoice.transform.GetChild(i).gameObject;
                child.GetComponent<Image>()
                    .DOFade(1f, 0.75f)
                    .SetUpdate(true);
                child.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(1f, 0.75f)
                    .SetUpdate(true);
            }
        }
        else
        {
            _panelChoice.GetComponent<Image>()
                .DOFade(0f, 0.5f)
                .SetUpdate(true);

            for (int i = 0; i < _panelChoice.transform.childCount; i++)
            {
                GameObject child = _panelChoice.transform.GetChild(i).gameObject;
                child.GetComponent<Button>().interactable = false;
                child.GetComponent<Image>()
                    .DOFade(0f,0.75f)
                    .SetUpdate(true)
                    .OnComplete(()=> Debug.Log("Fade Child " + i));
                child.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(0f,0.75f)
                    .SetUpdate(true)
                    .OnComplete(()=> Destroy(child));
            }
        }
    }

    //Put the sequence in the list, can have an index offset
    public void PlugVNSequence(VNSequence sequence,int value,int offset = 0)
    {
        if (offset != 0)
        {
            if (indexSequence + offset > _sequencesList.Count)
            {
                offset = _sequencesList.Count - indexSequence;
            }
            _sequencesList.Insert(indexSequence + offset,sequence);
        }
        else
        {
            _dictionarySequence.Add(sequence,value);
        }
       
    }

    //Function put in choice created buttons
    //Start the new sequence in paramater
    public void StartChoiceSequence(VNSequence sequence)
    {
        //_audioManager.PlaySound("VN_Choice");
        
        _dialogueIsFinished = false;
        _sequencePostChoice = sequence;
        ChoiceDisplay(false);
        NextDialogue();
    }

    //Function that will make a symbol winking if the player can pass to the next dialogue
    private void MakeCanContinueTextWink()
    {
        if (_dialogueIsFinished)
        {
            _timerCanContinue -= Time.unscaledDeltaTime;
            if (_timerCanContinue <= 0)
            {
                _timerCanContinue = _timerCanContinueMax;
                _imageCanContinueNextDialogue.SetActive(!_imageCanContinueNextDialogue.activeSelf);
            }
        }
    }

    //Make dialogue text appearing letter by letter in the VN text
    //If the player press space et left click, it displays faster
    private void DisplayLetterPerLetter(string dialogueToWrite)
    {
        if (!_dialogueIsFinished)
        {
            if (dialogueToWrite != null && dialogueToWrite.Length > 0)
            {
                if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
                {
                    _timerPerCharacterDialogue -= Time.unscaledDeltaTime * _speedUpText;
                }
                else
                {
                    _timerPerCharacterDialogue -= Time.unscaledDeltaTime;
                }
            
                if (_timerPerCharacterDialogue <= 0)
                {
                    _timerPerCharacterDialogue = _timerPerCharacterDialogueMax;
                    if (_indexCharacterDialogue < dialogueToWrite.Length)
                    {
                        _indexCharacterDialogue++;
                        _textDialogue.text = dialogueToWrite.Substring(0, _indexCharacterDialogue);
                    }
                    else
                    {
                        _dialogueIsFinished = true;
                    }
                }
            }
        }
    }

    //First function that will make the VN panel appear and stop the time
    public bool TriggerVNScene()
    {
        if (_dictionarySequence.Count > 0)
        {
            _sequencesList.Insert(indexSequence,_dictionarySequence.Aggregate((l, r) => l.Value > r.Value ? l : r).Key);
            _dictionarySequence.Clear();
        }
        
        if (GetActualSequence() != null)
        {
            //Stop the time
            Time.timeScale = 0;

            DisplayVNScene(true);
            _canGoNext = true;
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(_panelVNImage.DOFade(0.8f, 1f)).SetUpdate(true);
            mySequence.Join(_imageCanContinueNextDialogue.GetComponent<Image>().DOFade(1f, 1f)).SetUpdate(true);
            mySequence.Append(_panelDialogueImage.DOFade(0.9f, 0.5f)).SetUpdate(true).OnComplete(NextDialogue);
            mySequence.Join(_panelNameImage.DOFade(0.8f, 1f)).SetUpdate(true);
            //Return true because the GM needs to know if there is a scene to play, otherwise trigger the next day from GM
            
            //_audioManager.PlayMusic("BGM_VisualNovel");
            
            return true;
        }
        else
        {
            //Return false to the GM and make it start a new day
            indexSequence++;
            return false;
        }
    }

    //Display or Hide the VN panel
    public void DisplayVNScene(bool value)
    {
        _panelVN.SetActive(value);
    }

    //Reset text and image in order to restart a VN sequence next
    private void EndVNSequence()
    {
        _dialogueToWrite = null;
        _textDialogue.text = null;
        _textName.text = null;
        _sequencePostChoice = null;
        
        if (_sequencesList.Count > indexSequence)
        {
            ResetIndexAllSequences();
            indexSequence++;
        }
        else
        {
            Debug.Log("No More VN");
        }

        Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(_leftCharacterImage.DOFade(0f, 0.75f));
        mySequence.Append(_rightCharacterImage.DOFade(0f, 0.75f)).SetUpdate(true);
        mySequence.Append(_panelDialogueImage.DOFade(0f, 0.5f)).SetUpdate(true);
        mySequence.Join(_panelNameImage.DOFade(0f, 0.5f)).SetUpdate(true);
        mySequence.Join(_imageCanContinueNextDialogue.GetComponent<Image>().DOFade(0f, 0.5f)).SetUpdate(true);
        foreach (var leftCharacter in _leftCharacterImageList)
        {
            leftCharacter.GetComponent<Image>().DOFade(0f, 0.75f).SetUpdate(true);
        }

        _canGoNext = false;
        
        //_audioManager.StopMusic("BGM_VisualNovel");
        mySequence.Append(_panelVNImage.DOFade(0f, 1f).SetUpdate(true).OnComplete(() =>
        {
            DisplayVNScene(false);
            ResetCharacters();
            ResetTextPanel();
            

            //GameplayManager.Instance.StartCoroutine(GameplayManager.Instance.StartDay());
            //TutoManager.Instance.CreateTuto(TutoName.StartGame);
            //Get back to the neutral time and start a new day
        }));
    }

    //Reset all text from panel
    private void ResetTextPanel()
    {
        _imageCanContinueNextDialogue.SetActive(false);
        _dialogueIsFinished = false;
        _textDialogue.text = null;
        _textName.text = null;
        _indexCharacterDialogue = 0;
        _timerPerCharacterDialogue = _timerPerCharacterDialogueMax;
    }

    //Get the next Dialogue to play and display it on the VN
    private void NextDialogue()
    {
        //Check if there is a sequence
        if (GetActualSequence() != null)
        {
            if (GetActualSequence().indexDialogue < GetActualSequence().dialogueList.Count)
            {
                //Check the last dialogue and tell if this will trigger a choice
                if (GetActualDialogue().GetType() == typeof(VNDialogueChoice))
                {
                    _choiceWillHappen = true;
                }
                else
                {
                    _choiceWillHappen = false;
                }
                ResetTextPanel();
                DisplayDialogue();
                SetupCharacterDialogue();
                FocusCharacter(GetActualSequenceCharacter());
                GetActualSequence().indexDialogue++;
            }
            else
            {
                if (_choiceString.Length > 0)
                {
                    sequenceChoiceIsFinished = true;
                }
                else
                {
                    EndVNSequence();
                }
            }
        }
        
    }

    //Reset all index from scriptables 
    private void ResetIndexAllSequences()
    {
        foreach (var sequence in _sequencesList)
        {
            if (sequence != null)
            {
                ResetIndexOfSequence(sequence);
            }
        }
    }

    public static void ResetIndexOfSequence(VNSequence sequence)
    {
        sequence.indexDialogue = 0;
        foreach (var dialogue in sequence.dialogueList)
        {
            if (dialogue != null)
            {
                if (dialogue.GetType() == typeof(VNDialogueChoice))
                {
                    VNDialogueChoice dialogueChoice = dialogue as VNDialogueChoice;
                    foreach (var choice in dialogueChoice.choiceVNList)
                    {
                        if (choice.postChoiceSequence != null)
                        {
                            choice.postChoiceSequence.indexDialogue = 0;
                        }
                    }
                }
            }
        }
    }
    
    //Change sprite depending on what is inside VNDIalogue scriptable object
    private void SetCharacterImage(Sprite spriteCharacter,CharacterVN characterVn)
    {
        //If the character is the main prota of the sequence
        //We know he is on the right side 
        if (IsActualSequenceCharacterIsMain())
        {
            _rightCharacterImage.sprite = spriteCharacter;
            _rightCharacterEnum = characterVn;
            SetAlphaImage(_rightCharacterImage,1);

            if (characterVn == CharacterVN.Yakuza)
            {
                Debug.Log("Yakuza");
                _rightCharacterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1000);
            }
            else
            {
                _rightCharacterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(683, 1000);
            }
            //TODO make a smooth transition
        }
        else
        {
            GameObject characterImage = _leftCharacterImageList
                .Find(e => e.GetComponent<CharacterImage>().characterVn == characterVn);
            if (characterImage)
            {
                characterImage.GetComponent<Image>().sprite = spriteCharacter;
            }
            else
            {
                GameObject characterImage2 = Instantiate(_leftCharacterImagePrefab, _leftCharaTransformParent);
                characterImage2.GetComponent<Image>().sprite = spriteCharacter;
                characterImage2.GetComponent<RectTransform>().rotation = new Quaternion(0f, -180, 0f, 0f);
                characterImage2.GetComponent<CharacterImage>().characterVn = characterVn;

                if (characterImage2.GetComponent<CharacterImage>().characterVn != CharacterVN.Yakuza)
                {
                    characterImage2.GetComponent<RectTransform>().sizeDelta = new Vector2(683, 1000);
                }
                else
                {
                    characterImage2.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1000);
                }
                
                _leftCharacterImageList.Add(characterImage2);
            }
        }
    }
    
    //Highlight the character speaking
    private void FocusCharacter(CharacterVN characterVn)
    {
        if (characterVn == _rightCharacterEnum)
        {
            SetColorImage(_rightCharacterImage, new Color32(0xFF, 0xFF, 0xFF,255));
            ScaleFocus(_rightCharacterImage.transform,1.1f);
            foreach (var image in _leftCharacterImageList)
            {
                Image imageComponent = image.GetComponent<Image>();
                SetColorImage(imageComponent, new Color32(0x7F, 0x7F, 0x7F,255));
                ScaleFocus(imageComponent.transform);
            }
        }
        else
        {
            foreach (var image in _leftCharacterImageList)
            {
                Image imageComponent = image.GetComponent<Image>();
                if (image.GetComponent<CharacterImage>().characterVn != characterVn)
                {
                    SetColorImage(imageComponent, new Color32(0x7F, 0x7F, 0x7F,255));
                    ScaleFocus(imageComponent.transform);
                }
                else
                {
                    SetColorImage(imageComponent,  new Color32(0xFF, 0xFF, 0xFF,255));
                    ScaleFocus(imageComponent.transform,1.1f);
                }
            }
            _rightCharacterImage.color = new Color32(0x7F, 0x7F, 0x7F,255);
            ScaleFocus(_rightCharacterImage.transform);
        }
    }

    //Fonction that will scale an image in parameters over time
    private void ScaleFocus(Transform transform, float valueMultiplier = 1)
    {
        transform.DOScale(Vector3.one * valueMultiplier, 0.5f).SetUpdate(true);
    }


    //Get the dialogue from the scriptable and put it on the _dialogueToWrite variable
    //The Update fonction will get this string and display it over time
    private void DisplayDialogue()
    {
        string actualDialogue = GetTranslatedStringFromKey(GetActualTranslatedSequenceDialogue());
        
        _dialogueToWrite = actualDialogue;
        _textName.text = GetActualSequenceCharacter().ToString();
    }

    //Transform the Key code of the string to an actual sentence
    private string GetTranslatedStringFromKey(string key)
    {
        //return I2.Loc.LocalizationManager.GetTranslation("Scenario/" + key);
        return key;
    }
    
    //Check if the character of the sequence is the main (the main will be on the right)
    private bool IsActualSequenceCharacterIsMain()
    {
        return GetActualSequenceCharacter() == GetActualSequence().mainCharacterSequence;
    }
    
    //Get the indexed sequence
    //Get the post choice sequence if created 
    private VNSequence GetActualSequence()
    {
        if (_sequencePostChoice)
        {
            return _sequencePostChoice;
        }
        else
        {
            return _sequencesList[indexSequence];
        }
    }
    
    private VNDialogue GetActualDialogue(int offset = 0)
    {
        return GetActualSequence().dialogueList[GetActualSequence().indexDialogue + offset];
    }

    private string GetActualTranslatedSequenceDialogue(int offsetIndex = 0)
    {
        if (GetActualSequence().indexDialogue + offsetIndex <= GetActualSequence().dialogueList.Count - 1)
        {
            return GetActualSequence().dialogueList[GetActualSequence().indexDialogue].keyTextCharacter;
        }
        return "null";
    }

    private CharacterVN GetActualSequenceCharacter(int offsetIndex = 0)
    {
        if (GetActualSequence().indexDialogue + offsetIndex <= GetActualSequence().dialogueList.Count - 1)
        {
            return GetActualSequence().dialogueList[GetActualSequence().indexDialogue].character;
        }
        return CharacterVN.Null;
    }
    
    private Sprite GetActualSequenceCharacterImage(int offsetIndex = 0)
    {
        if (GetActualSequence().indexDialogue + offsetIndex <= GetActualSequence().dialogueList.Count - 1)
        {
            return GetActualSequence().dialogueList[GetActualSequence().indexDialogue].spriteCharacter;
        }
        return null;
    }

    private void ResetCharacters()
    {
        _rightCharacterImage.sprite = _transparentSprite;
        foreach (var leftImageChara in _leftCharacterImageList)
        {
            Destroy(leftImageChara.transform.gameObject);
        }
        _leftCharacterImageList.Clear();
    }

    private void SetupCharacterDialogue()
    {
        CharacterVN characterVn = GetActualSequenceCharacter();
        Sprite spriteCharacter = GetActualSequenceCharacterImage();

        SetCharacterImage(spriteCharacter,characterVn);
    }
    
    //TODO Create a color class with this function in it
    public static void SetAlphaImage(Image image,float value)
    {
        Color color = image.color;
        color.a = value;
        image.color = color;
    }
    
    //TODO Create a color class with this function in it
    public static void SetColorImage(Image image,Color32 color)
    {
        Color32 newColor = color;
        image.color = newColor;
    }
}