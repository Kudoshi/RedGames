using System;
using UnityEngine;

public class UXManager : SingletonMono<UXManager>
{
    public enum SceneType { MAIN_MENU, GAME }

    public GameUI GameUI;
    public AudioSource AudioSource;
    public GameOverScreen GameOverScreen;
    public MenuUI MenuUI;
    public SoundManager SoundManager;

    private void Start()
    {
        SwitchToMenu(SceneType.MAIN_MENU);
    }

    public void SwitchToMenu(SceneType sceneType)
    {
        if (sceneType == SceneType.MAIN_MENU)
        {
            GameUI.TurnOnOffUX(false);
            GameOverScreen.TurnOnOffUX(false);
            MenuUI.TurnOnOffUX(true);
        }
        else if (sceneType == SceneType.GAME)
        {
            GameUI.TurnOnOffUX(true);
            GameOverScreen.TurnOnOffUX(false);
            MenuUI.TurnOnOffUX(false);
        }

        // The Game --> Main menu is controlled directly by the train script
    }

}
