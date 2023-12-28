using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : AttackBase
{
    void Start()
    {
        lifetime = 0.5f;
        timer = lifetime;
    }

    void Update()
    {
        TickTimer();
    }
}
