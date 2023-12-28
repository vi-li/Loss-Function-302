using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class PawnType : PieceType
{
    public SlashSpawnData spawnData;

    private void Start()
    {
        abilityTimer = 0;
        player = gameObject.GetComponent<Player>();
    }
    
    private void Awake() 
    {
        // Use Pawn Attack Asset
        // string[] assetGuids = AssetDatabase.FindAssets("Slash_Player_Attack");
        // string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[0]);
        // spawnData = (SlashSpawnData)AssetDatabase.LoadAssetAtPath(assetPath, typeof(SlashSpawnData));

        string assetPath = "AttackData/Slash_Player_Attack";
        spawnData = (SlashSpawnData)Resources.Load<SlashSpawnData>(assetPath);
    }

    public override void Attack()
    {
        if (abilityTimer <= 0)
        {
            GameObject spawnedSlash = AttackManager.GetAttackFromPoolWithType(spawnData.type);

            if (spawnedSlash == null)
            {
                spawnedSlash = Instantiate(spawnData.attackResource);
                AttackManager.attacks.Add(spawnedSlash);
            }

            spawnedSlash.tag = "PlayerAttack";

            spawnedSlash.transform.SetParent(transform);
            spawnedSlash.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            spawnedSlash.transform.localPosition = new Vector2(0, 1 / transform.localScale.y);
            spawnedSlash.transform.rotation = transform.rotation;
            spawnedSlash.transform.SetParent(null);

            var slash = spawnedSlash.GetComponent<Slash>();
            slash.SetLifetime(spawnData.lifetime);
            slash.SetDamage(spawnData.damage);

            spawnedSlash.SetActive(true);

            abilityTimer = abilityCooldown;
        }   
    }
}
