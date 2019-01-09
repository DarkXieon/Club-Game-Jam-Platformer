using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceScene : MonoBehaviour
{
    public static AdvanceScene Singleton;

    private static string[] names = new string[]{ "Level 01", "Level 1", "Level 02", "Level 03", "Level 4" };
    private static int currentIndex;

    public void Awake()
    {
        Singleton = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();

            currentIndex = Mathf.Clamp(currentIndex + 1, 0, 4);
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            currentIndex = Mathf.Clamp(currentIndex - 1, 0, 4);

            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(names[currentIndex]);

        Menu.Singleton.gameObject.SetActive(false);
    }
}
