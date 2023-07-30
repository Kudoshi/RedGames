    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : UXBehaviour
{
    public AudioClip ButtonPress;
    public Button m_tapstartbutton;
    public Button m_quitbutton;
    public Label m_TapStartLabel;

    [SerializeField] private float m_PopSize;
    [SerializeField] private float m_PopSpeed;

    private void Awake()
    {
        InitializeDoc();

    }

    // Start is called before the first frame update
    void Start()
    {
        m_tapstartbutton = m_Root.Q<Button>("start_btn");
        m_TapStartLabel = m_Root.Q<Label>("tap_start");
        m_quitbutton = m_Root.Q<Button>("quit_btn");
        m_tapstartbutton.clicked += () => StartButton();
        m_quitbutton.clicked += () => QuitButton();

    }


    public override void TurnOnOffUX(bool onOff)
    {
        base.TurnOnOffUX(onOff);
        StartCoroutine(AnimateTapStartLabel());
    }

    private IEnumerator AnimateTapStartLabel()
    {
        bool increase = true;
        float originalSize = 110;
        float currentSize = originalSize;
        while (m_Document.enabled)
        {
            if (increase)
            {
                currentSize += Time.deltaTime * m_PopSpeed;
                    
            }
            else
            {
                currentSize -= Time.deltaTime * m_PopSpeed;

            }

            m_TapStartLabel.style.fontSize = currentSize;

            if (m_TapStartLabel.style.fontSize.value.value > (originalSize + m_PopSize))
                increase = false;
            else if (m_TapStartLabel.style.fontSize.value.value < (originalSize - m_PopSize))
                increase = true;

            yield return null;
        }
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
