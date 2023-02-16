using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cutscene : MonoBehaviour
{
    const float CUTSCENE_DURATION = 3f;
    
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Animator _animator;
    [SerializeField] string _animName;

    [Header("Every Anim")]
    
    [SerializeField] private Sprite _playerSpriteUp;
    [SerializeField] private Sprite _playerSpriteDown;
    [SerializeField] private Sprite _bagSpriteUp;
    [SerializeField] private Sprite _bagSpriteDown;

    [Header("Bus Anim")] 
    [SerializeField] private Sprite _busSprite;
    
    [Header("Car Anim")] 
    [SerializeField] private Sprite _carSprite;
    
    [Header("Shop Anim")]
    [SerializeField] private Sprite _shopBuilding;
    [SerializeField] private Sprite _shopDoor;


    [field: SerializeField] public CutsceneType Type { get; private set; }
    
    public static Action<Cutscene> CutsceneStarted;
    public static Action<Cutscene> CutsceneEnded;

    private Player _player;
    private JoystickControls _joystickControlsPlayer;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        if(_player)
            _joystickControlsPlayer = _player.GetComponent<JoystickControls>();
        else
            Debug.LogError("No JoystickControls on player");
    }

    public IEnumerator Play()
    {
        CutsceneStarted?.Invoke(this);

        _animator.enabled = true;
        _animator.Play(_animName);
        
        var timer = 0f;

        while (timer < CUTSCENE_DURATION * 0.2f)
        {
            yield return null;
            timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / (CUTSCENE_DURATION * 0.2f));
        }

        _canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(CUTSCENE_DURATION * 0.6f);

        timer = 0f;
        
        while (timer < CUTSCENE_DURATION * 0.2f)
        {
            yield return null;
            timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / (CUTSCENE_DURATION * 0.2f));
        }

        _canvasGroup.alpha = 0f;

        _animator.enabled = false;
        
        CutsceneEnded?.Invoke(this);
    }

    public void PlayAnim()
    {
        Sequence animationSequence = DOTween.Sequence();
        //Disable Player Movements
        _player.GetComponent<PlayerInput>().enabled = false;
        animationSequence.Append(_canvasGroup.DOFade(1, CUTSCENE_DURATION * 0.2f));
        switch (Type)
        {
            case CutsceneType.Bus :
                 break;
            case CutsceneType.Car : 
                break;
            case CutsceneType.Delivery :
                break;
            case CutsceneType.Shop : 
                break;
            default: 
                break;
        }
    }
}