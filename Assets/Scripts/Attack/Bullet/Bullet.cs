using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackBase
{
    public bool oneTimeUse = true;

    void Start()
    {
        timer = lifetime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    
    private void OnEnable() 
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    void Update()
    {
        DeltaMoveWithSpeed();
        TickTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (oneTimeUse)
        {
            string tag = gameObject.tag;

            if (tag == "PlayerAttack" && collision.tag == "Enemy")
            {
                gameObject.SetActive(false);
            }

            if (tag == "EnemyAttack" && collision.tag == "Player")
            {
                gameObject.SetActive(false);
            }
        }
    }
}