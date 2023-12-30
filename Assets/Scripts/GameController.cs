using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int roomIndex = 0;
    private Player playerToWake;
    InstantiateObstacles m_instantiateObstacles;
    protected List<Player> players;
    [SerializeField]
    protected GameObject evil;
    Camera mainCamera;
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
        if (roomIndex == -1)
        {
            animations.Last().Play();
        } else {
            animations[roomIndex].Play();
        }
    }

    public void InstantiateObstacles(int roomIndex)
    {
        m_instantiateObstacles.Instantiate(roomIndex);
    }

    // public void GameOver(){
    //     SceneManager.LoadScene("Defeat");
    // }    
}
