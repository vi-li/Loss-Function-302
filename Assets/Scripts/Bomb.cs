using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private BulletSpawner spawner;
    private DropUpgrades dropUpgrades;

    private void Start() {
        dropUpgrades = GetComponent<DropUpgrades>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(180 * Vector3.forward * Time.deltaTime);
    }

    public void Explode()
    {
        spawner = gameObject.GetComponent<BulletSpawner>();
        spawner.SpawnBullets();
        dropUpgrades.Drop();
        Destroy(gameObject);
    }
}
