using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuro : MonoBehaviour
{
    Player player;
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
        //player.GoToSleep();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.GoToSleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
