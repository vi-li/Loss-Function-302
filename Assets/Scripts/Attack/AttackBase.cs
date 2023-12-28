using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    protected Vector2 velocity;
    protected float speed;
    protected float rotation;
    protected float damage;
    protected float lifetime;
    protected float timer;
    public string type;

    public enum AttackDirection {
        left, right
    }

    public void TickTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void DeltaMoveWithSpeed()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    public void ResetTimer()
    {
        timer = lifetime;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetRotation(float rotation)
    {
        this.rotation = rotation;
    }

    public void SetLifetime(float lifetime)
    {
        this.lifetime = lifetime;
    }

    public float GetDamage()
    {
        return damage;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetRotation()
    {
        return rotation;
    }

    public float GetLifetime()
    {
        return lifetime;
    }

    public float GetTimer()
    {
        return timer;
    }

    public string GetAttackType()
    {
        return type;
    }
}
