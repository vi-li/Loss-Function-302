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
    // static variables are persisted across scene reloads
    [SerializeField]
    public static bool cutScenePlayed = false;
    private Player playerToWake;
    InstantiateObstacles m_instantiateObstacles;
    [SerializeField]
    protected List<Player> players;
    Camera mainCamera;
    CameraFollow cameraFollowScript;
    [SerializeField]
    PlayableDirector m_animation;
    [SerializeField]
    GameObject evil;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        cutScenePlayed = false;   
    }

    void Start()
    {
        m_instantiateObstacles = GameObject.FindGameObjectsWithTag("InstantiateObstacles")[0].GetComponent<InstantiateObstacles>();
        // GameObject[] playerGameObjs = GameObject.FindGameObjectsWithTag("Player");
        // foreach (GameObject playerGameObj in playerGameObjs)
        // {
        //    print(playerGameObj.ToString());
        //     players.Add(playerGameObj.GetComponent<Player>());
        // }

        mainCamera = Camera.main;
        cameraFollowScript = mainCamera.GetComponent<CameraFollow>();

        StartCoroutine(StartRoom(roomIndex));
    }

    public void Victory() 
    {
        PausePlay();
        PlayCutScene();
        SceneManager.LoadScene("Victory");
    }
    public IEnumerator StartRoom(int roomIndex)
    {
        PausePlay();
        PlayCutScene();
        yield return StartCoroutine(InstantiateObstacles(roomIndex));
        evil.SetActive(false);
        ResumePlay();
        cutScenePlayed = true;
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

    public void PlayCutScene()
    {
        if (cutScenePlayed)
        {
            return;
        }
        evil.SetActive(true);
        m_animation.Play();
    }

    IEnumerator InstantiateObstacles(int roomIndex)
    {
        yield return StartCoroutine(m_instantiateObstacles.InstantiateObstaclesInRoom(roomIndex, cutScenePlayed));
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // public void GameOver(){
    //     SceneManager.LoadScene("Defeat");
    // }    
}
