using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public abstract class UXBehaviour : MonoBehaviour
{
    protected UIDocument m_Document;
    protected VisualElement m_Root;

    protected void InitializeDoc()
    {
        this.m_Document = this.GetComponent<UIDocument>();
        this.m_Root = this.m_Document.rootVisualElement;
    }
}
