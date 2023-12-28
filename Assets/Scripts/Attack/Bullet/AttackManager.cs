using UnityEngine;
using System.Collections.Generic;
public class AttackManager : MonoBehaviour
{
    public static List<GameObject> attacks;

    private void Start()
    {
        attacks = new List<GameObject>();
    }

    public static GameObject GetAttackFromPoolWithType(string specificType)
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            if (!attacks[i].activeSelf && attacks[i].GetComponent<AttackBase>().GetAttackType() == specificType)
            {
                GameObject availableAttack = attacks[i];
                var b = availableAttack.GetComponent<AttackBase>();
                b.ResetTimer();
                return availableAttack;
            }
        }

        return null;
    }
}