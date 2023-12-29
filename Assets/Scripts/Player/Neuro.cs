using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuro : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        player.GoToSleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
