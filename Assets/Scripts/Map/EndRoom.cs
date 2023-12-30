using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    [SerializeField]
    List<string> playersInCollider;
    GameController gameController;
    List<GameObject> doorToDeactivate;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playersInCollider.Add(collision.gameObject.ToString());
            print(collision.gameObject.ToString() + " entered finish area");
        }

        if (playersInCollider.Count == 2)
        {
            // Do other stuff like pause play and animation stuff
            print("Would enter next room now");
            EndRoomSequence();
            GetComponent<SceneFader>().FadeOutScene("PuzzleRoom 1");
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playersInCollider.Remove(collision.gameObject.ToString());
            print(collision.gameObject.ToString() + " left finish area");
        }
    }

    IEnumerator EndRoomSequence()
    {
        yield return new WaitForSeconds(2.0f);
    }

}
