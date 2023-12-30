using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshProSortingLayer : MonoBehaviour
{
    MeshRenderer _meshRenderer;
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer.sortingLayerName = "UI";
        _meshRenderer.sortingOrder = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
