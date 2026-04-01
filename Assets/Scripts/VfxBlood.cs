using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxBlood : MonoBehaviour
{

    Renderer targetRenderer;
    Renderer myRenderer;
    public void SetLayer(SpriteRenderer owner)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerID = owner.sortingLayerID;
        sr.sortingOrder = owner.sortingOrder;
    }

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        targetRenderer = GetComponentInParent<Renderer>();
    }

    private void LateUpdate()
    {
        if (targetRenderer == null) return;
        myRenderer.sortingLayerID = targetRenderer.sortingLayerID;
        myRenderer.sortingOrder = targetRenderer.sortingOrder;
        gameObject.layer = targetRenderer.gameObject.layer;
    }
}
