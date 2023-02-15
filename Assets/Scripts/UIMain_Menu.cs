using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain_Menu : MonoBehaviour
{

    [SerializeField] Button startGame; 
    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        StartCoroutine(AnimTransition());
    }

    IEnumerator AnimTransition()
    {
        GetComponent<Animator>().SetTrigger("StartAnim");
        yield return new WaitForSeconds(1.55f);
        ScenesManager.instance.LoadNewGame();
    }

}
