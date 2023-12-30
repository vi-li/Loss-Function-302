using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartRoom : MonoBehaviour
{
    [SerializeField]
    int roomIndex = 0;
    GameController gameController;
    void Start()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameController.StartRoom(roomIndex);
        }
        gameObject.SetActive(false);
    }
}
