using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vedal : MonoBehaviour
{
    Player player;

    void Awake()
    {
        player = gameObject.GetComponent<Player>();
        //player.WakeUp();
    }
    void Start()
    {
        player.WakeUp();
    }

    void Update()
    {
        
    }
}
