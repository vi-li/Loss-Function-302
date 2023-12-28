using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageCanvasAnimator : MonoBehaviour
{   
    // the UI/Image component
    Image imageCanvas;
    // the fake SpriteRenderer
    public SpriteRenderer fakeRenderer;

    void Start ()
    {
        imageCanvas = GetComponent<Image>();
    }

    void Update () 
    {
        imageCanvas.sprite = fakeRenderer.sprite;
    }
}