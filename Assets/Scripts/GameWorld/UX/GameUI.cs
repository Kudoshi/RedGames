using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : UXBehaviour
{
    private Label m_AsdLbl;
    // Start is called before the first frame update
    void Start()
    {
        InitializeDoc();
        this.m_AsdLbl = m_Root.Q<Label>("asd");
    }
}
