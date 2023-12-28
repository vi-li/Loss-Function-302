using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startHp;
    public float hp;

    public float invulnerabilityCooldown;
    public float invulnerabilityTimer;
    public SpriteRenderer rend;
    // Start is called before the first frame update
    public void Start()
    {
        hp = startHp;
        rend = transform.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        TickTimer();
    }

    private void TickTimer()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
    }
    IEnumerator takeDamage(){
        Color currentColor = rend.color;
        rend.color = Color.red;
        yield return new WaitForSeconds(.2f);
        rend.color = currentColor;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack" && invulnerabilityTimer <= 0)
        {
            float damage = collision.gameObject.GetComponent<AttackBase>().GetDamage();
            hp -= damage;
            print("Enemy Health: " + hp);
            StartCoroutine(takeDamage());
            if (hp <= 0)
            {
                print("Enemy died");
                Death();
            }

            invulnerabilityTimer = invulnerabilityCooldown;
        }
    }

    public void Death()
    {
        gameObject.GetComponent<DropUpgrades>().Drop();
        gameObject.SetActive(false);
    }
}
