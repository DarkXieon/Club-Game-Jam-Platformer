using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdvancer : Activatable
{
    public string NextScene;

    public override void OnActivate()
    {
        SceneManager.LoadScene(NextScene);
    }

    //public string[] GameScenes; 

    //private static int currentScene;
    //private static LevelAdvancer singleton;

    //private void Start()
    //{
    //    DontDestroyOnLoad(gameObject);

    //    currentScene = 0;

    //    singleton = this;
    //}

    //public static void AdvanceScene()
    //{
    //    currentScene++;

    //    if(currentScene < singleton.GameScenes.Length)
    //    {
    //        SceneManager.LoadScene(singleton.GameScenes[currentScene]);
    //    }
    //}
}
