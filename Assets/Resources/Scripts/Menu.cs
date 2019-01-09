using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Singleton;

    public RectTransform MainMenu;
    public RectTransform LevelSelectionPage1;

    public LevelIcon[] LevelScenes;
    public AudioPlayer Player;
    
    public LevelIcon Selection { get; set; }

    private RectTransform currentRect;

    public void Awake()
    {
        Singleton = this;
        currentRect = MainMenu;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeTo(RectTransform rect)
    {
        currentRect.gameObject.SetActive(false);

        rect.gameObject.SetActive(true);

        currentRect = rect;
    }

    public void Play()
    {
        ChangeTo(LevelSelectionPage1);
        //AdvanceScene.Singleton.LoadNextScene();
        //gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Home()
    {
        ChangeTo(MainMenu);
        Player.Stop();
    }

    public void PageForward()
    {
        ChangeTo(LevelSelectionPage1);
    }

    public void PageBack()
    {
        ChangeTo(LevelSelectionPage1);
    }

    public void ScaleUpSelection()
    {
        Selection.transform.localScale *= 2f;
    }

    public void ResetScale()
    {
        if(Selection != null)
        {
            Selection.transform.localScale /= 2f;
        }
    }

    public void EnterLevel()
    {
        if(Selection != null)
        {
            SceneManager.LoadScene(Selection.SceneName);
            gameObject.SetActive(false);
            //SceneManager.sceneLoaded += (scene, loadMode) => SceneManager.SetActiveScene(scene);//SceneManager.GetSceneByName(Selection.SceneName));
            Player.Play();
        }
    }
}