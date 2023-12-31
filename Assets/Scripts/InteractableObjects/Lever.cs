using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lever : MonoBehaviour
{
    [SerializeField]
    List<GameObject> affected;
    Animator animator;
    [SerializeField]
    public float cooldownTimer = 0f;
    public float cooldownDuration = 0.5f;
    [SerializeField]
    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isActive", isActive);
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (cooldownTimer <= 0 && collision.tag == "Player" && Input.GetKey("space"))
        {
            Flip();
        }
    } 

    void Flip()
    {   
        isActive = !isActive;
        animator.SetBool("isActive", isActive);
        foreach (GameObject gameObj in affected)
        {
            gameObj.SetActive(isActive);
        }
        cooldownTimer = cooldownDuration;
    }
}
