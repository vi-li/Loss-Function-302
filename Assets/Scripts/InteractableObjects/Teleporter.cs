using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Teleporter destination;
    [SerializeField]
    public float cooldownTimer = 0f;
    public float cooldownDuration = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && cooldownTimer <= 0 && collision.gameObject.GetComponent<Neuro>() != null) {
            destination.cooldownTimer = cooldownDuration;
            Teleport(collision.gameObject);
        }
    } 

    void Teleport(GameObject objToTeleport)
    {
        Vector3 teleportPosition = destination.gameObject.transform.position;
        // Do any animation
        objToTeleport.transform.position = teleportPosition;
        objToTeleport.GetComponent<Player>().moveToPosition = teleportPosition;
        // Do any animation
    }
}
