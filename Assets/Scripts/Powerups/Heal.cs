using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Heal")]

public class Heal : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        float hp = player.hp;
        float startHp = player.startHp;
        hp += amount;

        if (hp > startHp)
        {
            hp = startHp;
        }

        player.hp = hp;
        player.UpdateHealth();
        Debug.Log(target.GetComponent<Player>().hp);
    }
}
