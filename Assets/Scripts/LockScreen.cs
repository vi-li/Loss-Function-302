using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LockScreen : MonoBehaviour
{
    public CameraFollow cameraF;
    public Camera cam;
    public Tilemap collisionTilemap;

    public GameObject enemyPieces;
    public GameObject coots;
    //public Canvas bossHealth;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        enemyPieces.SetActive(false);
        coots.SetActive(true);
        //bossHealth.transform.Find("BossHealth").gameObject.SetActive(true);

        collisionTilemap.gameObject.SetActive(true);
        if (collision.tag == "Player")
        {
            cam.orthographicSize += 1;
            cam.transform.position = cam.transform.position + new Vector3(0f, 5f);
            cameraF.isYLocked = true;
            gameObject.SetActive(false);
        }
       
    }
}
