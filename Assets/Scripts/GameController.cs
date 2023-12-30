using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int roomIndex = 0;
    // static variables are persistent across scene reloads
    public static bool cutScenePlayed = false;
    private Player playerToWake;
    InstantiateObstacles m_instantiateObstacles;
    protected List<Player> players;
    Camera mainCamera;
    CameraFollow cameraFollowScript;
    [SerializeField]
    List<PlayableDirector> animations;

    void Start()
    {
        m_instantiateObstacles = GameObject.FindGameObjectsWithTag("InstantiateObstacles")[0].GetComponent<InstantiateObstacles>();
        GameObject[] playerGameObjs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerGameObj in playerGameObjs)
        {
            print(playerGameObj.ToString());
            players.Add(playerGameObj.GetComponent<Player>());
        }
        mainCamera = Camera.main;
        cameraFollowScript = mainCamera.GetComponent<CameraFollow>();
    }

    public void Victory() {
        PausePlay();
        PlayCutScene(-1);
        SceneManager.LoadScene("Victory");
    }
    public void StartRoom(int roomIndex)
    {
        PausePlay();
        PlayCutScene(roomIndex);
        InstantiateObstacles(roomIndex);
        ResumePlay();
    }

    public void PausePlay()
    {
        cameraFollowScript.enabled = false;
        foreach (Player player in players)
        {
            if (player.isBeingControlled)
            {
                playerToWake = player;
            }
            player.isBeingControlled = false;
            player.isInvulnerable = true;
        }
    }

    public void ResumePlay()
    {
        cameraFollowScript.enabled = true;
        foreach (Player player in players)
        {
            if (player == playerToWake)
            {
                player.isBeingControlled = true;
            }
            player.isInvulnerable = false;
        }
    }

    public void PlayCutScene(int roomIndex)
    {
        if (cutScenePlayed)
        {
            return;
        }
        if (roomIndex == -1)
        {
            animations.Last().Play();
        } else {
            animations[roomIndex].Play();
        }
        cutScenePlayed = true;
    }

    public void InstantiateObstacles(int roomIndex)
    {
        m_instantiateObstacles.Instantiate(roomIndex);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // public void GameOver(){
    //     SceneManager.LoadScene("Defeat");
    // }    
}
