using UnityEngine;

public abstract class PowerupEffect : ScriptableObject
{
    public abstract void Apply(GameObject target);
    // Start is called before the first frame update
}
