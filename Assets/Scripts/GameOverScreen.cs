using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public void Setup(){
        gameObject.SetActive(true);
    }
    public void RestartButton(){
        SceneManager.LoadScene("bullethellchess");
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
