using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] 
    protected Canvas UI;
    [SerializeField]
    protected Tilemap groundTilemap;
    [SerializeField]
    protected Tilemap collisionTilemap;
    [SerializeField]
    protected Tilemap collisionTilemap2;
    protected PlayerMovement controls;
    [SerializeField]
    protected GameObject[] impermeables;

    [SerializeField]
    protected float smoothSpeed = 10.0f;
    public Vector3 moveToPosition;
    
    protected Vector3 velocity = Vector3.zero;
    protected Direction facingDirection;

    // public float startHp;
    // public float hp;

    public bool isInvulnerable = false;
    //public float transformTimer;
    public float flickerDuration;
    public float flickerCount;
    
    //public PlayerType defaultPiece;
    //public PlayerType piece;
    public GameController control;

    public bool isBeingControlled;
    public bool isPaused;

    protected SpriteRenderer spriteRenderer;
    Sprite currentSprite;
    Animator animator;
    CameraFollow cameraFollow;
    Vector3 currentPosition;

    protected enum Direction {
        left, right, up, down
    }

    private void Awake() {
        controls = new PlayerMovement();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    protected void OnEnable() {
        controls.Enable();
    }

    protected void OnDisable() {
        controls.Disable();
    }

    void Start()
    {
        controls.Main.Fire.performed += ctx => OnFire();
        // hp = startHp;
        // print("set player hp " + hp);
        //SetHealthBar();
        // invulnerabilityTimer = 0;

        moveToPosition = transform.position;
        //controls.Main.Fire.performed += ctx => OnFire();
        isInvulnerable = false;

        impermeables = GameObject.FindGameObjectsWithTag("Player");
    }

    public void GoToSleep()
    {
        controls.Main.Movement.performed -= MoveCallback;
        isBeingControlled = false;
        print(gameObject.ToString() + "is sleeping");
        
        // Visually go to sleep
        animator.SetInteger("facingDirection", (int)Direction.down);
        animator.SetBool("isSleeping", true);
    }

    public void WakeUp()
    {
        print(gameObject.ToString() + "is waking");
        controls.Main.Movement.performed += MoveCallback;
        isBeingControlled = true;

        // Visually wake up
        animator.SetInteger("facingDirection", (int)Direction.down);
        animator.SetBool("isSleeping", false);
        print("setting objectToTarget to " + gameObject.ToString());
        //cameraFollow.objectToTarget = gameObject;
    }
    
    void Update()
    {
        //TickTimer();
        SmoothMove();

        if (Input.GetButtonDown("Swap") && !isPaused)
        {
            if (isBeingControlled)
            {
                GoToSleep();
            } else
            {
                WakeUp();
            }
        }
    }

    private void TickTimer()
    {
        // if (invulnerabilityTimer > 0)
        // {
        //     invulnerabilityTimer -= Time.deltaTime;
        // }

        // if (transformTimer > 0)
        // {
        //     transformTimer -= Time.deltaTime;
        //     if (transformTimer <= 0)
        //     {
        //         transformation back to og
        //     }
        // }
    }

    protected bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(moveToPosition + (Vector3)direction);

        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition) 
            || (collisionTilemap2.HasTile(gridPosition) && collisionTilemap2.gameObject.activeSelf))
        {
            return false;
        }

        foreach (GameObject impermeable in impermeables)
        {
            // If NPC (or other impermable gameobject) is in the way of the player's intended position
            if (groundTilemap.WorldToCell(impermeable.transform.position) == gridPosition)
            {
                return false;
            }
        }

        return true;
    }

    private void MoveCallback(InputAction.CallbackContext ctx)
    {
        if (isBeingControlled)
        {
            Move(controls.Main.Movement.ReadValue<Vector2>());
        }
    }

    protected void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            moveToPosition = moveToPosition + (Vector3)direction;
        }

        Rotate(direction);
    }

    protected void Rotate(Vector2 direction)
    {
        if (direction.x > 0)
        {
            facingDirection = Direction.right;
        } else if (direction.x < 0)
        {
            facingDirection = Direction.left;
        } else if (direction.y > 0)
        {
            facingDirection = Direction.up;
        } else
        {
            facingDirection = Direction.down;
        }

        //UpdatePlayerRotation();
        UpdateAnimator();
    }

    // To be used in the case of "front-facing" sprites
    protected void UpdateAnimator()
    {
        animator.SetInteger("facingDirection", (int)facingDirection);
    }

    // To be used in the case of "top-down" sprites
    // protected void UpdatePlayerRotation()
    // {
    //     switch (facingDirection)
    //     {
    //         case(Direction.right):
    //             transform.eulerAngles = new Vector3(0, 0, 270);
    //             break;
    //         case(Direction.up):
    //             transform.eulerAngles = new Vector3(0, 0, 0);
    //             break;
    //         case(Direction.left):
    //             transform.eulerAngles = new Vector3(0, 0, 90);
    //             break;
    //         case(Direction.down):
    //             transform.eulerAngles = new Vector3(0, 0, 180);
    //             break;
    //     }
    // }

    protected void SmoothMove()
    {
        if (moveToPosition != transform.position)
        {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
        transform.position = Vector3.SmoothDamp(transform.position, moveToPosition, ref velocity, smoothSpeed);
    }

    //public virtual void SetHealthBar() {}
    /*
    if(hp == 3){
            UI.transform.Find("Heart1").gameObject.SetActive(true);
            UI.transform.Find("Heart2").gameObject.SetActive(true);
            UI.transform.Find("Heart3").gameObject.SetActive(true);
        }
        else if(hp == 2){
            UI.transform.Find("Heart1").gameObject.SetActive(true);
            UI.transform.Find("Heart2").gameObject.SetActive(true);
            UI.transform.Find("Heart3").gameObject.SetActive(false);
        }
        else if(hp == 1){
            UI.transform.Find("Heart1").gameObject.SetActive(true);
            UI.transform.Find("Heart2").gameObject.SetActive(false);
            UI.transform.Find("Heart3").gameObject.SetActive(false);
        }
    */

    IEnumerator DamageFlicker(float invul) {
        float flickerCount = 10f;
        float flickerInterval = invul / flickerCount;

        for (int i = 0; i < flickerCount; i++) {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(flickerInterval);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flickerInterval);
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "InstantDeath" && !isInvulnerable) // && invulnerabilityTimer <= 0
        {
            // float damage = collision.gameObject.GetComponent<Bullet>().GetDamage();
            // hp -= damage;
            // print("Player Health: " + hp);
            // SetHealthBar();
            // invulnerabilityTimer = invulnerabilityCooldown;
            // StartCoroutine(DamageFlicker(invulnerabilityTimer));
            // TODO: Make them die instantly
            print(gameObject.ToString() + " died");
            control.Reset();
        }
    }

     protected void OnFire()
     {
         currentPosition = gameObject.transform.position;
         //Vector2 direction = controls.Main.Movement.ReadValue<Vector2>();
     }
}