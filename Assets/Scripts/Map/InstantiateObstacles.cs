using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObstacles : MonoBehaviour
{
    Dictionary<int, List<GameObject>> toInstantiate;
    public IEnumerator InstantiateObstaclesInRoom(int roomToInstantiate, bool cutScenePlayed)
    {
        if (cutScenePlayed)
        {
            print("Instantiating all objs at once bc played cutscene");
            // Instantiate all objects in list corresponding to room at once
            yield break;
        }

        switch (roomToInstantiate)
        {
            case 0:
                // Do coroutine stuff
                print("Waiting 14 seconds");
                yield return new WaitForSeconds(14);
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
