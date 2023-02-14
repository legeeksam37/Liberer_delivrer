using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HomeSceneManager : MonoBehaviour
{

    [SerializeField]
    private GameObject popup_legal_mentions;

    // Start is called before the first frame update
    void Start()
    {
        hidePopUp();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void loadScene(string s_name){
        SceneManager.LoadScene(s_name);
    }

    public void showPopUp(){
         popup_legal_mentions.gameObject.SetActive(true);
    }

    public void hidePopUp(){
        popup_legal_mentions.gameObject.SetActive(false);
    }

}
