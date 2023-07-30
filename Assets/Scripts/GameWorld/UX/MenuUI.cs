    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : UXBehaviour
{
    public AudioClip ButtonPress;
    public Button m_tapstartbutton;
    public Button m_quitbutton;

    private void Awake()
    {
        InitializeDoc();

    }

    // Start is called before the first frame update
    void Start()
    {
        m_tapstartbutton = m_Root.Q<Button>("start_btn");
        m_quitbutton = m_Root.Q<Button>("quit_btn");
        m_tapstartbutton.clicked += () => StartButton();
        m_quitbutton.clicked += () => QuitButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {

        UXManager.Instance.AudioSource.PlayOneShot(ButtonPress);
        UXManager.Instance.SwitchToMenu(UXManager.SceneType.GAME);
        Train.Instance.StartTrain();
    }

    public void QuitButton()
    {
        UXManager.Instance.AudioSource.PlayOneShot(ButtonPress);
        Application.Quit();
    }
}
