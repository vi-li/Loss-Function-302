using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QueenType : PieceType
{
    public BulletSpawner bulletSpawner;

    private void Awake()
    {
        abilityTimer = 0;
        player = gameObject.GetComponent<Player>();

        bulletSpawner = gameObject.AddComponent<BulletSpawner>();
        bulletSpawner.spawnDatas = new List<BulletSpawnData>();

        // Use Queen Attack Asset
        // string[] assetGuids = AssetDatabase.FindAssets("Queen_Attack_Player");
        // string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[0]);
        // bulletSpawner.spawnDatas.Add((BulletSpawnData)AssetDatabase.LoadAssetAtPath(assetPath, typeof(BulletSpawnData)));

        string assetPath = "AttackData/Queen_Attack_Player";
        bulletSpawner.spawnDatas.Add((BulletSpawnData)Resources.Load<BulletSpawnData>(assetPath));
    }

    public override void Attack()
    {
        if (abilityTimer <= 0)
        {
            bulletSpawner.SpawnBullets();
            abilityTimer = bulletSpawner.GetSpawnData().cooldown;
        }
    }
}
