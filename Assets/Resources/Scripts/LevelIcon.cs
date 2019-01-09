using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelIcon : MonoBehaviour
{
    public string SceneName;

    public void FocusThis()
    {
        Menu.Singleton.ResetScale();
        Menu.Singleton.Selection = this;
        Menu.Singleton.ScaleUpSelection();

        Menu.Singleton.EnterLevel();
    }
}
