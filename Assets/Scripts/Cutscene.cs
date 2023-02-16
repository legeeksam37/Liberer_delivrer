using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Cutscene : MonoBehaviour
{
    const float CUTSCENE_DURATION = 3f;
    
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] TimelineAsset _timelineAsset;
    

    [field: SerializeField] public CutsceneType Type { get; private set; }
    
    public static Action<Cutscene> CutsceneStarted;
    public static Action<Cutscene> CutsceneEnded;

    private Player _player;
    private JoystickControls _joystickControlsPlayer;
    private PlayableDirector _playableDirector;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        if(_player)
            _joystickControlsPlayer = _player.GetComponent<JoystickControls>();
        else
            Debug.LogError("No JoystickControls on player");
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public IEnumerator Play()
    {
        CutsceneStarted?.Invoke(this);
        //Set up the cinematic
        _playableDirector.playableAsset = _timelineAsset;

        var timer = 0f;

        while (timer < CUTSCENE_DURATION * 0.2f)
        {
            yield return null;
            timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / (CUTSCENE_DURATION * 0.2f));
        }

        _canvasGroup.alpha = 1f;
        _playableDirector.Play();
        
        yield return new WaitForSeconds((float) _timelineAsset.duration);

        timer = 0f;
        
        while (timer < CUTSCENE_DURATION * 0.2f)
        {
            yield return null;
            timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / (CUTSCENE_DURATION * 0.2f));
        }

        _canvasGroup.alpha = 0f;
        
        
        CutsceneEnded?.Invoke(this);
    }
}