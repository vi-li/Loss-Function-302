using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Computer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> affected;
    Animator animator;
    bool isHacked = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (!isHacked && collision.tag == "Player" && collision.gameObject.GetComponent<Vedal>() != null && Input.GetKey("space")) {
            StartCoroutine(Hacked());
        }
    } 

    public IEnumerator Hacked()
    {
        animator.SetBool("isHacked", true);
        isHacked = true;
        foreach (GameObject gameObj in affected)
        {
            gameObj.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
