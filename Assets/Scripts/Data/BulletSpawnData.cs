using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletSpawnData", order = 1)]
public class BulletSpawnData : AttackSpawnData
{
    public Vector2 bulletVelocity;
    public float bulletSpeed;
    public int bulletCount;
    public float minRotation;
    public float maxRotation;
    public List<float> specificRotations;
    public bool isRandom;
    public bool isNotParent = true;
}