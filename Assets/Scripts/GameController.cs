using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int roomIndex = 0;
    private Player playerToWake;
    InstantiateObstacles m_instantiateObstacles;
    protected List<Player> players;

    void Start()
    {
        m_instantiateObstacles = GameObject.FindGameObjectsWithTag("InstantiateObstacles")[0].GetComponent<InstantiateObstacles>();
        GameObject[] playerGameObjs = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerGameObj in playerGameObjs)
        {
            print(playerGameObj.ToString());
            players.Add(playerGameObj.GetComponent<Player>());
        }
    }

    public void Victory() {
        StopPlay();
        PlayCutScene(-1);
        SceneManager.LoadScene("Victory");
    }
    public void StartRoom(int roomIndex)
    {
        StopPlay();
        PlayCutScene(roomIndex);
        InstantiateObstacles(roomIndex);
        ResumePlay();
    }

    public void StopPlay()
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
        switch (roomIndex)
        {
            // Tutorial
            case 0:
            // Easy
            case 1:
            // Medium
            case 2:
            // Hard
            case 3:
            // Victory
            case -1:
            return;
            // TODO: Not implemented yet
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
