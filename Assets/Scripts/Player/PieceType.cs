using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceType : MonoBehaviour
{
    public Player player;
    public float abilityTimer;
    public float abilityCooldown = 0.25f;

    private void Update()
    {
        TickTimer();
    }

    private void TickTimer()
    {
        if (abilityTimer > 0)
        {
            abilityTimer -= Time.deltaTime;
        }
    }

    public virtual void Attack() { }
}
