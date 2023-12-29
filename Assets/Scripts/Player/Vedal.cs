using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vedal : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        player.WakeUp();
    }

    void Update()
    {
        
    }
}
