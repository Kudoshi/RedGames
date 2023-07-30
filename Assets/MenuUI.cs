    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : UXBehaviour
{
    public AudioClip ButtonPress;
    public Button m_tapstartbutton;
    public Button m_quitbutton;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDoc();
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

    }

    public void QuitButton()
    {
        UXManager.Instance.AudioSource.PlayOneShot(ButtonPress);
    }
}
