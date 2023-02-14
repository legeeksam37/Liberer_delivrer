using System;
using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    const float CUTSCENE_DURATION = 3f;
    
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Animator _animator;
    [SerializeField] string _animName;
    
    [field: SerializeField] public CutsceneType Type { get; private set; }
    
    public static Action<Cutscene> CutsceneStarted;
    public static Action<Cutscene> CutsceneEnded;

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
}