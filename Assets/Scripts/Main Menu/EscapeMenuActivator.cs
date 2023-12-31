using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeMenuActivator : MonoBehaviour
{

    public GameObject escapeMenu;
    public GameController gameController;
    public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                escapeMenu.SetActive(true);
                isPaused = true;
                //PauseGame();
            }
            else
            {
                escapeMenu.SetActive(false);
                isPaused = false;
                //ResumeGame();
            }
        }
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
