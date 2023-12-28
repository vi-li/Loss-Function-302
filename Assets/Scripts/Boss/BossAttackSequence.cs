using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSequence : MonoBehaviour
{
    private bool isDead;

    public GameObject head;
    public GameObject facialExpression;
    public GameObject blinkExpression;
    public GameObject pawL;
    public GameObject pawR;

    public GameObject pawAnchorL;
    public GameObject pawAnchorR;

    private BulletSpawner headBS;
    private BulletSpawner pawLBS;
    private BulletSpawner pawRBS;

    private SpriteRenderer expressionSpriteRenderer;

    public Sprite faceScream;
    public Sprite faceNeutralOpenEyes;
    public Sprite faceNeutralClosedEyes;

    public GameObject objectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;

        head = gameObject.transform.Find("Head").gameObject;
        pawL = gameObject.transform.Find("PawL").gameObject;
        pawR = gameObject.transform.Find("PawR").gameObject;

        facialExpression = gameObject.transform.Find("Head").gameObject.transform.Find("FacialExpression").gameObject;
        blinkExpression = gameObject.transform.Find("Head").gameObject.transform.Find("BlinkExpression").gameObject;

        pawAnchorL = gameObject.transform.Find("PawAnchorL").gameObject;
        pawAnchorR = gameObject.transform.Find("PawAnchorR").gameObject;

        expressionSpriteRenderer = facialExpression.GetComponent<SpriteRenderer>();

        headBS = head.transform.Find("BulletSpawnerObj").gameObject.GetComponent<BulletSpawner>();
        pawLBS = pawL.transform.Find("BulletSpawnerObj").gameObject.GetComponent<BulletSpawner>();
        pawRBS = pawR.transform.Find("BulletSpawnerObj").gameObject.GetComponent<BulletSpawner>();

        facialExpression.SetActive(false);
        blinkExpression.SetActive(true);
        
        StartCoroutine(Loop());
        
    }
    
    IEnumerator Loop()
    {
        while(!isDead)
        {
            print("about to attack");
            yield return new WaitForSeconds(2);
            yield return StartCoroutine(AttackRoutine());
        }
    }
    
    IEnumerator AttackRoutine()
    {
        float rngNum = Random.Range(1, 4);
        bool rngPawBool = Random.Range(0, 2) == 1;
        GameObject paw = rngPawBool ? pawL : pawR;
        GameObject pawAnchor = rngPawBool ? pawAnchorL : pawAnchorR;
        BulletSpawner pawBS = rngPawBool ? pawLBS : pawRBS;

        switch (rngNum)
        {
            case 1:
                print("swipe");
                yield return StartCoroutine(SwipeAttack(pawL, pawAnchorL, pawLBS, 0.1f));
                yield return StartCoroutine(SwipeAttack(pawR, pawAnchorR, pawRBS, 0.1f));
                break;

            case 2:
                print("scream");
                float rngRotation = Random.Range(-10.0f, -45.0f);
                yield return StartCoroutine(ScreamAttack(1.5f, rngRotation, 5));
                break;

            case 3:
                print("bomb");
                yield return StartCoroutine(BombAttack(paw, pawAnchor, 0.75f));
                yield return StartCoroutine(BombAttack(paw, pawAnchor, 0.75f));
                break;
        }
        // RotateAround(GameObject obj, float inTimeSecs, float angle, GameObject anchorObj)
        ///yield return StartCoroutine(RotateToAngle(head, 0.5f, 20));
        // yield return StartCoroutine(ScreamAttack(2.0f, -45.0f, 5));
        // yield return new WaitForSeconds(2);
        // yield return StartCoroutine(ScreamAttack(2.0f, 45.0f, 5));
        // yield return new WaitForSeconds(2);
        //yield return StartCoroutine(RotateAround(pawL, 1.0f, -15.0f, pawAnchorL));
        //yield return StartCoroutine(SwipeAttack(pawL, pawLBS, 0.1f));
        //yield return StartCoroutine(BombAttack());
    }

    IEnumerator ScreamAttack(float duration, float angleToRotate, int numWavesBullets)
    {
        SwapExpression();
        expressionSpriteRenderer.sprite = faceScream;
        yield return StartCoroutine(RotateSpawnBullets(head, headBS, duration, angleToRotate, numWavesBullets));
        yield return StartCoroutine(RotateToAngle(head, 1.0f, 0.0f));
        SwapExpression();
    }

    IEnumerator SwipeAttack(GameObject paw, GameObject pawAnchor, BulletSpawner pawBS, float duration)
    {
        SwapExpression();
        expressionSpriteRenderer.sprite = faceNeutralClosedEyes;

        float rngRotation = Random.Range(10.0f, 30.0f);
        
        float pawRotateAngle;
        if (paw.tag == "CootsPawL")
        {
            pawRotateAngle = rngRotation;
        } else
        {
            pawRotateAngle = rngRotation * -1;
        }

        yield return StartCoroutine(RotateSpawnBullets(paw, pawBS, duration, pawRotateAngle, 1));
        yield return StartCoroutine(RotateToAngle(paw, 0.5f, 0.0f));
        SwapExpression();
    }

    IEnumerator BombAttack(GameObject pawFrom, GameObject pawFromAnchor, float duration)
    {
        Vector3 startLocation = pawFrom.transform.Find("BulletSpawnerObj").transform.position;
        Quaternion startRotation = pawFrom.transform.rotation;
        Vector3 endLocation = startLocation + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, -6f));

        yield return StartCoroutine(RotateAround(pawFrom, 0.2f, -15.0f, pawFromAnchor));
        yield return StartCoroutine(RotateAround(pawFrom, 0.1f, 15.0f, pawFromAnchor));
        GameObject bomb = Instantiate(objectToSpawn, startLocation, startRotation);
        yield return StartCoroutine(MoveToLocation(bomb, duration, startLocation, endLocation));
        bomb.GetComponent<Bomb>().Explode();
    }

    IEnumerator RotateToAngle(GameObject obj, float duration, float angleToRotateTo)
    {
        Transform objTranform = obj.transform;

        Quaternion startRotation = objTranform.rotation;
        Quaternion endRotation = Quaternion.Euler(objTranform.eulerAngles.x, objTranform.eulerAngles.y, angleToRotateTo);
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            objTranform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / duration);

            yield return null;
        }
    }

    IEnumerator RotateSpawnBullets(GameObject obj, BulletSpawner bs, float duration, float angleToRotate, int numWavesBullets)
    {
        Transform objTranform = obj.transform;

        Quaternion startRotation = objTranform.rotation;
        Quaternion endRotation = Quaternion.Euler(objTranform.eulerAngles.x, objTranform.eulerAngles.y, objTranform.eulerAngles.z + angleToRotate);
        float timer = 0.0f;
        float bulletInterval = duration / (float)numWavesBullets;
        float timeNextWave = timer + bulletInterval;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            objTranform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / duration);

            if (timer >= timeNextWave)
            {
                print(bs.transform.eulerAngles);
                bs.SpawnBullets();
                timeNextWave += bulletInterval;
            }

            yield return null;
        }
    }

    IEnumerator RotateAround(GameObject obj, float duration, float angle, GameObject anchorObj)
    {
        Vector3 anchorPosition = anchorObj.transform.position;
        float timer = 0.0f;
        float angleDelta = angle / duration; // How many degress to rotate in one second
        float ourTimeDelta = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            ourTimeDelta = Time.deltaTime;

            // Make sure we dont spin past the angle we want.
            if (timer > duration)
            {
                ourTimeDelta -= (timer - duration);
            }

            obj.transform.RotateAround(anchorPosition, Vector3.forward, angleDelta * ourTimeDelta);

            yield return null;
        }
    }

    IEnumerator MoveToLocation(GameObject obj, float duration, Vector3 startLocation, Vector3 endLocation)
    {
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            obj.transform.position = Vector3.Slerp(startLocation, endLocation, timer / duration);
            yield return null;
        }
    }

    void SwapExpression()
    {
        blinkExpression.SetActive(!blinkExpression.activeSelf);
        facialExpression.SetActive(!facialExpression.activeSelf);
    }

    // private float SmoothProgress(float progress)
    // {
    //     //maps the progress between -π/2 to π/2
    //     progress = Mathf.Lerp(-Mathf.PI/2, Mathf.PI/2, progress);
    //     //returns a value between -1 and 1
    //     progress = Mathf.Sin(progress);
    //     //scale the sine value between 0 and 1.
    //     progress = (progress/2f) + .5f;
    //     return progress;
    // }

    public void Death()
    {
        // Death Sequence
        isDead = true;
        StartCoroutine(DeathCoroutine());
        gameObject.SetActive(false);
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(2f);
    }
}
