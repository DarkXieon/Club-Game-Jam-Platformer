using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public KeyCode LevelReset = KeyCode.Alpha0;

    private void Update()
    {
        if(Input.GetKeyDown(LevelReset))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
