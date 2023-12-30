using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InstantiateObstacles : MonoBehaviour
{
    [SerializeField]
    List<Tilemap> tilemapsToInstantiate;

    void Awake()
    {

    }

    public IEnumerator InstantiateObstaclesInRoom(int roomToInstantiate, bool cutScenePlayed)
    {
        if (cutScenePlayed)
        {
            foreach (Tilemap tilemap in tilemapsToInstantiate)
            {
                foreach (Transform childTransform in tilemap.transform)
                {
                    childTransform.gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield break;
        }

        switch (roomToInstantiate)
        {
            case 0:
                print("Instantiating Tutorial Room");
                yield return new WaitForSeconds(10.8f);
                foreach (Transform childTransform in tilemapsToInstantiate[0].transform)
                {
                    childTransform.gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.1f); //11.2
                }
                yield return new WaitForSeconds(2.8f);
                yield break;
            // case 1:
            //     // Do coroutine
            // case 2:
            //     // Do coroutine
            // case 3:
            //     // Do coroutine
            // default:
        }
        yield break;
    }
}
