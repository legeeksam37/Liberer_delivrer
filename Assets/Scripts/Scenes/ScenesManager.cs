using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    
    public enum Scene
    {
        HomePage,
        Menu,
        Game
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.Menu);
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.Game.ToString());
    }
}
