using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour
{
    [SerializeField]
    float vfx = 0.3f;
    void Start()
    {
        Destroy(gameObject, vfx);
    }

    public void SetLayer(SpriteRenderer owner)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerID = owner.sortingLayerID;
        sr.sortingOrder = owner.sortingOrder;
    }
}