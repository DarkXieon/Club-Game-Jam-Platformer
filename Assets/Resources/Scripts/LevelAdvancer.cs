using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdvancer : Activatable
{
    public string NextScene;

    public override void OnActivate()
    {
        SceneManager.LoadScene(NextScene);
    }
}