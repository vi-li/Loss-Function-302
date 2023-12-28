using UnityEngine;
public class AttackSpawnData : ScriptableObject
{
    public GameObject attackResource;
    public float cooldown;
    public float damage;
    public float lifetime;
    public string type;
}
