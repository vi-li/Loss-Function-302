using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSpaceToPlay : MonoBehaviour
{
    SceneFader sceneFader;
    // Start is called before the first frame update
    void Start()
    {
        sceneFader = GetComponent<SceneFader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(sceneFader.FadeAndLoadScene(SceneFader.FadeDirection.Out, "bullethellchess"));
        }
    }
}
