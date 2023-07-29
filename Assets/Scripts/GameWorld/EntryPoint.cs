using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    [SerializeField, Voxell.Util.Scene] private string[] m_InitialScenes;

    private void Awake()
    {
        int sceneCount = SceneManager.sceneCount;
        Scene[] openedScenes = new Scene[sceneCount];

        for (int s = 0; s < sceneCount; s++)
        {
            openedScenes[s] = SceneManager.GetSceneAt(s);
        }

        for (int i = 0; i < this.m_InitialScenes.Length; i++)
        {
            if (
                !System.Array.Exists(
                    openedScenes,
                    (scene) => scene.name == this.m_InitialScenes[i]
                )
            ) {
                SceneManager.LoadSceneAsync(this.m_InitialScenes[i], LoadSceneMode.Additive);
            }
        }
    }
}
