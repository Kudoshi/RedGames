using System;
using UnityEngine;

public class UXManager : SingletonMono<UXManager>
{
    public GameUI GameUI;
    public AudioSource AudioSource;
    public GameOverScreen GameOverScreen;
    public SoundManager SoundManager;

}
