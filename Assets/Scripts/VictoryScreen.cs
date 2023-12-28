using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class VictoryScreen : MonoBehaviour
{
    public void Setup(){
        gameObject.SetActive(true);
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
